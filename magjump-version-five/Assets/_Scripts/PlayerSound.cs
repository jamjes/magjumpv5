using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource src;

    private void Awake() {
        src = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        Player.OnTrigger += PlaySound;
        Player.OnStop += StopSound;
    }

    private void OnDisable() {
        Player.OnTrigger -= PlaySound;
        Player.OnStop -= StopSound;
    }

    private void PlaySound(AudioClip target) {
        src.clip = target;
        src.Play();
    }

    private void StopSound(AudioClip target) {
        src.Stop();
    }
}
