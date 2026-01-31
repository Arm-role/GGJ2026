using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmotionController : MonoBehaviour
{
  [Header("Setup")]
  [SerializeField] private EmotionType emotionTypeMock;

  [Header("Setup")]
  [SerializeField] private GameSetting setting;
  [SerializeField] private EmotionTypeDifficulty[] emotionTypeDifficulty;

  [SerializeField] private IndicatorUI indicator;
  [SerializeField] private EmotionSpawnerUI spawner;
  [SerializeField] private EmotionScoreBar scoreBar;

  [Header("Setup Param")]
  [SerializeField] private string path;

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

  [SerializeField] private Image Image1;
  [SerializeField] private Image Image2;
  [SerializeField] private Image Image3;
  [SerializeField] private Image Image4;

  private readonly EmotionTurn turn = new();
  private IconProvider _iconProvider;

  private EmotionTypeDifficulty _typeDifficulty;
  private EmotionDifficulty _difficulty;
  private int score;

  private EmotionType _inputEmotion;
  private EmotionType _outputEmotion;

  public Action<int, EmotionType> OnComplete;
  public Action OnFail;

  void Start()
  {
    _iconProvider = new IconProvider(path);

    button1.onClick.AddListener(ChoiseA);
    button2.onClick.AddListener(ChoiseB);
    button3.onClick.AddListener(ChoiseC);
    button4.onClick.AddListener(ChoiseD);

    textMesh.text = $"Score : {score}";

    turn.SetEdgePadding(setting.EdgePadding);

    indicator.OnPress += OnIndicatorPress;


    Setup(emotionTypeMock);
  }

  public void Setup(EmotionType emotionInput)
  {
    _inputEmotion = emotionInput;
    _typeDifficulty = GettypeDifficulty(emotionInput);

    Image1.sprite = _iconProvider.Get(_typeDifficulty.Easy.EmotionType);
    Image2.sprite = _iconProvider.Get(_typeDifficulty.Normal.EmotionType);
    Image3.sprite = _iconProvider.Get(_typeDifficulty.Hard.EmotionType);
    Image4.sprite = _iconProvider.Get(_typeDifficulty.Chaos.EmotionType);

    ChoiseA();
  }

  private void SelectDifficulty(EmotionDifficulty difficulty)
  {
    turn.Clear();
    spawner.ClearAll();

    _difficulty = difficulty;

    var range = indicator.GetBarRange01();
    turn.SetBarRange(range.min, range.max);
    turn.Setup(_difficulty.InitialItems);

    spawner.SpawnAll(turn.EmotionItems);

    score = 0;

    scoreBar.Setup(_difficulty);
    scoreBar.SetScore(score);

  }
  private void ChoiseA()
  {
    _outputEmotion = _typeDifficulty.Easy.EmotionType;
    SelectDifficulty(_typeDifficulty.Easy.Difficulty);
  }
  private void ChoiseB()
  {
    _outputEmotion = _typeDifficulty.Normal.EmotionType;
    SelectDifficulty(_typeDifficulty.Normal.Difficulty);
  }
  private void ChoiseC()
  {
    _outputEmotion = _typeDifficulty.Hard.EmotionType;
    SelectDifficulty(_typeDifficulty.Hard.Difficulty);
  }
  private void ChoiseD()
  {
    _outputEmotion = _typeDifficulty.Chaos.EmotionType;
    SelectDifficulty(_typeDifficulty.Chaos.Difficulty);
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

    scoreBar.SetScore(score);

    if (score >= _difficulty.RequestScore)
    {
      OnComplete?.Invoke(score, _outputEmotion);
    }
    else if (score <= _difficulty.MissPenalty)
    {
      OnFail?.Invoke();

    }
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
