using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text waveText;

    // Update is called once per frame
    void Update()
    {
        goldText.text = $"Gold: {EconomySystem.Instance.Gold}";
        livesText.text = $"Lives: {PlayerHealth.Instance.CurrentHealth}/{PlayerHealth.Instance.MaxHealth}";
        waveText.text = $"Wave: {WaveManager.Instance.GetCurrentWave()}";
    }
}
