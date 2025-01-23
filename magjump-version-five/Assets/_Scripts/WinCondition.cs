using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    BoxCollider2D coll;
    public delegate void WinConditionEvent();
    public static event WinConditionEvent OnPlayerWin;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            bool end = Landed();

            if (end && OnPlayerWin != null) {
                OnPlayerWin();
            }
        }
    }

    private bool Landed() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(coll.bounds.center, coll.bounds.size, 0, Vector2.up, .2f);
        foreach(RaycastHit2D hit in hits) {
            if (hit.collider != null && hit.collider.tag == "Player") {
                return true;
            }
        }

        return false;
    }
}
