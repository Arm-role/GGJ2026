using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [Header("Timer")] [SerializeField] private TextMeshProUGUI timerText;
  [SerializeField] private float startTime = 60f;

  [SerializeField] private EmotionController emotionController;
  [SerializeField] private EmotionTableSO emotionTable;
  [SerializeField] private EndGameUI endGameUI;

  private float currentTime;
  private bool isRunning;
  private GameSessionData sessionData;

  private void Start()
  {
    GameStateManager.Set(GameState.Gameplay);

    sessionData = new GameSessionData();

    currentTime = startTime;
    isRunning = true;
    UpdateTimerUI();

    emotionController.OnEmotionChanged += GetData;
  }

  private void OnDestroy()
  {
    emotionController.OnEmotionChanged -= GetData;
  }

  private void Update()
  {
    if (!isRunning) return;
    if (GameStateManager.Current == GameState.Gameplay ||
        GameStateManager.Current == GameState.HugglingUI)
    {
      currentTime -= Time.deltaTime;

      if (currentTime <= 0f)
      {
        currentTime = 0f;
        OnTimeUp();
      }

      UpdateTimerUI();
    }
  }

  private void UpdateTimerUI()
  {
    int minutes = Mathf.FloorToInt(currentTime / 60f);
    int seconds = Mathf.FloorToInt(currentTime % 60f);

    timerText.text = $"{minutes:00}:{seconds:00}";
  }

  private void OnTimeUp()
  {
    isRunning = false;

    EndingType ending = EvaluateEnding();
    endGameUI.Show(ending);

    GameStateManager.Set(GameState.GameOver);
  }

  private void GetData(EmotionContext ctx)
  {
    sessionData.totalInteractions++;
    sessionData.totalScore += ctx.Score;

    var transition = emotionTable.Get(ctx.From, ctx.To);

    if (transition != null)
    {
      sessionData.difficultyScore += transition.difficulty;
      sessionData.humanityScore += transition.humanity;
    }

    if (ctx.Result == EmotionTypeResutl.Success)
      sessionData.successCount++;
    else if (ctx.Result == EmotionTypeResutl.Failure)
      sessionData.failureCount++;
  }

  private EndingType EvaluateEnding()
  {
    float successRate = sessionData.SuccessRate;
    float humanity = sessionData.humanityScore;
    float difficulty = sessionData.difficultyScore;

    if (successRate >= 0.8f && humanity >= 5f && difficulty >= 10f)
      return EndingType.True;

    if (successRate >= 0.6f && humanity > 0f)
      return EndingType.Good;

    if (successRate >= 0.4f)
      return EndingType.Neutral;

    return EndingType.Bad;
  }
}