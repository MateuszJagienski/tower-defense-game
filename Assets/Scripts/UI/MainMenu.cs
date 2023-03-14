using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Scene03", LoadSceneMode.Single);
        }

        public void CloseGame()
        {
            Application.Quit();
        }

        public void ShowOptions()
        {

        }
    }
}
