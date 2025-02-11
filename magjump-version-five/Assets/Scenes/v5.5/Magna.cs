using UnityEngine;

public class Magna : MonoBehaviour {
    [Header("Input")]
    private Vector2 _direction;
    private MagnaInputActions _actions;

    [Header("Collision")]
    [SerializeField] private LayerMask groundLayer;
    public BoxCollider2D Coll { get; private set; }
    public MagnaCollision Collision { get; private set; }

    [Header("Physics")]
    [SerializeField] private float force = 15f;
    public Rigidbody2D Rb { get; private set; }
    public MagnaMovement Movement { get; private set; }

    private void Awake() {
        Coll = GetComponent<BoxCollider2D>();
        Rb = GetComponent<Rigidbody2D>();
        _actions = new MagnaInputActions();
        Movement = new MagnaMovement(this, force);
        Collision = new MagnaCollision(this, groundLayer);
    }

    private void OnEnable() {
        _actions.Enable();
    }

    private void Update() {
        bool isGrounded = Collision.CollisionCheck(Vector2.down);
        float x = GetMouseRelativePosition().x;
        float y = GetMouseRelativePosition().y > 0 ? 1 : -1;
        _direction = new Vector2(x, y).normalized;

        if (_actions.Default.Attract.triggered) {
            Movement.ApplyForce(_direction);
        }

        if (isGrounded == false) {
            return;
        }

        if (_actions.Default.Repulse.triggered) {
            Movement.ApplyForce(_direction * -1);
        }
    }

    private void OnDisable() {
        _actions.Disable();
    }

    private Vector2 GetMouseRelativePosition() {
        Vector2 mousePos = _actions.Default.Magnet.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos -= (Vector2)transform.position;
        return mousePos.normalized;
    }
}
