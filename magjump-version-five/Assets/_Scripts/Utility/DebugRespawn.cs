using System.Collections;
using UnityEngine;

public class DebugRespawn : MonoBehaviour
{
    private Vector3 spawnPos;

    private void OnEnable() {
        Killzone.OnPlayerDeath += Respawn;
        Hazard.OnHazordEnter += Respawn;
        //WinCondition.OnPlayerWin += Respawn;
    }

    private void OnDisable() {
        Killzone.OnPlayerDeath -= Respawn;
        Hazard.OnHazordEnter -= Respawn;
        //WinCondition.OnPlayerWin -= Respawn;
    }

    private void Start() {
        spawnPos = transform.position;
    }

    private void Respawn() {
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine() {
        yield return new WaitForSeconds(.3f);
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        transform.position = spawnPos;
    }
}
