using System.Collections;
using System.Transactions;
using UnityEngine;

public class PlayerSimple : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    bool grounded;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private float direction;
    [SerializeField] private float xSpeed, yForce;
    bool isApex;
    bool isJumpBuffer;
    float elapsedTime;
    [SerializeField] float bufferTime;
    bool canMagnetise;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        grounded = GroundCheck();

        if (grounded && isApex == true) {
            isApex = false;
        } else if (isJumpBuffer && grounded && elapsedTime <= bufferTime) {
            isJumpBuffer = false;
            Jump();
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
        else if (!grounded && Input.GetKeyDown(KeyCode.Space)) {
            StartJumpBuffer();
        }
        
        if (Input.GetKey(KeyCode.LeftArrow)) {
            direction = -1;
        } else if (Input.GetKey(KeyCode.RightArrow)) { 
            direction = 1; 
        } else {
            direction = 0;
        }

        if ((rb.linearVelocityY < 0.25f && rb.linearVelocityY > -0.25f) && grounded == false) {
            if (isApex != true) {
                StartCoroutine(Apex());
            }
        }

        if (isJumpBuffer == true) {
            elapsedTime += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && canMagnetise == true) {
            if (rb.gravityScale != 0) {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.gravityScale == 0) {
            rb.gravityScale = 1;
        } else if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocityY > 0) {
            rb.linearVelocityY = -.3f;
        }
    }

    private bool GroundCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, groundLayer);
        return hit.collider != null;
    }

    private void Jump() {
        float jumpForce = yForce;
        if (direction != 0) {
            jumpForce = yForce * .95f;
        }
        rb.linearVelocity = new Vector2(direction * xSpeed, jumpForce);
    }


    private IEnumerator Apex() {
        isApex = true;
        float originalSpeed = xSpeed;
        xSpeed = originalSpeed * 1.125f;
        rb.gravityScale = 0;
        rb.linearVelocityY = 0;
        yield return new WaitForSeconds(.25f);
        rb.gravityScale = 1;
        xSpeed = originalSpeed;
    }

    private void StartJumpBuffer() {
        elapsedTime = 0;
        isJumpBuffer = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, .52f, groundLayer);
        if (hit.collider != null && hit.collider == collision.collider) {
            canMagnetise = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) {
            canMagnetise = false;
        }
    }
}
