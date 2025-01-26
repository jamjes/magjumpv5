using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] WayPoints;
    private float speed = 2f;
    private int targetIndex;
    public GameObject platform;

    private void Start() {
        platform.transform.position = WayPoints[0].position;
        targetIndex = 1;
    }

    private void Update() {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, WayPoints[targetIndex].position, speed * Time.deltaTime);
        if (platform.transform.position == WayPoints[targetIndex].position) {
            targetIndex++;
            if (targetIndex == WayPoints.Length) {
                targetIndex = 0;
            }
        }
    }
}
