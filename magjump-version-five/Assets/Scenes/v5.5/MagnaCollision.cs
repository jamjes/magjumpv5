using UnityEngine;

public class MagnaCollision
{
    private Magna _player;
    private BoxCollider2D _coll;
    private LayerMask _groundLayer;

    public MagnaCollision(Magna player, LayerMask groundLayer) {
        _player = player;
        _coll = player.Coll;
        _groundLayer = groundLayer;
    }

    public bool CollisionCheck(Vector2 targetDirection) {
        RaycastHit2D hit = Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0, targetDirection, .2f, _groundLayer);
        return hit.collider != null;
    }

    public void CollisionEnter(Collision2D collision) {
        if (collision.gameObject.layer == 7) {
            bool isGrounded = CollisionCheck(Vector2.down);
            bool isCeiled = CollisionCheck(Vector2.up);
            if (isGrounded) {
                _player.Rb.linearVelocity = Vector2.zero;
            } else if (isCeiled && _player.CanFreeze) {
                _player.Freeze();
            }
        }
    }
}
