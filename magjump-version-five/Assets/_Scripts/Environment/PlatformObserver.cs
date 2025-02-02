using UnityEngine;

public class PlatformObserver : MonoBehaviour
{
    private BoxCollider2D coll;
    private SpriteRenderer spr;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            this.transform.parent.GetComponent<Powered>()?.PowerOn(spr);
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            this.transform.parent.GetComponent<Powered>()?.PowerOff(spr);
            collision.transform.SetParent(null);
        }
    }
}
