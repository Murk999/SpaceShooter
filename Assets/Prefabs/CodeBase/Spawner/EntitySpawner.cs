using UnityEngine;
using Common;
namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode // Выбираем мод как спавним , при старте или периодически
        {
            Start,
            Loop
        }

        [SerializeField] private Entity[] m_EntityPrefabs; // Префабы которые может заспавнить спавнер 

        [SerializeField] private CircleAreas m_Area; // Зона в которой будет спавн префабов

        [SerializeField] private SpawnMode m_SpawnMode; // ссылка на спавн мод

        [SerializeField] private int m_NumSpawns; // Кол-во спавнов

        [SerializeField] private float m_RespawnTime; // Как часто обновляем спавн, таймер 

        private float m_Timer; // Таймер

        private void Start() // задаем начальное значение при старте 
        {
            if(m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_RespawnTime;
        }

        private void Update()
        {
            if(m_Timer > 0)               // Если таймер больше 0 , то отнимаем по секунде 
            {
                m_Timer -= Time.deltaTime;
            }
            if(m_SpawnMode == SpawnMode.Loop && m_Timer < 0)   // Если мод переодический и таймер меньше 0 , то обновляем таймер 
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities() // Метод который будет спавнить сущности 
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                int index = Random.Range(0, m_EntityPrefabs.Length);
                GameObject e = Instantiate(m_EntityPrefabs[index].gameObject);
                e.transform.position = m_Area.GetRandomInsideZone();
            }
        }
    } 
}