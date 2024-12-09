using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelController : SingletonBase<LevelController>
    {
        private const string MainMenuSceneName = "main_menu";
        public event UnityAction LevelPassed; // Уровень пройден
        public event UnityAction LevelLost; // Уровень проигран 

        [SerializeField] private LevelProperties m_LevelProperties;

        [SerializeField] private LevelCondition[] m_Condition;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;

        public bool HasNextLevel => m_LevelProperties.NextLevel != null;

        public float LevelTime => m_LevelTime;

        private void Start()
        {
            Time.timeScale = 1;
            m_LevelTime = 0;
        }

        private void Update()
        {
            if (m_IsLevelCompleted == false)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelCondition();
            }

            if(Player.Instance.NumLives == 0)
            {
                Lose();
            }
        }

        private void CheckLevelCondition()
        {
            int numComleted = 0;

            for (int i = 0; i < m_Condition.Length; i++)
            {
                if (m_Condition[i].IsCompleted)
                {
                    numComleted++;
                }
            }

            if (numComleted == m_Condition.Length)
            {
                m_IsLevelCompleted = true;
                Pass();
            }
        }

        private void Lose()
        {
            LevelLost?.Invoke();
            Time.timeScale = 0;
        }

        private void Pass()
        {
            LevelPassed?.Invoke();
            Time.timeScale = 0;
        }

        public void LoadNestLevel()
        {
            if(HasNextLevel == true)           
               SceneManager.LoadScene(m_LevelProperties.NextLevel.SceneName);           
            else            
                SceneManager.LoadScene(MainMenuSceneName);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(m_LevelProperties.SceneName);
        }
    } 
}