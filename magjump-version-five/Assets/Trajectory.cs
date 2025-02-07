using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int _segmentCount = 20;
    private LineRenderer _lineRenderer;
    private float curveLength = 1f;
    private Vector2[] _segments;
    private float force = 15;
    [SerializeField] private PlayerMove player;
    [SerializeField] private Rigidbody2D rb;

    private void Start() {
        _segments = new Vector2[_segmentCount];
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segmentCount;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.X) == false) {
            return;
        }
        
        Vector2 startPos = transform.position;
        _segments[0] = startPos;
        _lineRenderer.SetPosition(0, startPos);

        Vector2 startVel;
        int multiplier = 1;

        if (player.Direction.y < 0) {
            multiplier = -1;
        }

        startVel = new Vector2((player.Direction.x * multiplier * force), force);

        for(int i = 1; i < _segmentCount; i++) {
            float timeOffset = (i * Time.fixedDeltaTime * curveLength);
            Vector2 gravOffset = .25f * Physics2D.gravity * 2 * Mathf.Pow(timeOffset, 2);
            _segments[i] = _segments[0] + startVel * timeOffset + gravOffset;
            _lineRenderer.SetPosition(i, _segments[i]);
        }

    }
}
