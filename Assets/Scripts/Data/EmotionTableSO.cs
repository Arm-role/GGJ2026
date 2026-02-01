using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Emotion Table")]
public class EmotionTableSO : ScriptableObject
{
  public List<EmotionTransitionSO> transitions;

  private Dictionary<(EmotionType, EmotionType), EmotionTransitionSO> lookup;

  public void Init()
  {
    lookup = new Dictionary<(EmotionType, EmotionType), EmotionTransitionSO>();

    foreach (var t in transitions)
    {
      var key = (t.from, t.to);
      if (!lookup.ContainsKey(key))
        lookup.Add(key, t);
    }
  }

  public EmotionTransitionSO Get(EmotionType from, EmotionType to)
  {
    if (lookup == null) Init();

    if (lookup.TryGetValue((from, to), out var result))
      return result;

    return null;
  }
}