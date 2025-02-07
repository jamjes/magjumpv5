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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            targetPosition = activePosition;
        }
    }
}


