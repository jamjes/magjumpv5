using UnityEngine;

public class Pivot : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 objectPosition;
    Vector2 direction;
    float angle;

    private void Update() {
        mousePosition = Input.mousePosition;
        mousePosition.z = -10;
        objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        direction.x = mousePosition.x - objectPosition.x;
        direction.y = mousePosition.y - objectPosition.y;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public Vector2 GetDirection() {
        return direction;
    }

    public float GetAngle() {
        return angle;
    }
}
