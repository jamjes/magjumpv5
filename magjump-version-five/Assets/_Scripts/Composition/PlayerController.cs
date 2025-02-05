using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerCollisionHandler _collisions;
    public bool IsGrounded, IsCeiling;
    private Rigidbody2D _rbd;


    private void Awake() {
        _collisions = GetComponent<PlayerCollisionHandler>();
        _rbd = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        IsGrounded = _collisions.IsGround();
        IsCeiling = _collisions.IsCeiling();

        if (Input.GetKeyDown(KeyCode.X)) {
            _rbd.linearVelocityY = 15;
        }
    }
}
