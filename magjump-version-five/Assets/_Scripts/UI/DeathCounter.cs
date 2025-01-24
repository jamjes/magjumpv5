using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    TextMeshProUGUI deathCount;

    private void Awake() {
        deathCount = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        deathCount.text = GameManager.instance.deathCount.ToString();
    }
}
