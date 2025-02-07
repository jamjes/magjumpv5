using System.IO;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spr;
    public bool CanImpulse {  get; private set; }
    private Vector2 maxPos;
    private Vector2 mouseAbsPos;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        spr = GetComponent<SpriteRenderer>();
    }


    void Update() {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, Time.deltaTime * 9);
        
        Vector2 playerPos = player.transform.position;
        Vector2 currentPos = transform.position - player.transform.position;
        Vector2 dif = currentPos.normalized * 3;
        maxPos = new Vector2(Mathf.Abs(dif.x), Mathf.Abs(dif.y));
        mouseAbsPos = new Vector2(Mathf.Abs(currentPos.x), Mathf.Abs(currentPos.y));

        if (mouseAbsPos.x > maxPos.x &&
            mouseAbsPos.y > maxPos.y) {
            CanImpulse = false;
        }
        else {
            CanImpulse = true;
        }

        //Get the Screen positions of the object
        //Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        //Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(player.transform.position);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(transform.position, player.transform.position);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void Hide(bool condition) {
        if (condition == true) {
            spr.enabled = false;
        } else {
            spr.enabled = true;
        }
    }
}
