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
    emotionController.OnEmotionChanged += PlayEmotionResult;
  }

  private void OnDestroy()
  {
    emotionController.OnEmotionChanged -= PlayEmotionResult;
  }

  public void PlayEmotionResult(EmotionContext ctx)
  {
    StartCoroutine(CutsceneRoutine(ctx));
  }

  private IEnumerator CutsceneRoutine(EmotionContext ctx)
  {
    GameStateManager.Set(GameState.Cutscene);
    cameraController.SetState(CameraState.ZoomIn);
    cameraController.ZoomTo(3f);

    Debug.Log("Cutscene Start");
    yield return new WaitForSeconds(cutsceneDuration);

    if (ctx.Result == EmotionTypeResutl.Success)
      ctx.Target.SetEmotionType(ctx.To);

    cameraController.SetState(CameraState.Follow);
    cameraController.ResetZoom();
    
    Debug.Log("Cutscene End");
    textPowerUI.ActivaUI(ctx);
  }
}