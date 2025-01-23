using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int deathCount = 0;

    private void OnEnable() {
        Killzone.OnPlayerDeath += ResetAfterDeath;
    }

    private void OnDisable() {
        Killzone.OnPlayerDeath -= ResetAfterDeath;
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void ResetAfterDeath() {
        deathCount++;
        //int index = SceneManager.GetActiveScene().buildIndex;
        //StartCoroutine(LoadSceneAfter(index, .75f));
    }

    private IEnumerator LoadSceneAfter(int index, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
}
