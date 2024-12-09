using Common;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] GameObject m_Asteroid;
        [SerializeField] GameObject m_Asteroid2;
        private Destructible m_des;

        public enum Size // ������� ������������ ������ ������� ������
        {
            Small, // ���������           
            Huge // ��������
        }

        [SerializeField] private Size size; // ������� ������� ������ 

        protected virtual void Awake() 
        {
            m_des = FindObjectOfType<Destructible>();

            m_des.m_EventOnDeath.AddListener(OnAsteroidDestroyed); // ������������� �� ������� Die

          //  SetSize(size);
        }

        protected virtual void OnDestroy() // ������� ����� 
        {
            m_des.m_EventOnDeath.RemoveListener(OnAsteroidDestroyed); // ������������ �� ������� 
        }

        public void OnAsteroidDestroyed() // ������� ����� ����������� ���������
        {
            if (size != Size.Small) // ��������� ���� �������� ��������� �� ���������� �������� 
            {
                AsteroidSpawn(); // ����������� ������� �� ������� ������ 
            }
            Destroy(gameObject);
        }

        public void AsteroidSpawn()
        {
            for (int i = 0; i < 1; i++) // ������� ���� ������� �� ������ ���������  1 ���
            {

                // TimeSTOPS.changeTimeTickEvent?.Invoke(1);
                m_Asteroid = Instantiate(m_Asteroid, transform.position, Quaternion.identity); 
                m_Asteroid2 = Instantiate(m_Asteroid2, transform.position, Quaternion.identity); 

                //    m_Asteroid.SetSize(size - 1);

            }
        }

        public void SetSize(Size size) // ������� ����� � ������� ������������� ��������� ������� � ���������� ������� �� ���� ��� ��� ��������� ������
        {
            if (size < 0) return;
            transform.localScale = GetVectorFromSize(size); // ������ ������� ������� ����� ������ 
            this.size = size;
        }

        public Vector3 GetVectorFromSize(Size size) // ������� ����� � ������� ������ ��������� ������� 
        {
            if (size == Size.Huge) return new Vector3(1, 1, 1); // ������ ����� ������ ��������� �����
                                                                //   transform.position = new Vector3(0, 0, +0.001f);

            if (size == Size.Small) return new Vector3(0.6f, 0.6f, 0.6f); // ������ ����� ������ ��������� �����
                                                                          //  transform.position = new Vector3(0, 0, +0.004f);

            return Vector3.one;
        }
    }
}