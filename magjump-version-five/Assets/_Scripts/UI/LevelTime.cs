using UnityEngine;
using TMPro;
using System.Collections;

public class LevelTime : MonoBehaviour
{
    float timeRef;
    TextMeshProUGUI timeValue;
    bool run = false;

    private void OnEnable() {
        WinCondition.OnPlayerWin += StopTime;
        Killzone.OnPlayerDeath += StopTime;
    }

    private void OnDisable() {
        WinCondition.OnPlayerWin -= StopTime;
        Killzone.OnPlayerDeath -= StopTime;
    }

    private void Awake() {
        timeValue = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        StartCoroutine(StartDelay());
    }

    private void Update() {
        if (run == false) {
            return;
        }
        
        timeRef += Time.deltaTime;
        timeValue.text = timeRef.ToString("0.00");
    }

    IEnumerator StartDelay() {
        yield return new WaitForSeconds(GameManager.instance.transitionDuration);
        run = true;
    }

    private void StopTime() => run = false;
    private void ResumeTime() => run = true;
    private void ResetTime() => timeRef = 0;
}
