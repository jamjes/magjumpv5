using UnityEngine;

public interface IPowerable {
    void PowerOn(SpriteRenderer spr);
    void PowerOff(SpriteRenderer spr);
}

public interface IMagnetisable {
    void Magnetise(Collider2D player);
    void DeMagnetise();
}
