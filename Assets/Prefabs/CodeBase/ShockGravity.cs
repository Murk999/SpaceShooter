using UnityEngine;

namespace SpaceShooter
{
    public class ShockGravity : MonoBehaviour
    {
        [SerializeField] private float m_Lifetime; // Время жизни снаряда

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Asteroid"))
            {
                //  TimeSTOPS.changeTimeTickEvent?.Invoke(0);
                Destroy(other.gameObject);
            }
        }
        private float m_Timer; // Время жизни эффекта 

        private void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);
        }
    }
}