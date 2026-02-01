using System;
using UnityEngine;
using UnityEngine.UI;

public class TextPowerUI : MonoBehaviour
{
  [SerializeField] private TextDatabase database;

  [SerializeField] private TypewriterText typewriterText;
  [SerializeField] private GameObject targetUI;
  [SerializeField] private Button button;

  private bool isRunning;
  private bool isTextFinish;

  private EmotionContext target;

  private void Start()
  {
    targetUI.SetActive(false);
    isRunning = false;
    isTextFinish = false;

    button.onClick.AddListener(LeaveUI);
  }

  public void ActivaUI(EmotionContext ctx)
  {
    GameStateManager.Set(GameState.TextPowerUI);

    targetUI.SetActive(true);

    isRunning = true;
    isTextFinish = false;
    target = ctx;

    var text = database.Get(ctx.From, ctx.To).Get();
    typewriterText.Play(text);
  }

  private void Update()
  {
    if (GameStateManager.Current != GameState.TextPowerUI)
      return;

    if (!isRunning)
      return;

    if (Input.GetKeyDown(KeyCode.Space))
    {
      HandleSpaceInput();
    }
  }

  private void HandleSpaceInput()
  {
    Debug.Log("HandleSpaceInput");
    if (!isTextFinish)
    {
      typewriterText.Skip();
      isTextFinish = true;
      return;
    }

    LeaveUI();
  }

  private void LeaveUI()
  {
    isRunning = false;
    isTextFinish = false;

    targetUI.SetActive(false);
    
    target.Target.OnUnInteraction();
    GameStateManager.Set(GameState.Gameplay);
  }
}