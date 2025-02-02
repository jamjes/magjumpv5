using UnityEngine;

public class Killzone : MonoBehaviour
{
    public delegate void CollisionEvent();
    public static event CollisionEvent OnPlayerDeath;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            if (OnPlayerDeath == null) {
                return;
            }

            OnPlayerDeath();
        }
    }
}


