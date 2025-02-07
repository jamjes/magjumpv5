using NUnit.Framework.Internal.Filters;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Magnet magnet;
    public PlayerMovementHandler movement;
    public PlayerCollisionHandler collisions;

    private void Start() {
        magnet.Hide(true);
    }

    private void Update() {
        bool isGrounded = collisions.IsGround();
        Vector2 direction = (magnet.transform.position - transform.position).normalized;
        float xDirection = direction.x * -1;

        if (Input.GetKey(KeyCode.Space)) {
            magnet.Hide(false);
        }
        else {
            magnet.Hide(true);
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            if (direction.y > 0 || isGrounded == false) {
                return;
            }
            
            movement.Impulse(xDirection);
        }
    }

}
