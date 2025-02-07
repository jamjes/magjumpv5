using UnityEngine;

public class SoundController : MonoBehaviour
{
    private bool timer = false;
    private BoxCollider2D coll;
    private float elapsedTime;
    public LayerMask groundLayer;
    public AudioSource land;
    public AudioSource death;
    public AudioSource win;
    private Rigidbody2D rb;

    private void OnEnable() {
        Hazard.OnHazordEnter += PlayDeathSound;
        WinCondition.OnPlayerWin += PlayWinSound;
    }

    private void OnDisable() {
        Hazard.OnHazordEnter -= PlayDeathSound;
        WinCondition.OnPlayerWin -= PlayWinSound;
    }

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (rb.linearVelocityY > 0 && timer == false) {
            timer = true;
            elapsedTime = 0;
        }

        if (timer) {
            elapsedTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 6) {
            return;
        }

        timer = false;

        if (elapsedTime < .5f) {
            elapsedTime = 0;
            return;
        }

        RaycastHit2D groundCheck = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, groundLayer);
        RaycastHit2D ceilingCheck = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.up, .1f, groundLayer);

        if (groundCheck.collider != null) {
            land.Play();
        }
        else if (ceilingCheck.collider != null) {
            //
        }
        elapsedTime = 0;
    }

    private void PlayDeathSound() {
        death.Play();
        elapsedTime = 0;
    }

    private void PlayWinSound() {
        win.Play();
        elapsedTime = 0;
    }
}
