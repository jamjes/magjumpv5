using System.Threading;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem landParticles;
    [SerializeField] private ParticleSystem magnetiseParticles;
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    bool timer = false;
    float elapsedTime = 0;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (rb.linearVelocityY > 0 && timer == false) {
            timer = true;
            elapsedTime = 0;
        }

        if (timer) {
            elapsedTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 6) {
            return;
        }

        timer = false;

        if (elapsedTime < .5f) {
            return;
        }

        RaycastHit2D groundCheck = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, groundLayer);
        RaycastHit2D ceilingCheck = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.up, .1f, groundLayer);

        if (groundCheck.collider != null) {
            landParticles.Play();
        } else if (ceilingCheck.collider != null) {
            magnetiseParticles.Play();
        }

    }
}
