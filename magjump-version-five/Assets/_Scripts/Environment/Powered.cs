using UnityEngine;

public class Powered : MonoBehaviour, IPowerable
{
    private bool powered;
    [SerializeField] private float speed;
    [SerializeField] private GameObject platform;
    public Transform[] WayPoints;
    private int targetIndex;
    private Color originalColor;
    public Color activeColor;

    private void Start() {
        platform.transform.position = WayPoints[0].position;
        powered = false;
        targetIndex = 0;
        originalColor = platform.GetComponent<SpriteRenderer>().color;
    }

    private void Update() {
        if (powered == true) {
            if (platform.transform.position != WayPoints[WayPoints.Length - 1].position) {
                if (platform.transform.position == WayPoints[targetIndex].position) {
                    targetIndex++;
                    return;
                }
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, WayPoints[targetIndex].position, speed * Time.deltaTime);
            }
        } else {
            if (platform.transform.position != WayPoints[0].position) {
                if (platform.transform.position == WayPoints[targetIndex].position) {
                    targetIndex--;
                    return;
                }
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, WayPoints[targetIndex].position, speed * Time.deltaTime);
            }
        }
    }

    public void PowerOn(SpriteRenderer spr) {
        powered = true;
        spr.color = activeColor;
        targetIndex = 1;
    }

    public void PowerOff(SpriteRenderer spr) {
        powered = false;
        spr.color = originalColor;
        targetIndex = WayPoints.Length - 2;
    }
}
