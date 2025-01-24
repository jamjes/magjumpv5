using TMPro;
using UnityEngine;

public class LevelCount : MonoBehaviour
{
    TextMeshProUGUI levelCount;

    private void Awake() {
        levelCount = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        levelCount.text = GameManager.instance.levelCount.ToString();
    }
}
