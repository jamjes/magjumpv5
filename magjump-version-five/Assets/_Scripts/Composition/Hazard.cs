using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hazard : MonoBehaviour {
    public delegate void HazardEvent();
    public static event HazardEvent OnHazordEnter;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player") && OnHazordEnter != null) {
            OnHazordEnter();
        }
    }
}
