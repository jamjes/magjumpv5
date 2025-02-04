using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem landParticles;
    [SerializeField] private ParticleSystem magnetiseParticles;
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D coll;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 6) {
            return;
        }

        RaycastHit2D groundCheck = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, groundLayer);
        RaycastHit2D ceilingCheck = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.up, .1f, groundLayer);

        if (groundCheck.collider != null) {
            landParticles.Play();
            Camera.main.GetComponent<Shake>().Trigger();
        } else if (ceilingCheck.collider != null) {
            magnetiseParticles.Play();
        }

    }
}
