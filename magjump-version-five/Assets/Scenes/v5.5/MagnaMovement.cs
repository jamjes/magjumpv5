using UnityEngine;

public class MagnaMovement
{
    private Magna _player;
    private Rigidbody2D _rb;
    private float _magnetForce;
    private float _defaultGravityScale;
    private float _fallGravityScale;

    public MagnaMovement(Magna player, float magnetForce, float fallGravityMultiplier) {
        _player = player;
        _rb = _player.Rb;
        _magnetForce = magnetForce;
        _defaultGravityScale = _rb.gravityScale;
        _fallGravityScale = _rb.gravityScale * fallGravityMultiplier;
    }

    public void GravityUpdate() {
        if (_rb.linearVelocityY > 0 && _rb.gravityScale != _defaultGravityScale) {
            _rb.gravityScale = _defaultGravityScale;
        } else if (_rb.linearVelocityY < 0 && _rb.gravityScale != _fallGravityScale) {
            _rb.gravityScale = _fallGravityScale;
        }
    }

    public void ApplyForce(Vector2 direction) {
        _rb.linearVelocity = new Vector2(direction.x, Mathf.Sign(direction.y)) * _magnetForce;
    }
}
