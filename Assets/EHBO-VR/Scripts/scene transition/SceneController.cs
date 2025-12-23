using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private float SceneFadeDuration;

    private SceneFade sceneFade;

    private void Awake()
    {
        sceneFade = GetComponentInChildren<SceneFade>();

    }

    private IEnumerator Start()
    {
        yield return sceneFade.FadeInCoroutine(SceneFadeDuration);
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return sceneFade.FadeOutCoroutine(SceneFadeDuration);
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
