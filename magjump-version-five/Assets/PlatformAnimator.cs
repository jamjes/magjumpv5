using System.Globalization;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlatformAnimator : MonoBehaviour, IMagnetisable
{
    private Vector2 idlePosition;
    private Vector2 activePosition;
    private Vector2 startPosition, targetPosition;
    private float elapsedTime;
    private float duration = 1;
    private BoxCollider2D coll;
    [SerializeField] LayerMask groundLayer;

    private void Awake() {
        //coll = GetComponent<BoxCollider2D>();
    }

    private void Start() {
        idlePosition = transform.position;
        activePosition = new Vector2(idlePosition.x, idlePosition.y - .125f);
        targetPosition = idlePosition;
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime);

        if (targetPosition == (Vector2)transform.position) {
            targetPosition = idlePosition;
        }

        //RaycastHit2D[] hits = Physics2D.BoxCastAll(coll.bounds.center, coll.bounds.size, 0, Vector2.up, .2f);

        //bool found = false;
        //foreach(RaycastHit2D hit in hits) {
        //    if (hit.collider != null && hit.collider.CompareTag("Player")) {
        //        found = true;
        //    }
        //}

        //if (!found && targetPosition != idlePosition) {
        //    targetPosition = idlePosition;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //if (collision.collider.CompareTag("Player")) {
        //    targetPosition = activePosition;
        //}
    }

    public void Magnetise(Collider2D player) {
        throw new System.NotImplementedException();
    }

    public void DeMagnetise() {
        throw new System.NotImplementedException();
    }

    public void Effect() {
        targetPosition = activePosition;
    }
}


