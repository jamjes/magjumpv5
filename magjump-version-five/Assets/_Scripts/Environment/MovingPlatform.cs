using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] WayPoints;
    private float speed = 2f;
    private int currentIndex;
    public GameObject platform;

    private void Start() {
        currentIndex = 0;
        platform.transform.position = WayPoints[currentIndex].position;
    }

    private void Update() {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, WayPoints[currentIndex + 1].position, speed * Time.deltaTime);

        if (platform.transform.position == WayPoints[currentIndex + 1].position) {
            currentIndex++;

            if (currentIndex == WayPoints.Length - 1) {
                currentIndex = -1;
            }
        }
    }
}
