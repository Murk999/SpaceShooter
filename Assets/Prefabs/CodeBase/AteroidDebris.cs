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
        private void AsteroidDebrisMove() // ������� ������������ ���-�� � ���� 1 ������������ �� ������ �������� �����
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }
    }
}