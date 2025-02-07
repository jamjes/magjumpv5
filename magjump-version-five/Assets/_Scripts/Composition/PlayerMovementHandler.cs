using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Transactions;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private float force = 15;
    private float defaultGravityScale, fallGravityScale;
    private bool isInApex;
    private PlayerCollisionHandler collision;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
        fallGravityScale = defaultGravityScale * 1.5f;
        collision = GetComponent<PlayerCollisionHandler>();
    }

    private void Update() {
        bool isGrounded = collision.IsGround();
        if (isGrounded == true) {
            if (isInApex == true) {
                isInApex = false;
            }
            return;
        }

        //Apex Modifier
        if (rb.linearVelocityY < .2f && rb.linearVelocityY > -2f) {
            if (isInApex != true) {
                StartCoroutine(ApexModifier(rb.linearVelocityX));
            }
        }

        
        //Stronger gravity when falling
        if (rb.linearVelocityY < 0 && rb.gravityScale != fallGravityScale) {
            rb.gravityScale = fallGravityScale;
        } else if (rb.linearVelocityY > 0 && rb.gravityScale != defaultGravityScale) {
            rb.gravityScale = defaultGravityScale;
        }
    }

    public void Impulse(float direction) {
        rb.linearVelocity = new Vector2(direction * force, force);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 6) {
            rb.linearVelocity = Vector2.zero; //Stop velocity after landing
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.GetComponent<PlatformAnimator>() is IMagnetisable magnet) {
            magnet.Effect();
        }
    }

    private IEnumerator ApexModifier(float currentXVelocity) {
        isInApex = true;
        rb.gravityScale = 0;
        rb.linearVelocityY = 0;
        rb.linearVelocityX = currentXVelocity * .75f;
        yield return new WaitForSeconds(.2f);
        rb.linearVelocityY = -.3f;
        rb.gravityScale = fallGravityScale;
    }
}
