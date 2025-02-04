using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;

    private void OnEnable() {
        Killzone.OnPlayerDeath += Trigger;
    }

    private void OnDisable() {
        Killzone.OnPlayerDeath -= Trigger;
    }

    private void Update() {
        if (start == true) {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    private IEnumerator Shaking() {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

    private void Trigger() {
        start = true;
    }
}
