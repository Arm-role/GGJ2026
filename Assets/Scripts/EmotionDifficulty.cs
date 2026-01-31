
using UnityEngine;

[CreateAssetMenu(
  fileName = "EmotionDifficulty",
  menuName = "Emotion/Difficulty"
)]
public class EmotionDifficulty : ScriptableObject
{
  [Header("Spawn Items")]
  [SerializeField] private EmotionItem[] initialItems;

  [Header("Respawn Pool")]
  [SerializeField] private EmotionItem[] respawnPool;

  [Header("Rules")]
  [SerializeField] private int missPenalty = -10;
  [SerializeField] private int requestScore;

  public EmotionItem[] InitialItems => initialItems;
  public EmotionItem[] RespawnPool => respawnPool;
  public int MissPenalty => missPenalty;
  public int RequestScore => requestScore;

  public EmotionItem GetRandomRespawn()
  {
    if (respawnPool == null || respawnPool.Length == 0)
      return null;

    return respawnPool[Random.Range(0, respawnPool.Length)];
  }
}
