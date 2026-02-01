using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  public static SceneController instance;

  [SerializeField] private Animator transitionAnim;
  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.W))
    {
      NextLevel();
    }
  }
  public void NextLevel()
  {
    StartCoroutine(LoadLevel());
  }

  IEnumerator LoadLevel()
  {
    transitionAnim.SetTrigger("End");
    yield return new WaitForSeconds(1f);
    //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    transitionAnim.SetTrigger("Start");
  }
}
