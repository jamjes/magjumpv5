using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public bool CanImpulse;
    public bool CanMagnetise;
    
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float detectionLenience;
    private BoxCollider2D coll;
    private Rigidbody2D rbd;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
        rbd = GetComponent<Rigidbody2D>();
    }

    

    private void Update() {
        CanImpulse = FloorCheck();
        //CanMagnetise = CeilingCheck();

        if (Input.GetKeyDown(KeyCode.X)) {
            rbd.linearVelocityY = 15;
        }
    }

    private bool FloorCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, detectionLenience, platformLayer);
        return hit.collider != null;
    }

    private bool CeilingCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.up, detectionLenience, platformLayer);
        return hit.collider != null;
    }
}
