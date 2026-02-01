using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterText2 : MonoBehaviour
{
  [Header("Targets")]
  [SerializeField] private TextMeshProUGUI[] targets;

  [Header("Settings")]
  [SerializeField] private float charDelay = 0.04f;

  private Coroutine typingRoutine;
  private string[] fullTexts;
  private bool isTyping;

  public bool IsTyping => isTyping;

  // ใช้กับ text เดียว
  public void Play(string text)
  {
    Play(new[] { text });
  }

  // ใช้กับหลายภาษา / หลาย UI
  public void Play(params string[] texts)
  {
    StopTyping();

    fullTexts = texts;
    typingRoutine = StartCoroutine(TypeText());
  }

  public void Skip()
  {
    if (!isTyping) return;

    StopTyping();

    for (int i = 0; i < targets.Length; i++)
    {
      targets[i].text = fullTexts[i];
      targets[i].maxVisibleCharacters = fullTexts[i].Length;
    }
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

    int maxLength = 0;

    for (int i = 0; i < targets.Length; i++)
    {
      targets[i].text = fullTexts[i];
      targets[i].maxVisibleCharacters = 0;
      maxLength = Mathf.Max(maxLength, fullTexts[i].Length);
    }

    for (int i = 0; i <= maxLength; i++)
    {
      foreach (var t in targets)
        t.maxVisibleCharacters = i;

      yield return new WaitForSeconds(charDelay);
    }

    isTyping = false;
    typingRoutine = null;
  }
}