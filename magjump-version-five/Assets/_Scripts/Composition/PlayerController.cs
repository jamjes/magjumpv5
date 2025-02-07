using NUnit.Framework.Internal.Filters;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Magnet magnet;
    public PlayerMovementHandler movement;
    public PlayerCollisionHandler collisions;
    public Vector2 Direction {  get; private set; }

    private void Start() {
        magnet.Hide(true);
    }

    private void Update() {
        bool isGrounded = collisions.IsGround();
        Direction = (magnet.transform.position - transform.position).normalized;
        float xDirection = Direction.x * -1;

        if (Input.GetKey(KeyCode.Space)) {
            magnet.Hide(false);
        }
        else {
            magnet.Hide(true);
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            if (Direction.y > 0 || isGrounded == false) {
                return;
            }

            if (magnet.CanImpulse == false) {
                return;
            }
            
            movement.Impulse(xDirection);
        }
    }

}
