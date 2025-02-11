using UnityEngine;

public class MagnaMovement
{
    private Magna _player;
    private Rigidbody2D _rb;
    private float _magnetForce;

    public MagnaMovement(Magna player, float magnetForce) {
        _player = player;
        _rb = _player.Rb;
        _magnetForce = magnetForce;
    }

    public void ApplyForce(Vector2 direction) {
        _rb.linearVelocity = new Vector2(direction.x, Mathf.Sign(direction.y)) * _magnetForce;
    }
}
