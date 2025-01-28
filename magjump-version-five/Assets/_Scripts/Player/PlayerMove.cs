using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {
    [Header("Dependencies")]
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] CustomPointer pointer;

    [Header("Player Physics")]
    [SerializeField] private float power;
    [SerializeField] private float strength;
    private bool isInApex;
    [SerializeField] private float apexDuration;

    [Header("Player Collision")]
    [SerializeField] private LayerMask platformLayer;
    private Vector2 impulseDirection;
    private float lastGroundedTime;
    private bool canMagnetise;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        bool canImpulse = CanImpulse();

        if (canImpulse) {
            pointer.SetColor(Color.green);
        } else {
            pointer.SetColor(Color.white);
        }
        
        if (Input.GetKeyDown(KeyCode.X) && canImpulse) {
            Impulse();
        }

        RaycastHit2D ceilingCollision = PlatformCollisionCheck(Vector2.up, .25f);

        if (Input.GetKey(KeyCode.X)){
            canMagnetise = true;
        } else {
            canMagnetise = false;
        }

        if (Input.GetKeyUp(KeyCode.X) && rb.linearVelocityY != 0) {
            CancelImpulse();
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        RaycastHit2D floorCollision = PlatformCollisionCheck(Vector2.down, .25f);

        if (floorCollision.collider != null) {
            lastGroundedTime = Time.time;
            
            if (isInApex == true) {
                isInApex = false;
            }
        }

        if (floorCollision.collider == null) {
            if (rb.linearVelocityY > -1f && rb.linearVelocityY < 1f) {
                ApexEffector();
            }
        }
    }

    private void Impulse() {
        impulseDirection = ClampDirection(pointer.GetAngle());
        rb.linearVelocity = power * impulseDirection * GetImpulseMode();
    }

    private int GetImpulseMode() {
        if (pointer.transform.position.y > transform.position.y) {
            return 1;
        } else {
            return -1;
        }
    }

    private void CancelImpulse() {
        if (rb.linearVelocityY < 0) {
            return;
        }
        rb.linearVelocityY = 0;
    }

    private bool CanImpulse() {
        impulseDirection = pointer.GetNormalDirection();
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, impulseDirection, strength, platformLayer);
        return hit.collider != null;
    }

    private void ApexEffector() {
        float timeDifference = Time.time - lastGroundedTime;
        
        if (isInApex || timeDifference < .3f) {
            return;
        }

        isInApex = true;
        StartCoroutine(Apex());
    }

    private IEnumerator Apex() {
        rb.gravityScale = 0;
        rb.linearVelocityY = 0;
        yield return new WaitForSeconds(apexDuration);
        rb.gravityScale = 1;
    }

    private RaycastHit2D PlatformCollisionCheck(Vector2 direction, float distance) {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, direction, distance, platformLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) {
            transform.SetParent(collision.transform);
        }
    }

    private Vector2 ClampDirection(float angle) {
        Vector2 direction = Vector2.zero;

        if (angle <= 20 && angle >= -20) direction = new Vector2(1, 0);
        else if (angle < 70 && angle > 20) direction = new Vector2(.5f, .9f);
        else if (angle <= 110 && angle >= 70) direction = new Vector2(0, 1);
        else if (angle < 160 && angle > 110) direction = new Vector2(-.5f, .9f);
        else if (angle <= -160 || angle >= 160) direction = new Vector2(-1, 0);
        else if (angle < -110 && angle > -160) direction = new Vector2(-.5f, -.9f);
        else if (angle <= -70 && angle >= -110) direction = new Vector2(0, -1);
        else if (angle <= -20 && angle >= -70) direction = new Vector2(.5f, -.9f);

        return direction;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer == platformLayer) {
            transform.SetParent(null);
        }
    }
}
