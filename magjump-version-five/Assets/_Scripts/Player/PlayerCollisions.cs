using UnityEngine;

public class PlayerCollisions : MonoBehaviour {
    public bool CanImpulse { private set; get; }
    public bool CanMagnetise { private set; get; }
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float detectionLenience = .51f;
    private Rigidbody2D rbd;

    private void Update() {
        CanImpulse = PlatformCheck(Vector2.down);
        CanMagnetise = PlatformCheck(Vector2.up);
    }

    private bool PlatformCheck(Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionLenience, platformLayer);
        return hit.collider != null;
    }
}
