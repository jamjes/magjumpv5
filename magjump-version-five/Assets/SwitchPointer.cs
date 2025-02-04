using System.IO;
using UnityEngine;

public class SwitchPointer : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spr;
    public bool canImpulse;
    public Vector2 maxPos, mouseAbsPos;

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
            spr.color = Color.grey;
            canImpulse = false;
        }
        else {
            spr.color = Color.green;
            canImpulse = true;
        }
    }
}
