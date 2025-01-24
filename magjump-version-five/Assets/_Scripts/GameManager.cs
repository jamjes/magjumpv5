using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int deathCount = 0;
    public int levelCount = 1;
    public float transitionDuration = .75f;

    private void OnEnable() {
        Killzone.OnPlayerDeath += ResetAfterDeath;
        WinCondition.OnPlayerWin += NextLevel;
    }

    private void OnDisable() {
        Killzone.OnPlayerDeath -= ResetAfterDeath;
        WinCondition.OnPlayerWin -= NextLevel;
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void ResetAfterDeath() => deathCount++;
    private void NextLevel() => levelCount++;
}
