using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int deathCount;
    public int levelCount;
    public float transitionDuration = .75f;

    private void OnEnable() {
        Killzone.OnPlayerDeath += ResetAfterDeath;
        WinCondition.OnPlayerWin += NextLevel;
        SceneTransitionHandler.OnGameReset += ResetGame;
    }

    private void OnDisable() {
        Killzone.OnPlayerDeath -= ResetAfterDeath;
        WinCondition.OnPlayerWin -= NextLevel;
        SceneTransitionHandler.OnGameReset -= ResetGame;
    }

    private void ResetGame() {
        deathCount = 0;
        levelCount = 1;
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

    private void Start() {
        ResetGame();
    }

    private void ResetAfterDeath() => deathCount++;
    private void NextLevel() => levelCount++;
}
