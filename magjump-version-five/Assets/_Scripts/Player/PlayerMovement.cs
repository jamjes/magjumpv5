using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rbd;
    [SerializeField] private Magnet magnet;
    [SerializeField] private PlayerCollisions collisions;

    private void Awake() {
        rbd = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            CalculateDirection();
        }
    }

    private void CalculateDirection() {
        Vector2 magnetPos = magnet.transform.position;
        if (magnetPos.y < transform.position.y && collisions.CanImpulse) {
            rbd.linearVelocityY = 15f;
        }
    }
}
