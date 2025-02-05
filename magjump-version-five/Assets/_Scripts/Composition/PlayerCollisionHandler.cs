using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private BoxCollider2D _coll;
    private float _detectionLength;
    [SerializeField] private LayerMask _groundLayer;
    public bool CanMagnetise { get; private set; }

    private void Start() {
        _coll = GetComponent<BoxCollider2D>();
        _detectionLength = .15f;
    }

    public bool IsGround() {
        RaycastHit2D groundHit = BoxCollisionCheck(Vector2.down);
        return groundHit.collider != null;
    }

    public bool IsCeiling() {
        RaycastHit2D ceilingHit = BoxCollisionCheck(Vector2.up);
        return ceilingHit.collider != null;
    }

    public RaycastHit2D BoxCollisionCheck(Vector2 direction) {
        RaycastHit2D hit = Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0, direction, _detectionLength, _groundLayer);
        return hit;
    }

    public RaycastHit2D RayCheck(Vector2 direction, float magnetStrength) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, magnetStrength, _groundLayer);
        return hit;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        RaycastHit2D hit = BoxCollisionCheck(Vector2.up);
        if (hit.collider != null && collision.gameObject.layer == 6) {
            CanMagnetise = true;
        }
    }

    public void SetMagnetise(bool condition) {
        if (CanMagnetise != condition) {
            CanMagnetise = condition;
        }
    }
}
