using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private Rigidbody2D rbd;
    [SerializeField] private Magnet pointer;
    private float force = 15f;
    private bool isInApex;
    private float fallGravity = 1.5f;

    private PlayerCollisionHandler _collisions;

    private void Awake() {
        rbd = GetComponent<Rigidbody2D>();
        _collisions = GetComponent<PlayerCollisionHandler>();
    }

    private void Update() {
        Vector2 direction = (pointer.transform.position - transform.position).normalized;
        RaycastHit2D grounded = _collisions.BoxCollisionCheck(Vector2.down);
        bool isGrounded = _collisions.IsGround();

        if (Input.GetKeyDown(KeyCode.X)) {
            Impulse(direction, grounded);
        }

        if (Input.GetKey(KeyCode.X) && _collisions.CanMagnetise) {
            if (rbd.gravityScale != 0) {
                rbd.gravityScale = 0;
                rbd.linearVelocity = Vector3.zero;
            }
        }

        if (Input.GetKeyUp(KeyCode.X) && rbd.gravityScale != 1) {
            rbd.gravityScale = 1;
            _collisions.SetMagnetise(false);
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

        RaycastHit2D target = _collisions.RayCheck(direction, 3);

        if (grounded.collider != null && direction.y < 0) {
            targetVelocity = new Vector2(direction.x * force * -1, force);
        }
        else if (target.collider != null && direction.y > 0) {
            targetVelocity = new Vector2(direction.x * force, force);
            if (isInApex == true) {
                isInApex = false;
            }
        }

        if (targetVelocity != Vector2.zero) {
            rbd.linearVelocity = targetVelocity;
        }
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
        if (isInApex == true) {
            rbd.linearVelocityY = -3;
            isInApex = false;
        }
    }
}