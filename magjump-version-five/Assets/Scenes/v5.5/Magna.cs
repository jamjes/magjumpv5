using UnityEngine;
using UnityEngine.InputSystem;

public class Magna : MonoBehaviour {
    [Header("Dependencies")]
    [SerializeField] private MagnaPointer pointer;
    
    [Header("Input")]
    private Vector2 _direction;
    private MagnaInputActions _actions;
    public bool CanFreeze { get; private set; }

    [Header("Collision")]
    [SerializeField] private LayerMask groundLayer;
    public BoxCollider2D Coll { get; private set; }
    public MagnaCollision Collision { get; private set; }

    [Header("Physics")]
    [SerializeField] private float force = 15f;
    [SerializeField] private float fallGravityMultiplier = 1.25f;
    public Rigidbody2D Rb { get; private set; }
    public MagnaMovement Movement { get; private set; }

    private void Awake() {
        Coll = GetComponent<BoxCollider2D>();
        Rb = GetComponent<Rigidbody2D>();
        _actions = new MagnaInputActions();
        Movement = new MagnaMovement(this, force, fallGravityMultiplier);
        Collision = new MagnaCollision(this, groundLayer);
    }

    private void OnEnable() {
        _actions.Enable();
        _actions.Default.Attract.performed += EvaluateMagnetism;
        _actions.Default.Attract.canceled += EvaluateMagnetism;
    }

    private void Update() {
        bool isGrounded = Collision.CollisionCheck(Vector2.down);
        float x = GetMouseRelativePosition().x;
        float y = GetMouseRelativePosition().y > 0 ? 1 : -1;
        _direction = new Vector2(x, y).normalized;

        if (_actions.Default.Attract.triggered && _direction.y > 0) {
            if (pointer.CanImpulse()) {
                Movement.ApplyForce(_direction);
            }
        }

        if (isGrounded == false) {
            return;
        }

        if (_actions.Default.Repulse.triggered && _direction.y < 0) {
            if (pointer.CanImpulse()) {
                Movement.ApplyForce(_direction * -1);
            }
        }
    }

    private void FixedUpdate() {
        Movement.GravityUpdate();
    }

    private Vector2 GetMouseRelativePosition() {
        Vector2 mousePos = _actions.Default.Magnet.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos -= (Vector2)transform.position;
        return mousePos.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Collision.CollisionEnter(collision);
    }

    private void EvaluateMagnetism(InputAction.CallbackContext cxt) {
        if (cxt.performed) {
            CanFreeze = true;
        } else if (cxt.canceled) {
            Unfreeze();
        }
    }


    private void OnDisable() {
        _actions.Default.Attract.performed -= EvaluateMagnetism;
        _actions.Default.Attract.canceled -= EvaluateMagnetism;
        _actions.Disable();
    }

    public void Freeze() {
        Rb.gravityScale = 0;
        Rb.linearVelocity = Vector2.zero;
    }

    public void Unfreeze() {
        Rb.gravityScale = 1 * fallGravityMultiplier;
        CanFreeze = false;
    }
}
