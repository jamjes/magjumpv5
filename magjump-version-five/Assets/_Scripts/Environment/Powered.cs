using UnityEngine;

public class Powered : MonoBehaviour, IPowerable
{
    bool powered = false;
    [SerializeField] private float speed;
    [SerializeField] Transform target;
    Vector3 homePosition;
    [SerializeField] private GameObject platform;
    bool end = false;

    public Transform[] WayPoints;
    public int targetIndex = 1;

    private void Start() {
        homePosition = platform.transform.position;
    }

    private void Update() {
        if (powered == true) {
            if (platform.transform.position == WayPoints[WayPoints.Length - 1].position) {
                return;
            }
            
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, WayPoints[targetIndex].position, speed * Time.deltaTime);
            if (platform.transform.position == WayPoints[targetIndex].position) {
                targetIndex++;
                if (targetIndex == WayPoints.Length) {
                    end = true;
                }
            }

            //platform.transform.position = Vector3.MoveTowards(platform.transform.position, target.position, speed * Time.deltaTime);
        } else {
            if (platform.transform.position == WayPoints[0].position) {
                return;
            }

            platform.transform.position = Vector3.MoveTowards(platform.transform.position, WayPoints[targetIndex].position, speed * Time.deltaTime);
            if (platform.transform.position == WayPoints[targetIndex].position) {
                targetIndex--;
                if (targetIndex == -1) {
                    end = true;
                }
            }

            //platform.transform.position = Vector3.MoveTowards(platform.transform.position, homePosition, speed * Time.deltaTime);
        }
    }

    public void PowerOn(SpriteRenderer spr) {
        powered = true;
        spr.color = Color.white;
        end = false;
    }

    public void PowerOff(SpriteRenderer spr) {
        powered = false;
        spr.color = Color.grey;
        end = false;
    }
}
