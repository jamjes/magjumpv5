using UnityEngine;

public class Launchpad : MonoBehaviour
{
    private BoxCollider2D coll;
    private float impulseForce = 15f;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            Debug.Log("Player Detected");
            bool valid = Check();
            if (valid) {
                Rigidbody2D player = collision.collider.GetComponent<Rigidbody2D>();
                player.linearVelocity = Vector3.zero;
                player.linearVelocityY += impulseForce;
            }
        }
    }

    private bool Check() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(coll.bounds.center, coll.bounds.size, 0, Vector2.up, .25f);
        if (hits == null) {
            return false;
        } else {
            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.tag == "Player") {
                    return true;
                }
            }

            return false;
        }
    }
}
