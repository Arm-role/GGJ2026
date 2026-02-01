using System;

[Serializable]
public class GameSessionData
{
  public int totalInteractions;
  public int successCount;
  public int failureCount;
  public int totalScore;

  public float difficultyScore;   // ความยากสะสม
  public float humanityScore;     // ผลต่อมนุษย์

  public float SuccessRate =>
    totalInteractions == 0 ? 0 :
      (float)successCount / totalInteractions;
}