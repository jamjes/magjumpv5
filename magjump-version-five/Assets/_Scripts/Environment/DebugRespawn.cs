using UnityEngine;

public class DebugRespawn : MonoBehaviour
{
    private Vector3 spawnPos;

    private void OnEnable() {
        Killzone.OnPlayerDeath += Respawn;
        WinCondition.OnPlayerWin += Respawn;
    }

    private void OnDisable() {
        Killzone.OnPlayerDeath -= Respawn;
        WinCondition.OnPlayerWin -= Respawn;
    }

    private void Start() {
        spawnPos = transform.position;
    }

    private void Respawn() {
        transform.position = spawnPos;
    }
}
