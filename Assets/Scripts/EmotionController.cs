using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmotionController : MonoBehaviour
{
  [Header("Setup")]
  [SerializeField] private GameSetting setting;
  [SerializeField] private AceptItem aceptItem;

  [SerializeField] private IndicatorUI indicator;
  [SerializeField] private EmotionSpawnerUI spawner;

  [Header("Data")]
  [SerializeField] private EmotionDifficulty easyDifficulty;
  [SerializeField] private EmotionDifficulty normalDifficulty;
  [SerializeField] private EmotionDifficulty hardDifficulty;
  [SerializeField] private EmotionDifficulty chaosDifficulty;

  [Header("UI")]
  [SerializeField] private Button button1;
  [SerializeField] private Button button2;
  [SerializeField] private Button button3;
  [SerializeField] private Button button4;

  [SerializeField] private TextMeshProUGUI textMesh;
  private EmotionDifficulty _difficulty;
  private readonly EmotionTurn turn = new();
  private int score;

  void Start()
  {
    button1.onClick.AddListener(() => Setup(easyDifficulty));
    button2.onClick.AddListener(() => Setup(normalDifficulty));
    button3.onClick.AddListener(() => Setup(hardDifficulty));
    button4.onClick.AddListener(() => Setup(chaosDifficulty));

    textMesh.text = $"Score : {score}";

    turn.SetEdgePadding(setting.EdgePadding);

    indicator.OnPress += OnIndicatorPress;

    Setup(easyDifficulty);
  }

  private void Setup(EmotionDifficulty difficulty)
  {
    turn.Clear();
    spawner.ClearAll();

    _difficulty = difficulty;

    var range = indicator.GetBarRange01();
    turn.SetBarRange(range.min, range.max);
    turn.Setup(_difficulty.InitialItems, aceptItem);

    spawner.SpawnAll(turn.EmotionItems);
  }

  private void OnIndicatorPress(float t)
  {
    var respawn = _difficulty.GetRandomRespawn();

    bool hit = turn.Resolve(
      t,
      respawn,
      out var hitItem,
      out int gain,
      out bool endTurn);

    score += hit ? gain : _difficulty.MissPenalty;

    if (hit)
      spawner.Remove(hitItem);

    if (endTurn)
    {
      turn.Clear();
      spawner.ClearAll();
      indicator.ResetIndicator();

      Debug.Log($"Deal Accepted! Final Score: {score}");
      return;
    }

    spawner.SpawnAll(turn.EmotionItems);
    textMesh.text = $"Score : {score}";

    if (score >= _difficulty.RequestScore)
    {
      Debug.Log("Complete");
    }
  }
}
