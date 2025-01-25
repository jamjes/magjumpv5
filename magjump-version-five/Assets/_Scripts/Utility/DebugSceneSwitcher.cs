using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneSwitcher : MonoBehaviour
{
    public string levelOne, levelTwo, levelThree, levelFour, levelFive;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene(levelOne);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene(levelTwo);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SceneManager.LoadScene(levelThree);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SceneManager.LoadScene(levelFour);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SceneManager.LoadScene(levelFive);
        }
    }
}
