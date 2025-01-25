using UnityEngine;

public class Powered : MonoBehaviour
{
    bool powered = false;
    [SerializeField] private float speed;
    [SerializeField] Transform target;
    Vector3 homePosition;

    private void Start() {
        homePosition = transform.position;
    }

    private void Update() {
        if (powered == true) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, homePosition, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            powered = true;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            powered = false;
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }
}
