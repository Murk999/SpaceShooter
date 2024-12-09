using UnityEngine;
using Common;
namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefabs; // Ссылка на префаб мусора

        [SerializeField] private CircleAreas m_Area; // Зона спавна мусора

        [SerializeField] private int m_NumDebris; // Кол-во мусора
     
        [SerializeField] private float m_RandomSpeed; // Скорость мусора 

        private void Start()
        {
            for (int i = 0; i < m_NumDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris() // Спавним определенное кол-во и если 1 уничтожается то должен появится новый
        {
            int index = Random.Range(0, m_DebrisPrefabs.Length);
            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            debris.transform.position = m_Area.GetRandomInsideZone();
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if(rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }

        private void OnDebrisDead()
        {
            SpawnDebris();   // При уничтожении камня создаем новый 
        }
    }
}