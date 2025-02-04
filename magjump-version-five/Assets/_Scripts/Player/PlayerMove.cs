using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private BoxCollider2D coll;
    private Rigidbody2D rbd;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Magnet pointer;
    private float force = 15f;
    private float detectionLength = 3f;
    private bool canMagnetise;
    private bool isInApex;
    private float fallGravity = 1.5f;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
        rbd = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 direction = (pointer.transform.position - transform.position).normalized;
        RaycastHit2D grounded = GroundCheck();
        bool isGrounded = grounded.collider != null ? true : false;

        if (Input.GetKeyDown(KeyCode.X)) {
            Impulse(direction, grounded);
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

        if (rbd.linearVelocityY > -2 && rbd.linearVelocityY < 2
            && isGrounded == false) {
            if (isInApex != true) {
                StartCoroutine(ApexModifier());
            }
        }

        if (isGrounded && rbd.gravityScale != 1) {
            rbd.gravityScale = 1;
        }

    }

    private void Impulse(Vector2 direction, RaycastHit2D grounded) {
        Vector2 targetVelocity = Vector2.zero;
        
        if (pointer.CanImpulse == false) {
            return;
        }

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

    private IEnumerator ApexModifier() {
        isInApex = true;
        rbd.gravityScale = 0;
        rbd.linearVelocityY = 0;
        float xForce = rbd.linearVelocityX;
        rbd.linearVelocityX = xForce * 1.125f;
        yield return new WaitForSeconds(.2f);
        rbd.linearVelocityX = xForce;
        rbd.gravityScale = fallGravity;
        rbd.linearVelocityY = -3;
        isInApex = false;
    }
}