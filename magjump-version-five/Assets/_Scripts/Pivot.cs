using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pivot : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 objectPosition;
    Vector2 direction;
    [SerializeField] SpriteRenderer spr;
    Color originalCol;
    float angle;
    bool run;

    private void Start() {
        originalCol = spr.color;
    }

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

    public void SetColor(Color target) {
        if (spr.color != target) {
            spr.color = target;
        }
    }

    public void ResetColor() {
        if (spr.color != originalCol) {
            spr.color = originalCol;
        }
    }
}
