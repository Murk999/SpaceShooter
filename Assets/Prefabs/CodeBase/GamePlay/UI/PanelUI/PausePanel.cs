using UnityEngine.SceneManagement;
using UnityEngine;

namespace SpaceShooter
{
    public class PausePanels : MonoBehaviour
    {
        [SerializeField] private GameObject m_Panel;

        private void Start()
        {
            m_Panel.SetActive(false);
            Time.timeScale = 1;
        }

        public void ShowPause() // Когда показываем паузу включаем панел и ставим время на 0
        {
            m_Panel.SetActive(true);
            Time.timeScale = 0;
        }

        public void HidePause()
        {
            m_Panel.SetActive(false);
            Time.timeScale = 1;
        }

        public void LoadMainMenu()
        {
            m_Panel.SetActive(false);
            Time.timeScale = 0;

            SceneManager.LoadScene(0);
        }
    }
}