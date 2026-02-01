using UnityEngine;

public class NPC_Controller : MonoBehaviour, ITargetable
{
  [SerializeField] private EmotionType emotionType;
  [SerializeField] private NPCView npcView;
  [SerializeField] private NPC_Movement npcMovement;

  private NPCState currentState;

  private void Start()
  {
    npcView.OnChange(emotionType);
    ChangeState(NPCState.Patrol);
  }

  public void OnInteraction()
  {
    ChangeState(NPCState.Interacting);
  }

  public void OnUnInteraction()
  {
    if (currentState == NPCState.Interacting)
      ChangeState(NPCState.Patrol);
  }

  public void OnFocus()
  {
    if (currentState == NPCState.Patrol)
      ChangeState(NPCState.Focused);
  }

  public void OnUnfocus()
  {
    if (currentState == NPCState.Focused)
      ChangeState(NPCState.Patrol);
  }

  private void ChangeState(NPCState newState)
  {
    currentState = newState;

    switch (currentState)
    {
      case NPCState.Patrol:
        npcMovement.enabled = true;
        npcView.ShowInteractIcon(false);
        break;

      case NPCState.Focused:
        npcMovement.enabled = false;
        npcView.ShowInteractIcon(true);
        break;

      case NPCState.Interacting:
        npcMovement.enabled = false;
        npcView.ShowInteractIcon(false);
        break;
    }
  }

  public EmotionType GetEmotionType => emotionType;

  public void SetEmotionType(EmotionType emotionType)
  {
    Debug.Log("Change To : " + emotionType);
    this.emotionType = emotionType;
    npcView.OnChange(emotionType);
  }
}