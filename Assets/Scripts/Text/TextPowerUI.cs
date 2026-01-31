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

  private void Start()
  {
    targetUI.SetActive(false);
    isRunning = false;
    isTextFinish = false;

    button.onClick.AddListener(LeaveUI);
  }
  
  public void ActivaUI(int _, EmotionType from, EmotionType to)
  {
    GameStateManager.Set(GameState.TextPowerUI);
    
    targetUI.SetActive(true);

    isRunning = true;
    isTextFinish = false;

    Debug.Log(from.ToString() + " : " +  to.ToString());
    
    var text = database.Get(from, to).Get();
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
    
    GameStateManager.Set(GameState.Gameplay);
  }
}