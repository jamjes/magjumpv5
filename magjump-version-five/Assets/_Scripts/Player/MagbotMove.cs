using Mono.Cecil.Cil;
using UnityEngine;

public class MagbotMove : MonoBehaviour
{
    private Vector2 direction;
    private float magnetStrength = 3;
    [SerializeField] private LayerMask platformLayer;
    private Rigidbody2D rbd;
    private BoxCollider2D coll;
    bool magnetised = true;
    [SerializeField] private float force = 15;

    private void Awake() {
        rbd = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        Color c = Color.red;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(x,y).normalized;


        RaycastHit2D ceiling = CeilingCheck();
        if (Input.GetKey(KeyCode.X)) { //Magnetise //Disable gravity on ceiling collision
            if (ceiling.collider != null && rbd.gravityScale != 0) {
                rbd.gravityScale = 0;
                rbd.linearVelocity = Vector3.zero;
            }
        }

        
        if (Input.GetKeyUp(KeyCode.X)) {
            if (rbd.gravityScale != 1) { //De-Magnetise //Enable gravity
                rbd.gravityScale = 1;
            }
            else if (rbd.linearVelocityY > 0) { //Short Jump
                rbd.linearVelocityY = 0;
            }
        }

        //Impulse in Magnet Direction
        if (Input.GetKeyDown(KeyCode.X)) {
            RaycastHit2D groundData = GroundCheck();
            RaycastHit2D magnetData = MagnetCheck(direction);
            if (magnetData.collider == null && groundData.collider == null) //No Collision
            {
                return;
            }

            if (magnetData.collider == groundData.collider) { //Impulse direction is plaforn
                Impulse(direction * -1);
            }
            else if (magnetData.collider != null){ //Impulse direction is not platform
                Impulse(direction);
            }
        }
    }

    private void Impulse(Vector2 direction) {
        rbd.linearVelocity = new Vector2(direction.x, Mathf.Sign(direction.y)) * force;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) {
            rbd.linearVelocity = Vector3.zero;
        }
    }

    private RaycastHit2D GroundCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .2f, platformLayer);
        return hit;
    }
    private RaycastHit2D MagnetCheck(Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, magnetStrength, platformLayer);
        return hit;
    }
    private RaycastHit2D CeilingCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.up, .2f, platformLayer);
        return hit;
    }
}
