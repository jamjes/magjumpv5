using UnityEngine;

public class Magna : MonoBehaviour
{
    private BoxCollider2D _coll;
    private Rigidbody2D _rb;
    private MagnaInputActions _actions;

    private void Awake() {
        _coll = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _actions = new MagnaInputActions();
    }

    private void OnEnable() {
        _actions.Enable();
    }

    private void Update() {
        if (_actions.Default.Attract.triggered) {
            Debug.Log("Attract Triggered");
        }
    }

    private void OnDisable() {
        _actions.Disable();
    }
}
