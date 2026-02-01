using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Ending Table")]
public class EndingTableSO : ScriptableObject
{
  [SerializeField] private List<EndingNarrativeSO> endings;

  private Dictionary<EndingType, EndingNarrativeSO> lookup;

  public void Init()
  {
    lookup = new Dictionary<EndingType, EndingNarrativeSO>();
    foreach (var e in endings)
    {
      if (!lookup.ContainsKey(e.endingType))
        lookup.Add(e.endingType, e);
    }
  }

  public EndingNarrativeSO Get(EndingType type)
  {
    if (lookup == null) Init();
    lookup.TryGetValue(type, out var result);
    return result;
  }
}