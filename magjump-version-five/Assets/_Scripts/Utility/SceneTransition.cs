using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image overlay;
    float elapsedTime;
    float duration = .75f;
    Color startColor, endColor;
    public bool run;
    bool fadeIn;

    private void OnEnable() {
        WinCondition.OnPlayerWin += NextScene;
        UIButton.OnNext += NextScene;
        UIButton.OnReset += ResetGame;
        UIButton.OnQuit += QuitGame;
    }

    private void OnDisable() {
        WinCondition.OnPlayerWin -= NextScene;
        UIButton.OnNext -= NextScene;
        UIButton.OnReset -= ResetGame;
        UIButton.OnQuit -= QuitGame;
    }

    private void Start() {
        overlay.enabled = true;
        FadeIn();
    }

    private void Update() {
        if (run == true) {
            elapsedTime += Time.deltaTime;
            float progress = Fade(elapsedTime);
            if (progress == 1) {
                run = false;
                elapsedTime = 0;
                if (fadeIn) {
                    fadeIn = false;
                    overlay.gameObject.SetActive(false);
                }
            }
        }
    }

    private float Fade(float elapsedTime) {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;
        if (percentageComplete > 1) {
            percentageComplete = 1;
        }

        overlay.color = Color.Lerp(startColor, endColor, percentageComplete);
        return percentageComplete;
    }

    private void FadeIn() {
        startColor = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        endColor = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        run = true;
        fadeIn = true;
    }

    private void FadeOut() {
        startColor = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        endColor = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        run = true;
        overlay.gameObject.SetActive(true);
    }

    private void NextScene() {
        StartCoroutine(FadeToScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator FadeToScene(int index) {
        FadeOut();
        yield return new WaitForSeconds(duration);
        if (index == -1) {
            Application.Quit();
        } else {
            SceneManager.LoadScene(index);
        }
    }

    private void ResetGame() {
        StartCoroutine(FadeToScene(0));
    }

    private void QuitGame() {
        StartCoroutine(FadeToScene(-1));
    }
}
