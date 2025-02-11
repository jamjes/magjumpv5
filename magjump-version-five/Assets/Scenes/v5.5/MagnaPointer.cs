using UnityEngine;

public class MagnaPointer : MonoBehaviour
{
    private GameObject _player;
    private SpriteRenderer _spr;
    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, Time.deltaTime * 9);

        Vector2 playerPos = _player.transform.position;
        Vector2 currentPos = transform.position - _player.transform.position;
        Vector2 dif = currentPos.normalized * 3;
        
        float angle = AngleBetweenTwoPoints(transform.position, _player.transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        if (CanImpulse()) {
            _spr.color = Color.green;
        } else {
            _spr.color = Color.white;
        }
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public bool CanImpulse() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 3, Vector2.zero);
        foreach(RaycastHit2D hit in hits) {
            if (hit.collider.tag == "Player") {
                return true;
            }
        }

        return false;
    }
}
