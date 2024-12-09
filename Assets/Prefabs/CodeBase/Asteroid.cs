using Common;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] GameObject m_Asteroid;
        [SerializeField] GameObject m_Asteroid2;
        private Destructible m_des;

        public enum Size // создаем перечисление камней размера камней
        {
            Small, // маленький           
            Huge // огромный
        }

        [SerializeField] private Size size; // Создаем функцию размер 

        protected virtual void Awake() 
        {
            m_des = FindObjectOfType<Destructible>();

            m_des.m_EventOnDeath.AddListener(OnAsteroidDestroyed); // Подписываемся на событие Die

          //  SetSize(size);
        }

        protected virtual void OnDestroy() // создаем метод 
        {
            m_des.m_EventOnDeath.RemoveListener(OnAsteroidDestroyed); // отписываемся от события 
        }

        public void OnAsteroidDestroyed() // создаем метод уничтожения астероида
        {
            if (size != Size.Small) // проверяем если астероид маленький то уничтожаем астероид 
            {
                AsteroidSpawn(); // уничтожение объекта на котором скрипт 
            }
            Destroy(gameObject);
        }

        public void AsteroidSpawn()
        {
            for (int i = 0; i < 1; i++) // Создаем цикл спавним из одного Астероида  1 раз
            {

                // TimeSTOPS.changeTimeTickEvent?.Invoke(1);
                m_Asteroid = Instantiate(m_Asteroid, transform.position, Quaternion.identity); 
                m_Asteroid2 = Instantiate(m_Asteroid2, transform.position, Quaternion.identity); 

                //    m_Asteroid.SetSize(size - 1);

            }
        }

        public void SetSize(Size size) // создаем метод в котором устанавлеваем изменения размера в лоакальной позиции то есть там где находится камень
        {
            if (size < 0) return;
            transform.localScale = GetVectorFromSize(size); // Задаем текущей позиции новый размер 
            this.size = size;
        }

        public Vector3 GetVectorFromSize(Size size) // Создаем метод в котором задаем изменения размера 
        {
            if (size == Size.Huge) return new Vector3(1, 1, 1); // Задаем новый размер Огромному камню
                                                                //   transform.position = new Vector3(0, 0, +0.001f);

            if (size == Size.Small) return new Vector3(0.6f, 0.6f, 0.6f); // Задаем новый размер Маленький камню
                                                                          //  transform.position = new Vector3(0, 0, +0.004f);

            return Vector3.one;
        }
    }
}