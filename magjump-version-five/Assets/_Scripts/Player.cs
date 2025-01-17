using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private Pivot pivotReference;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float power;

    private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 direction = pivotReference.GetDirection();

        if (Input.GetKeyDown(KeyCode.X)) { //Input.GetMouseButtonDown(1)
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, detectionRadius, platformLayer);
            if (hit.collider != null) {
                direction = CalculateDirection(pivotReference.GetAngle());
                if (hit.collider.tag == "South") {
                    Repel(direction * -1);
                }
   
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, detectionRadius, platformLayer);
            if (hit.collider != null) {
                direction = CalculateDirection(pivotReference.GetAngle());
                if (hit.collider.tag == "North") {
                    Attract(direction);
                }

            }
        }

        if (Input.GetKey(KeyCode.Z)) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, .5f, platformLayer);
            if (hit.collider != null) {
                rigidBody.gravityScale = 0;
                rigidBody.linearVelocity = Vector2.zero;
                transform.SetParent(hit.collider.transform);
            }
        }

        if (Input.GetKeyUp(KeyCode.Z) && rigidBody.gravityScale == 0) {
            rigidBody.gravityScale = 1;
            if (transform.parent != null) {
                transform.SetParent(null);
            }
        }
    }

    private Vector2 CalculateDirection(float angle) {
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

    private void Repel(Vector2 direction) {
        rigidBody.linearVelocity = direction * power;
    }

    private void Attract(Vector2 direction) {
        rigidBody.linearVelocity = direction * power;
    }
}
