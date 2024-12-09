using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

namespace Common
{
    /// <summary>
    /// Уничтожаемый объект на сцене. Тот что может иметь здоровье ХП.
    /// </summary>
    public class Destructible : Entity // Destructible дочерний класс от родителя  Entity
    {
        // Регион параметры
        #region Properties 
        /// <summary>
        /// Объект Игнорирует Повреждения
        /// </summary>
        [SerializeField] private bool m_Indestructible; // Свойство неуничтожаемый объект 
        public bool IsIndestructible => m_Indestructible;
        /// <summary>
        /// Стартовое значени здоровья  ХП.
        /// </summary
        [SerializeField] private int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;

        /// <summary>
        /// Текущее значение здоровья ХП.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;
        #endregion

        // Регион события
        #region Unity Event
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints; // Текущее ХП  = заданному хп при Старте 

            transform.SetParent(null);
        }
        #endregion
        // Регион Публичный АПИ
        #region Public API
        /// <summary>
        /// Применение дамага к объекту
        /// </summary>
        /// <param name="damage"> Урон наносимый объекту </param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return; // Если объект неуничтожаемый то возвращаем значение

            m_CurrentHitPoints -= damage; // Отнимаем у текущеко ХП , число в дамаге 
            
            if (m_CurrentHitPoints <= 0) // Если хп меньше или равно нулю, то вызываем метод уничтожения объекта 
                OnDeath();
        }
        #endregion

        public void ResistDamage()
        {
            m_CurrentHitPoints = 999999999;
        }

        public void ResetHP()
        {
            m_CurrentHitPoints = 100;
        }

        public void AsteroidCrash()
        {

        }
        /// <summary>
        /// Переопределяемое событие уничтожения объекта, когда ХП ниже нуля.
        /// </summary>
        protected virtual void OnDeath() // метод уничтодения объекта 
        {
            Destroy(gameObject, 1f);
            if (m_EventOnDeath != null)
            {
                m_EventOnDeath.Invoke(); // Вызываем событие и проверяем с помощью ? на null 
            }
        }

        private static HashSet<Destructible> m_AllDestructibles; // ХэшСет(Лист) список который хронит в себе все уничтожаемые объекты 

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles; // Коллекция которую мы можем только прочитать ссылка на ХэшЛист коллекцию

        protected virtual void OnEnable()
        {
            if(m_AllDestructibles == null) // Когда у нас появляется объект             
                m_AllDestructibles = new HashSet<Destructible>(); // Проверяем если не создана , то добавляем собственный класс на объект 
            
            m_AllDestructibles.Add(this); // Доступ к нашей основной коллекции 
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }
        /// <summary>
        /// Создание команд 
        /// </summary>
        public const int TeamIdNeutral = 0; // Нейтральная команда

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] public UnityEvent m_EventOnDeath; // Создаем событие 
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;  
    }
}
