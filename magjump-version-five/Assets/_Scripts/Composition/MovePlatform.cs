using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private LerpMove lerper;
    private float percentageComplete;
    private bool move;
    private int index;

    [SerializeField] private float moveInterval;
    [SerializeField] private float moveDuration;
    [SerializeField] private Transform[] wayPoints;

    private void Awake() {
        lerper = GetComponent<LerpMove>();
    }

    private void Start() {
        index = 0;
        lerper.SetValue(wayPoints[index].position, wayPoints[index+1].position, moveDuration);
        percentageComplete = 0;
        move = true;
    }

    private void Update() {
        if (move == false) {
            return;
        }
        
        if (percentageComplete != 1) {
            percentageComplete = lerper.Lerp(percentageComplete);
        } else {
            StartCoroutine(NextPos());
        }
    }

    private IEnumerator NextPos() {
        move = false;
        yield return new WaitForSeconds(moveInterval);
        index++;
        if (index + 1 == wayPoints.Length) {
            lerper.SetValue(wayPoints[index].position, wayPoints[0].position, moveDuration);
            index = -1;
        } else {
            lerper.SetValue(wayPoints[index].position, wayPoints[index + 1].position, moveDuration);
        }
        percentageComplete = 0;
        move = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player") && collision.transform.parent != transform) {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player") && collision.transform.parent != null) {
            collision.transform.parent = null;
        }
    }
}
