using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionHandler : MonoBehaviour
{
    private int currentIndex;
    private float transitionDuration;
    private bool isFading = false;
    private float elapsedTime = 0;
    private Image fader;
    private float start = 1, end = 0;

    private void OnEnable() {
        Killzone.OnPlayerDeath += ResetScene;
        WinCondition.OnPlayerWin += NextScene;
    }

    private void OnDisable() {
        Killzone.OnPlayerDeath -= ResetScene;
        WinCondition.OnPlayerWin -= NextScene;
    }

    private void Awake() {
        fader = GetComponent<Image>();
    }

    private void Start() {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        transitionDuration = GameManager.instance.transitionDuration;
        Transition(-1);
    }

    private void Update() {
        if (isFading == true) {
            Fade();
        }
    }

    private void Fade() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < transitionDuration) {
            float lerpPercentage = elapsedTime / transitionDuration;
            float alpha = Mathf.Lerp(start, end, lerpPercentage);
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, alpha);
        }
        else {
            if (end == 0 && fader.enabled == true) {
                fader.enabled = false;
            }

            elapsedTime = 0;
            float startRef = start;
            start = end;
            end = startRef;
            isFading = false;
        }
    }

    private void Transition(int direction) {
        if (fader.enabled == false) {
            fader.enabled = true;
        }

        int totalScenes = SceneManager.sceneCountInBuildSettings;
        int targetScene = SceneManager.GetActiveScene().buildIndex + direction;
        if (totalScenes <= targetScene) {
            targetScene = 0;
        }

        if (direction != -1) {
            StartCoroutine(LoadSceneAferDelay(targetScene, transitionDuration));
        }

        isFading = true;
    }

    private IEnumerator LoadSceneAferDelay(int scene, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    public void NextScene() {
        Transition(1);
    }

    private void ResetScene() {
        Transition(0);
    }
}
