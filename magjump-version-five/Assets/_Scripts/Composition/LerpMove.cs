using UnityEngine;

public class LerpMove : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float duration;
    private float elapsedTime;

    public void SetValue(Vector2 startPosition, Vector2 endPosition, float duration) {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.duration = duration;
        elapsedTime = 0;
    }

    public float Lerp(float percentageComplete) {
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / duration;
        if (elapsedTime > duration) {
            percentageComplete = 1f;
        }

        Vector2 targetPosition = Vector2.Lerp(startPosition, endPosition, percentageComplete);
        transform.position = targetPosition;

        return percentageComplete;
    }
}
