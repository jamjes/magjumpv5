using UnityEngine;

public class SignedPlatform : MonoBehaviour, IMagnetisable {
    public void DeMagnetise() {
        transform.DetachChildren();
    }

    public void Magnetise(Collider2D player) {
        player.transform.SetParent(transform);
    }
}
