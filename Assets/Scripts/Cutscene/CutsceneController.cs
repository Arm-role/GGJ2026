using System.Collections;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
  [SerializeField] private TextPowerUI textPowerUI;
  [SerializeField] private float cutsceneDuration = 1.5f;
  [SerializeField] private EmotionController emotionController;
  [SerializeField] private CameraController cameraController;

  private void Start()
  {
    emotionController.OnComplete += PlayEmotionResult;
  }

  private void OnDestroy()
  {
    emotionController.OnComplete -= PlayEmotionResult;
  }

  public void PlayEmotionResult(
    int score,
    EmotionType from,
    EmotionType to
  )
  {
    StartCoroutine(CutsceneRoutine(score, from, to));
  }

  private IEnumerator CutsceneRoutine(
    int score,
    EmotionType from,
    EmotionType to
  )
  {
    GameStateManager.Set(GameState.Cutscene);
    cameraController.SetState(CameraState.ZoomIn);
    cameraController.ZoomTo(3f);
    
    Debug.Log("Cutscene Start");
    yield return new WaitForSeconds(cutsceneDuration);

    Debug.Log("Cutscene End");
    cameraController.SetState(CameraState.Follow);
    cameraController.ResetZoom();
    textPowerUI.ActivaUI(score, from, to);
  }
}