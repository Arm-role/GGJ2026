using UnityEngine;

[CreateAssetMenu(menuName = "Game/Ending Narrative")]
public class EndingNarrativeSO : ScriptableObject
{
  public EndingType endingType;
  public LanguageText message;
}