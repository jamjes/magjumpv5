using UnityEngine;

public class Magna : MonoBehaviour
{
    private BoxCollider2D _coll;
    private Rigidbody2D _rb;
    private MagnaInputActions _actions;
    [SerializeField] private Vector2 _direction;
    private float _magnetForce;
    private LayerMask groundLayer;

    private void Awake() {
        _coll = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _actions = new MagnaInputActions();
    }

    private void Start() {
        _direction = Vector2.down;
        _magnetForce = 15f;
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void OnEnable() {
        _actions.Enable();
    }

    private void Update() {
        bool isGrounded = IsGrounded();
        float x = GetMouseRelativePosition().x;
        float y = GetMouseRelativePosition().y > 0 ? 1 : -1;
        _direction = new Vector2(x, y).normalized;

        if (_actions.Default.Attract.triggered) {
            ApplyForce(_direction);
        }

        if (isGrounded == false) {
            return;
        }

        if (_actions.Default.Repulse.triggered) {
            ApplyForce(_direction * -1);
        }
    }

    private void OnDisable() {
        _actions.Disable();
    }

    private void ApplyForce(Vector2 direction) {
        _rb.linearVelocity = direction * _magnetForce;
    }

    private bool IsGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0, Vector2.down, .2f, groundLayer);
        return hit.collider != null;
    }

    private Vector2 GetMouseRelativePosition() {
        Vector2 mousePos = _actions.Default.Magnet.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos -= (Vector2)transform.position;
        return mousePos.normalized;
    }
}
