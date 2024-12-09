using UnityEngine;

namespace SpaceShooter
{
    public class AteroidDebris : MonoBehaviour
    {
        [SerializeField] private float m_RandomSpeed = 3;

        private void Start()
        {
            AsteroidDebrisMove();
        }
        private void AsteroidDebrisMove() // Спавним определенное кол-во и если 1 уничтожается то должен появится новый
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }
    }
}