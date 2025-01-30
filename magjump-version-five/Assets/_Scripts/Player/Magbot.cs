using UnityEngine;

public class Magbot : MonoBehaviour
{
    BoxCollider2D coll;
    [SerializeField] LayerMask groundLayer;
    public Vector2 Mouse;
    Rigidbody2D rbd;
    [SerializeField] Vector2 power;

    void Awake() {
        coll = GetComponent<BoxCollider2D>();
        rbd = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        bool isGrounded = IsGrounded();
        Mouse = CalculateDirection();

        if (Input.GetKeyDown(KeyCode.X) && isGrounded == true) {
            rbd.linearVelocity = Mouse * power;
        }
        

    }

    private bool IsGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .25f, groundLayer);
        return hit.collider != null;
    }

    private Vector2 CalculateDirection() {
        //Get mouse world position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //Calculate distance between player and mouse
        mousePosition.x -= transform.position.x;
        mousePosition.y -= transform.position.y;

        //Clamp distance to a radius of 4
        mousePosition.x = Mathf.Clamp(mousePosition.x, transform.position.x - 4, transform.position.x + 4);
        mousePosition.y = Mathf.Clamp(mousePosition.y, transform.position.y - 4, transform.position.y + 4);

        //center
        mousePosition.x -= transform.position.x;
        mousePosition.y -= transform.position.y;

        //Multiply by .25 to get a percentage
        mousePosition.x *= .25f;
        mousePosition.y *= .25f;

        //Round digit to 2 decimal points
        float x = (float)System.Math.Round(mousePosition.x, 2);
        float y = (float)System.Math.Round(mousePosition.y, 2);

        return new Vector2(x, y);
    }
}
