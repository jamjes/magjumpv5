using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private KeyCode trigger;

    private void Update() {
        if (Input.GetKeyDown(trigger)) {
            int index = SceneManager.GetActiveScene().buildIndex + 1;
            if (index != SceneManager.sceneCountInBuildSettings) {
                SceneManager.LoadScene(index);
            } else {
                SceneManager.LoadScene(0);
            }
        }
    }
}
