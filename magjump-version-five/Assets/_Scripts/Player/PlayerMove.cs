using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private BoxCollider2D coll;
    private Rigidbody2D rbd;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Magnet pointer;
    private float force = 15f;
    private float detectionLength = 3f;
    private bool canMagnetise;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
        rbd = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 direction = (pointer.transform.position - transform.position).normalized;

        if (Input.GetKeyDown(KeyCode.X)) {
            Impulse(direction);
        }

        if (Input.GetKey(KeyCode.X) && canMagnetise) {
            if (rbd.gravityScale != 0) {
                rbd.gravityScale = 0;
                rbd.linearVelocity = Vector3.zero;
            }
        }

        if (Input.GetKeyUp(KeyCode.X) && rbd.gravityScale != 1) {
            rbd.gravityScale = 1;
            canMagnetise = false;
        }
    }

    private void Impulse(Vector2 direction) {
        Vector2 targetVelocity = Vector2.zero;
        
        if (pointer.CanImpulse == false) {
            return;
        }

        RaycastHit2D grounded = GroundCheck();
        RaycastHit2D target = MagnetCheck(direction, detectionLength);

        if (grounded.collider != null && direction.y < 0) {
            targetVelocity = new Vector2(direction.x * force * -1, force);
        }
        else if (target.collider != null && direction.y > 0) {
            targetVelocity = new Vector2(direction.x * force, force);
        }

        if (targetVelocity != Vector2.zero) {
            rbd.linearVelocity = targetVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.up, .2f, groundLayer);
        if (hit.collider != null && collision.gameObject.layer == 6) {
            canMagnetise = true;
        }
    }

    private RaycastHit2D MagnetCheck(Vector2 direction, float magnetStrength) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, magnetStrength, groundLayer);
        return hit;
    }

    private RaycastHit2D GroundCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .2f, groundLayer);
        return hit;
    }
}