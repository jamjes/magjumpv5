using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private Pivot pivotReference;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float power;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Shake shake;
    [SerializeField] private BoxCollider2D boxColl;
    [SerializeField] private AudioClip impactClip;
    [SerializeField] private AudioClip magnetClip;

    private float groundTimeRef;
    public delegate void SoundEvent(AudioClip target);
    public static event SoundEvent OnTrigger;
    public static event SoundEvent OnStop;
    bool run = false;

    private void OnEnable() {
        Killzone.OnPlayerDeath += StopLogic;
        WinCondition.OnPlayerWin += StopLogic;
    }

    private void OnDisable() {
        Killzone.OnPlayerDeath -= StopLogic;
        WinCondition.OnPlayerWin -= StopLogic;
    }

    private void StopLogic() {
        run = false;
    }

    private void Start() {
        groundTimeRef = Time.time;
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(GameManager.instance.transitionDuration);
        run = true;
    }

    

    private void Update() {
        if (run == false) {
            return;
        }
        
        Vector2 direction = pivotReference.GetDirection();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, detectionRadius, platformLayer); //Player is within range to attract / repel
        
        if (hit.collider != null) {
            pivotReference.SetColor(Color.green);
        } else {
            pivotReference.ResetColor();
        }

        if (Input.GetKeyDown(KeyCode.X) && hit.collider != null) {
            direction = ClampDirection(pivotReference.GetAngle());
            int impulseMode = direction.y < 0 ? -1 : 1; //If direction is down, repel, else, attract
            Impulse(direction, impulseMode);
        }

        if (Input.GetKey(KeyCode.X)) {
            RaycastHit2D topHit = Physics2D.Raycast(transform.position, Vector2.up, .5f, platformLayer); //If player is holding magnetic key and platform is above player
            if (topHit.collider != null) {
                rigidBody.gravityScale = 0;
                rigidBody.linearVelocity = Vector2.zero;
                transform.SetParent(topHit.collider.transform); //Attach to platform
                shake.start = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.X) && rigidBody.gravityScale == 0) { //When player released magnetic key
            rigidBody.gravityScale = 1;
            transform.SetParent(null); //Detach from platform
            
        }

        if (Input.GetKeyUp(KeyCode.X)) {
            if (OnStop != null) {
                OnStop(magnetClip);
            }
        }

        bool isGrounded = GroundCheck();

        if (isGrounded) {
            float lastGroundTime = groundTimeRef;
            groundTimeRef = Time.time;
            if (groundTimeRef - lastGroundTime > .3f) { //Player has been airbourne for more than .3 seconds
                shake.start = true;
                if (OnTrigger != null) {
                    OnTrigger(impactClip);
                }
            }
        }
    }

    private Vector2 ClampDirection(float angle) { 
        Vector2 direction = Vector2.zero;
        
        if (angle <= 10 && angle >= -10) direction = new Vector2(1, 0);
        else if (angle < 80 && angle > 10) direction = new Vector2(.5f, .9f);
        else if (angle <= 100 && angle >= 80) direction = new Vector2(0, 1);
        else if (angle < 170 && angle > 100) direction = new Vector2(-.5f, .9f);
        else if (angle <= -170 || angle >= 170) direction = new Vector2(-1, 0);
        else if (angle < -100 && angle > -170) direction = new Vector2(-.5f, -.9f);
        else if (angle <= -80 && angle >= -100) direction = new Vector2(0, -1);
        else if (angle <= -10 && angle >= -80) direction = new Vector2(.5f, -.9f);

        return direction;
    }

    private void Impulse(Vector2 direction, int impulseMode) {
        Vector2 velocity = direction * impulseMode * power;
        rigidBody.linearVelocity = velocity;
        shake.start = true;
        if (OnTrigger != null) {
            OnTrigger(magnetClip);
        }
    }

    private bool GroundCheck() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, platformLayer);
        return hit.collider != null;
    }
}
