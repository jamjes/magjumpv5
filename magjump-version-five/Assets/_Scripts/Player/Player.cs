using UnityEngine;

public class Player : MonoBehaviour
{
    public BoxCollider2D Coll { get; private set; }
    public Rigidbody2D Rbd { get; private set; }
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Magnet pointer;


    private void Awake() {
        Coll = GetComponent<BoxCollider2D>();
        Rbd = GetComponent<Rigidbody2D>();
    }


}
