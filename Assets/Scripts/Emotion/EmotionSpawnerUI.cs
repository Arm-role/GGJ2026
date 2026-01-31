using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmotionSpawnerUI : MonoBehaviour
{
  [SerializeField] private EmotionZoneUI prefab;
  [SerializeField] private SliderBar bar;
  [SerializeField] private Transform root;

  private readonly Dictionary<EmotionRuntimeItem, EmotionZoneUI> map = new();

  public void SpawnAll(IEnumerable<EmotionRuntimeItem> items)
  {
    Clear();

    foreach (var item in items)
      Spawn(item);
  }

  public void ClearAll()
  {
    foreach (Transform c in transform)
      Destroy(c.gameObject);
  }

  public void Spawn(EmotionRuntimeItem item)
  {
    var view = Instantiate(prefab, root);
    view.Setup(item, bar);
    map[item] = view;
  }

  public void Remove(EmotionRuntimeItem item)
  {
    if (!map.TryGetValue(item, out var view))
      return;

    Object.Destroy(view.gameObject);
    map.Remove(item);
  }

  public void Clear()
  {
    foreach (var v in map.Values)
      Object.Destroy(v.gameObject);

    map.Clear();
  }
}