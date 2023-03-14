using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EndgameUI : MonoBehaviour
    {
        private Canvas canvas;
        [SerializeField]
        private Text endgameText;
        [SerializeField]
        private Text buttonText;
        private bool win;

        void Start()
        {
            canvas = GetComponent<Canvas>();
            PlayerHealth.Instance.PlayerDeath += OnPlayerDeath;
            WaveManager.Instance.GameWon += OnPlayerWin;
            canvas.enabled = false;
        }

        void OnPlayerDeath()
        {
            win = false;
            canvas.enabled = true;
            endgameText.text = "You've lost the game. Do you want to start again?";
            buttonText.text = "Restart";
        }

        void OnPlayerWin()
        {
            win = true;
            canvas.enabled = true;
            endgameText.text = "Congratulations, you've won the game. Press the button to proceed";
            buttonText.text = "Ok";
        }

        public void EndgameOrRestart()
        {
            canvas.enabled = false;
            if (!win)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
            }
        }
    }
}