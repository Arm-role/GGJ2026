using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmotionController : MonoBehaviour
{
  [Header("Setup")] [SerializeField] private GameSetting setting;
  [SerializeField] private EmotionTypeDifficulty[] emotionTypeDifficulty;

  [SerializeField] private IndicatorUI indicator;
  [SerializeField] private EmotionSpawnerUI spawner;
  [SerializeField] private EmotionScoreBar scoreBar;

  [Header("Setup Param")] [SerializeField]
  private string path;

  [Header("Data")] [SerializeField] private EmotionDifficulty easyDifficulty;
  [SerializeField] private EmotionDifficulty normalDifficulty;
  [SerializeField] private EmotionDifficulty hardDifficulty;
  [SerializeField] private EmotionDifficulty chaosDifficulty;

  [Header("UI")] [SerializeField] private Button button1;
  [SerializeField] private Button button2;
  [SerializeField] private Button button3;
  [SerializeField] private Button button4;


  [SerializeField] private Image Image1;
  [SerializeField] private Image Image2;
  [SerializeField] private Image Image3;
  [SerializeField] private Image Image4;

  [SerializeField] private TextMeshProUGUI textMesh;
  [SerializeField] private GameObject huggingObject;

  private readonly EmotionTurn turn = new();
  private SpriteProvider spriteProvider;

  private EmotionGameSession session;
  private EmotionGameState state = EmotionGameState.Idle;

  private EmotionTypeDifficulty _typeDifficulty;
  private EmotionDifficulty _difficulty;
  private int score;

  private EmotionType _inputEmotion;
  private EmotionType _outputEmotion;

  public Action<EmotionContext> OnEmotionChanged;

  private ITargetable _target;

  void Start()
  {
    spriteProvider = new SpriteProvider(path);

    button1.onClick.AddListener(ChoiseA);
    button2.onClick.AddListener(ChoiseB);
    button3.onClick.AddListener(ChoiseC);
    button4.onClick.AddListener(ChoiseD);

    textMesh.text = $"{score}";

    turn.SetEdgePadding(setting.EdgePadding);

    indicator.OnPress += OnIndicatorPress;

    huggingObject.SetActive(false);
  }

  public void Setup(EmotionType emotionInput, ITargetable targetable)
  {
    GameStateManager.Set(GameState.HugglingUI);

    _target = targetable;
    _inputEmotion = emotionInput;
    _typeDifficulty = GettypeDifficulty(emotionInput);

    Image1.sprite = spriteProvider.Get(_typeDifficulty.Easy.EmotionType.ToString());
    Image2.sprite = spriteProvider.Get(_typeDifficulty.Normal.EmotionType.ToString());
    Image3.sprite = spriteProvider.Get(_typeDifficulty.Hard.EmotionType.ToString());
    Image4.sprite = spriteProvider.Get(_typeDifficulty.Chaos.EmotionType.ToString());

    ChoiseA();

    Debug.Log("Setup");
    huggingObject.SetActive(true);
  }

  private void SelectDifficulty(EmotionDifficulty difficulty)
  {
    state = EmotionGameState.Playing;

    _difficulty = difficulty;

    session = new EmotionGameSession(
      difficulty,
      _inputEmotion,
      _outputEmotion
    );

    turn.Clear();
    spawner.ClearAll();
    indicator.ResetIndicator();

    var range = indicator.GetBarRange01();
    turn.SetBarRange(range.min, range.max);
    turn.Setup(difficulty.InitialItems);

    spawner.SpawnAll(turn.EmotionItems);

    scoreBar.Setup(difficulty);
    scoreBar.SetScore(session.Score);
  }

  private void ChoiseA()
  {
    if (indicator.IsRunning) return;

    _outputEmotion = _typeDifficulty.Easy.EmotionType;
    SelectDifficulty(_typeDifficulty.Easy.Difficulty);
    EventSystem.current.SetSelectedGameObject(null);
    Debug.Log(_outputEmotion);
  }

  private void ChoiseB()
  {
    if (indicator.IsRunning) return;

    _outputEmotion = _typeDifficulty.Normal.EmotionType;
    SelectDifficulty(_typeDifficulty.Normal.Difficulty);
    EventSystem.current.SetSelectedGameObject(null);
    Debug.Log(_outputEmotion);
  }

  private void ChoiseC()
  {
    if (indicator.IsRunning) return;

    _outputEmotion = _typeDifficulty.Hard.EmotionType;
    SelectDifficulty(_typeDifficulty.Hard.Difficulty);
    EventSystem.current.SetSelectedGameObject(null);
    Debug.Log(_outputEmotion);
  }

  private void ChoiseD()
  {
    if (indicator.IsRunning) return;

    _outputEmotion = _typeDifficulty.Chaos.EmotionType;
    SelectDifficulty(_typeDifficulty.Chaos.Difficulty);
    EventSystem.current.SetSelectedGameObject(null);
    Debug.Log(_outputEmotion);
  }

  private void OnIndicatorPress(float t)
  {
    if (state != EmotionGameState.Playing)
      return;

    bool hit = turn.Resolve(
      t,
      _difficulty.GetRandomRespawn(),
      out var hitItem,
      out int gain);

    session.ApplyResult(hit, gain, _difficulty.MissPenalty);

    if (hit)
      spawner.Remove(hitItem);

    scoreBar.SetScore(session.Score);
    spawner.SpawnAll(turn.EmotionItems);
    textMesh.text = $"Score : {session.Score}";

    if (session.IsCompleted)
    {
      FinishSession(true);
    }
    else if (session.IsFailed)
    {
      FinishSession(false);
    }
  }

  private void FinishSession(bool success)
  {
    state = EmotionGameState.Finished;

    turn.Clear();
    spawner.ClearAll();
    indicator.ResetIndicator();
    huggingObject.SetActive(false);

    OnEmotionChanged?.Invoke(new EmotionContext(
      success ? EmotionTypeResutl.Success : EmotionTypeResutl.Failure,
      session.Score,
      _inputEmotion,
      session.OutputEmotion,
      _target));
  }


  private EmotionTypeDifficulty GettypeDifficulty(EmotionType emotion)
  {
    foreach (var diff in emotionTypeDifficulty)
    {
      if (diff.Type == emotion) return diff;
    }

    return null;
  }
}