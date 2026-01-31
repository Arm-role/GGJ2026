using System.Collections.Generic;
using UnityEngine;

public class EmotionTurn
{
  private readonly List<EmotionRuntimeItem> emotionItems = new();

  private const int MAX_TRY = 30;

  private float edgePadding;

  private float barMin = 0f;
  private float barMax = 1f;

  public IReadOnlyList<EmotionRuntimeItem> EmotionItems => emotionItems;

  /* ---------- SETUP ---------- */

  public void Setup(IEnumerable<EmotionItem> levelItems)
  {
    emotionItems.Clear();

    var range = GetEmotionSpawnRange();

    //SetAceptDeal(acept);

    foreach (var item in levelItems)
      TrySpawn(item, range.min, range.max);

  }

  public void SetEdgePadding(float value)
  {
    edgePadding = Mathf.Clamp01(value);
  }

  public void Clear()
  {
    emotionItems.Clear();
  }

  public void SetBarRange(float min, float max)
  {
    barMin = min;
    barMax = max;
  }

  /* ---------- RESOLVE ---------- */

  public bool Resolve(
    float pointer,
    EmotionItem respawnItem,
    out EmotionRuntimeItem hit,
    out int score)
  {
    hit = default;
    score = 0;

    // 2️⃣ Emotion item ปกติ
    for (int i = 0; i < emotionItems.Count; i++)
    {
      if (!emotionItems[i].Contains(pointer))
        continue;

      hit = emotionItems[i];
      score = hit.Data.Value;

      emotionItems.RemoveAt(i);
      TrySpawnInEmptySpace(respawnItem);
      return true;
    }

    score = -10;
    return false;
  }


  /* ---------- SPAWN LOGIC ---------- */

  private bool TrySpawn(
    EmotionItem item,
    float min,
    float max)
  {
    float half = item.Width * 0.5f;

    for (int i = 0; i < MAX_TRY; i++)
    {
      float center = Random.Range(min + half, max - half);
      var candidate = new EmotionRuntimeItem(item, center);

      if (IsOverlappingEmotion(candidate))
        continue;
      
      emotionItems.Add(candidate);
      return true;
    }

    return false;
  }

  private void TrySpawnInEmptySpace(EmotionItem item)
  {
    Debug.Log(
      $"Respawn {item.name} width={item.Width} " +
      $"bar=({barMin},{barMax})"
    );

    foreach (var space in GetEmptySpaces())
    {
      if (space.max - space.min < item.Width)
        continue;

      if (TrySpawn(item, space.min, space.max))
        return;
    }
  }

  /* ---------- EMPTY SPACE ---------- */
  private List<(float min, float max)> GetEmptySpaces()
  {
    var spaces = new List<(float, float)>();
    float current = barMin;

    var blockers = new List<EmotionRuntimeItem>();
    blockers.AddRange(emotionItems);

    blockers.Sort((a, b) => a.Min.CompareTo(b.Min));

    foreach (var item in blockers)
    {
      if (item.Min > current)
        spaces.Add((current, item.Min));

      current = Mathf.Max(current, item.Max);
    }

    if (current < barMax)
      spaces.Add((current, barMax));

    return spaces;
  }


  private (float min, float max) GetEmotionSpawnRange()
  {
    return (barMin, barMax);
  }

  private bool IsOverlappingEmotion(EmotionRuntimeItem candidate)
  {
    foreach (var e in emotionItems)
      if (candidate.Overlaps(e))
        return true;

    return false;
  }

  public IEnumerable<(float min, float max)> Debug_GetEmptySpaces()
  {
    return GetEmptySpaces();
  }
}