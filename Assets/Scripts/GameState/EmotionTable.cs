using System.Collections.Generic;

public static class EmotionTable
{
  private static Dictionary<(EmotionType, EmotionType), EmotionTransitionData> table;

  static EmotionTable()
  {
    table = new Dictionary<(EmotionType, EmotionType), EmotionTransitionData>
    {
      { (EmotionType.Angry, EmotionType.Happy),
        new EmotionTransitionData {
          from = EmotionType.Angry,
          to = EmotionType.Happy,
          difficulty = 1.5f,
          humanity = 1.2f
        }
      },

      { (EmotionType.Sadness, EmotionType.Confidence),
        new EmotionTransitionData {
          from = EmotionType.Sadness,
          to = EmotionType.Confidence,
          difficulty = 1.3f,
          humanity = 1.0f
        }
      },

      { (EmotionType.Fear, EmotionType.Confidence),
        new EmotionTransitionData {
          from = EmotionType.Fear,
          to = EmotionType.Confidence,
          difficulty = 1.4f,
          humanity = 1.1f
        }
      }
    };
  }

  public static EmotionTransitionData Get(EmotionType from, EmotionType to)
  {
    if (table.TryGetValue((from, to), out var data))
      return data;

    // default fallback
    return new EmotionTransitionData
    {
      from = from,
      to = to,
      difficulty = from == to ? 0.2f : 1f,
      humanity = 0f
    };
  }
}