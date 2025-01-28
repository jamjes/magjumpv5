using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 objectPosition;
    private Vector2 direction;
    private float angle;
    [SerializeField] private Pivot pivot;
    [SerializeField] private LayerMask platformLayer;
    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private PlayerAttributes attributes;

    private void Awake() {
        //spr = pivot.transform.GetChild(0).GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, attributes.detection_radius, platformLayer); //Player is within range to attract / repel
        bool canImpulse;
        if (hit.collider != null) {
            pivot.SetColor(Color.green);
            //spr.color = Color.green;
            canImpulse = true;
        }
        else {
            pivot.SetColor(Color.red);
            //spr.color = Color.red;
            canImpulse = false;
        }

        if (canImpulse && Input.GetKeyDown(KeyCode.X)) {
            Vector2 impulseDirection = ClampDirection(angle);
            Impulse(impulseDirection.normalized);
        }

        bool canStick = MagnetiseCheck();

        if (canStick && Input.GetKey(KeyCode.X) && mousePosition.y > objectPosition.y) {
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
        }
        
        if (Input.GetKeyUp(KeyCode.X)) {
            rb.gravityScale = 1;
        }
    }

    private bool MagnetiseCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.up, 0, platformLayer);
        return hit.collider != null;
    }

    private void Impulse(Vector2 direction) {
        int impulseMode;
        if (mousePosition.y > objectPosition.y) {
            impulseMode = 1;
        }
        else {
            impulseMode = -1;
        }
        rb.linearVelocity = direction * impulseMode * attributes.power;
    }

    private Vector2 ClampDirection(float angle) {
        Vector2 direction = Vector2.zero;

        if (angle <= 10 && angle >= -10) direction = new Vector2(1, 0);
        else if (angle < 80 && angle > 10) direction = new Vector2(.5f, .9f);
        else if (angle <= 100 && angle >= 80) direction = new Vector2(0, 1);
        else if (angle < 170 && angle > 100) direction = new Vector2(-.5f, .9f);
        else if (angle <= -170 || angle >= 170) direction = new Vector2(-1, 0);
        else if (angle < -100 && angle > -170) direction = new Vector2(-.5f, -.9f);
        else if (angle <= -80 && angle >= -100) direction = new Vector2(0, -1);
        else if (angle <= -10 && angle >= -80) direction = new Vector2(.5f, -.9f);

        return direction;
    }
}
