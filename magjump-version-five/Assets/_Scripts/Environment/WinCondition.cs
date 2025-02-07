using UnityEngine;

public class WinCondition : MonoBehaviour
{
    BoxCollider2D coll;
    public delegate void WinConditionEvent();
    public static event WinConditionEvent OnPlayerWin;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (OnPlayerWin != null) {
                OnPlayerWin();
            }
        }
    }
}
