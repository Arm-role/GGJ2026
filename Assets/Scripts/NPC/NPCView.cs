using System;
using UnityEngine;
using UnityEngine.UI;

public class NPCView : MonoBehaviour
{
  [SerializeField] private SpriteRenderer npcRenderer;
  [SerializeField] private GameObject interactIcon;
  [SerializeField] private Animator animator;

  private Color npcColor;

  public void ShowInteractIcon(bool show)
  {
    if (interactIcon != null)
      interactIcon.SetActive(show);
  }
  
  public void OnChange(EmotionType emotionType)
  {
    switch (emotionType)
    {
      case EmotionType.Angry : npcRenderer.color = Color.red; break;
      case EmotionType.Happy : npcRenderer.color = Color.green; break;
      case EmotionType.Confidence : npcRenderer.color = Color.magenta; break;
      case EmotionType.Fear : npcRenderer.color = Color.yellow; break;
      case EmotionType.Sadness : npcRenderer.color = Color.blue; break;
    }
    
    npcColor =  npcRenderer.color;
  }
}