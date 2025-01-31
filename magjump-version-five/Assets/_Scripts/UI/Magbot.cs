using UnityEngine;
using UnityEngine.InputSystem;

public class Magbot : MonoBehaviour {

    public Vector2 directionalInput;
    public Rigidbody2D rbd;
    public BoxCollider2D coll;
    public LayerMask groundLayer;

    public delegate void PlayerEvent();
    public static event PlayerEvent OnJumpEnter;

    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.X) && Grounded()) {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition -= (Vector2)transform.position;
            Vector2 normal = mousePosition.normalized;
            if (normal.y > 0) {
                return;
            }
            directionalInput = new Vector2((-15 * normal.x), 15);
            rbd.linearVelocity = directionalInput;
            if (OnJumpEnter != null) {
                OnJumpEnter();
            }
        }
        
    }

    private bool Grounded() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .25f, groundLayer);
        return hit.collider != null;
    }

}