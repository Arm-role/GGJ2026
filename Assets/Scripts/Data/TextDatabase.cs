using UnityEngine;

[CreateAssetMenu(
  fileName = "TextDatabase",
  menuName = "Localization/Text Database"
)]
public class TextDatabase : ScriptableObject
{
  [SerializeField] private TextTemplate[] texts;

  public TextTemplate Get(EmotionType from, EmotionType to)
  {
    foreach (var t in texts)
    {
      if (t.IsMatch(from, to))
        return t;
    }

    Debug.LogError($"TextTemplate not found: {name}");
    return null;
  }
}