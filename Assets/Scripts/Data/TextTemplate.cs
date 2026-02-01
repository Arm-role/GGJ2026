using System;
using UnityEngine;

[CreateAssetMenu(
  fileName = "TextTemplate",
  menuName = "Localization/Text Template"
)]
public class TextTemplate : ScriptableObject
{
  [SerializeField] private EmotionType from;
  [SerializeField] private EmotionType to;

  [TextArea(2, 5)] [SerializeField] private string english;

  [TextArea(2, 5)] [SerializeField] private string thai;

  public bool IsMatch(EmotionType f, EmotionType t) => f == from && t == to;

  public LanguageText Get()
  {
    return new LanguageText(english, thai);
  }
}

[Serializable]
public class LanguageText
{
  [TextArea(2, 5)] public string English;
  [TextArea(2, 5)] public string Thai;

  public LanguageText(string english, string thai)
  {
    English = english;
    Thai = thai;
  }
}