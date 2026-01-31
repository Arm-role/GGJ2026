using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterText : MonoBehaviour
{
  [Header("Reference")]
  [SerializeField] private TextMeshProUGUI textUIEng;
  [SerializeField] private TextMeshProUGUI textUIThai;
  
  [Header("Settings")]
  [SerializeField] private float charDelay = 0.04f;

  private Coroutine typingRoutine;
  private string fullTextEng;
  private string fullTextThai;
  
  private bool isTyping;

  public bool  IsTyping => isTyping;
  public void Play(LanguageText text)
  {
    StopTyping();

    fullTextEng = text.English;
    fullTextThai = text.Thai;
    
    typingRoutine = StartCoroutine(TypeText());
  }

  public void Skip()
  {
    if (!isTyping)
      return;

    StopTyping();
    
    textUIEng.text = fullTextEng;
    textUIThai.text = fullTextThai;
    
    textUIEng.maxVisibleCharacters = fullTextEng.Length;
    textUIThai.maxVisibleCharacters = fullTextThai.Length;
  }

  private void StopTyping()
  {
    if (typingRoutine != null)
    {
      StopCoroutine(typingRoutine);
      typingRoutine = null;
    }

    isTyping = false;
  }

  private IEnumerator TypeText()
  {
    isTyping = true;
    textUIEng.text = fullTextEng;
    textUIThai.text = fullTextThai;

    textUIEng.maxVisibleCharacters = 0;
    textUIThai.maxVisibleCharacters = 0;

    int maxLength = Mathf.Max(
      fullTextEng.Length,
      fullTextThai.Length
    );

    for (int i = 0; i <= maxLength; i++)
    {
      textUIEng.maxVisibleCharacters = i;
      textUIThai.maxVisibleCharacters = i;

      yield return new WaitForSeconds(charDelay);
    }

    isTyping = false;
    typingRoutine = null;
  }
}