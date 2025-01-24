using UnityEngine;

public class Killzone : MonoBehaviour
{
    public delegate void CollisionEvent();
    public static event CollisionEvent OnPlayerDeath;

    private AudioSource src;

    private void Awake() {
        src = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            Debug.Log("Kill Player");
            if (OnPlayerDeath == null) {
                return;
            }

            OnPlayerDeath();
            src.Play();
        }
    }
}


