using UnityEngine;

public class Powered : MonoBehaviour, IPowerable
{
    bool powered = false;
    [SerializeField] private float speed;
    [SerializeField] Transform target;
    Vector3 homePosition;
    [SerializeField] private GameObject platform;

    private void Start() {
        homePosition = platform.transform.position;
    }

    private void Update() {
        if (powered == true) {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, target.position, speed * Time.deltaTime);
        } else {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, homePosition, speed * Time.deltaTime);
        }
    }

    public void PowerOn(SpriteRenderer spr) {
        powered = true;
        spr.color = Color.white;
    }

    public void PowerOff(SpriteRenderer spr) {
        powered = false;
        spr.color = Color.grey;
    }
}
