using UnityEngine;

public class UIButton : MonoBehaviour
{
    public delegate void UIEvent();
    public static event UIEvent OnNext;
    public static event UIEvent OnReset;
    public static event UIEvent OnQuit;

    public void NextScene() {
        if (OnNext != null) {
            OnNext();
        }
    }

    public void ResetGame() {
        if (OnReset != null) {
            OnReset();
        }
    }

    public void QuitGame() {
        if (OnQuit != null) {
            OnQuit();
        }
    }
}
