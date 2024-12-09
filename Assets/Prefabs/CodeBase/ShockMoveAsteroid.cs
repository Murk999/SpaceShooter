using Common;
using UnityEditor;
using UnityEngine;

namespace SpaceShooter
{
    public class ShockMoveAsteroid : MonoBehaviour
    {
        [SerializeField] private float m_Velocity; // �������� �������

        [SerializeField] private float m_Lifetime; // ����� ����� �������

        [SerializeField] private int m_Damage; // ���� 

        [SerializeField] private float m_Radius;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab; // ������ ������

        [SerializeField] private GameObject boomSplash;

        [SerializeField] private GameObject shockGravityWell;

        public float Radius => m_Radius;

        private float m_Timer; // ����� ����� ������� 

        private void Start()
        {

            
        }

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity; // ������� ���������� ��� ������� = ������� ���������� �� �������� 

            Vector2 step = transform.up * stepLength; // ������ ����������� ������� ������ ����� ��������� �� ��� ������� � ������� 

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength); // ��������� ������ ������� ������� ����  �� ��� �� �� ����������� ��������

            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>(); // ��������� ������ �� ����� ����������� � ��������� �������

                if (dest != null && dest != m_Parent) // ���� ���������� ����  ����������� �� �� ����� ���� 
                {
                    dest.ApplyDamage(m_Damage);  // �� ��������� ����
                }
                OnProjectileLifeEnd(hit.collider, hit.point); //������� �� ���� ������ 
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0); // ������ ����������� ������� ������ ���������� ���� �� � � �� �
                                                                  //  transform.position = Vector2.MoveTowards(transform.position, asteroid.transform.position, stepLength);

        }
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos) // ����� ��� �� ������ ��� ��������� � �� ��������� ���� 
        {
            Destroy(gameObject);
            Instantiate(boomSplash, transform.position, Quaternion.identity);
            Instantiate(shockGravityWell, transform.position, Quaternion.identity);
        }

        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent) // ������ ����� , � ��������� ����������� ��� ������
        {
            m_Parent = parent; // ������ ������� = ���������� ������ ��� �� � �������� �������� ��� ��������� �� �� ����� �������
        }

        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }
}