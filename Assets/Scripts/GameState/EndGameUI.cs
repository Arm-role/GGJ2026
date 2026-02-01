using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
  [SerializeField] private string endPath;
  [SerializeField] private GameObject endGameUI;
  [SerializeField] private Image endImage;

  [SerializeField] private EndingTableSO endingTable;
  [SerializeField] private TypewriterText2 typewriter;

  public void Show(EndingType ending)
  {
    endGameUI.SetActive(true);
    var narrative = endingTable.Get(ending);

    var spriteProvider = new  SpriteProvider(endPath);
    endImage.sprite = spriteProvider.Get(ending.ToString());
    
    if (narrative == null)
    {
      Debug.LogWarning($"No ending narrative for {ending}");
      return;
    }

    typewriter.Play(
      narrative.message.English,
      narrative.message.Thai
    );
  }

  public void Hide()
  {
    endGameUI.SetActive(false);
  }
}