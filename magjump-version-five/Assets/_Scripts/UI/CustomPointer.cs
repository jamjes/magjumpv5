using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomPointer : MonoBehaviour {
    private Vector3 mousePosition;
    [SerializeField] private float moveSpeed = 9f;
    private GameObject player;
    private Vector2 direction;
    private SpriteRenderer img;
    private float angle;

    private void Awake() {
        img = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        FollowCursor();
        RotatePointer();
    }

    private void FollowCursor() {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
    }

    private void RotatePointer() {
        mousePosition = Input.mousePosition;
        Vector3 objectPosition;

        objectPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        direction.x = mousePosition.x - objectPosition.x;
        direction.y = mousePosition.y - objectPosition.y;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public Vector2 GetNormalDirection() {
        return direction.normalized;
    }

    public float GetAngle() {
        return angle;
    }

    public void SetColor(Color c) {
        img.color = c;
    }

}
