using System.Collections;
using Assets.Scripts.Economy;
using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Text goldText;
        [SerializeField] private Text livesText;
        [SerializeField] private Text waveText;

        private void Start()
        {
            StartCoroutine(CheckForUiChanges());
        }

        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator CheckForUiChanges()
        {
            while (true)
            {
                goldText.text = $"Gold: {EconomySystem.Instance.Gold}";
                livesText.text = $"Lives: {PlayerHealth.Instance.CurrentHealth}/{PlayerHealth.Instance.MaxHealth}";
                waveText.text = $"Wave: {WaveManager.Instance.GetCurrentWave()}";
                yield return new WaitForSeconds(1);
            }
        }
    }
}
