using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float followSpeed = 12f;
    private float elapsedTime;
    private float duration;
    private GameObject player;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, Time.deltaTime * followSpeed);
        mousePosition = Input.mousePosition;
        Vector2 direction;
        Vector3 objectPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        direction.x = mousePosition.x - objectPosition.x;
        direction.y = mousePosition.y - objectPosition.y;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
