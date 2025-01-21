using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            StartCoroutine(DelayScene(SceneManager.GetActiveScene().buildIndex));
        }
    }

    private IEnumerator DelayScene(int sceneIndex) {
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene(sceneIndex);
    }
}


