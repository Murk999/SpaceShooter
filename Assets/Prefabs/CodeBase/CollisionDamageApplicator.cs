using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary"; // ������������ ��� �� � ���������� ��������� �� �������� ���� 

        [SerializeField] private float m_VelocityDamageModifier; // ���������� ����������� ����� ��������� �� �������� 

        [SerializeField] private float m_DamageConstatnt; // ���� ������� 100% ����� ������� 

        private void OnCollisionEnter2D(Collision2D collision) // �������� ������������ 
        {
            if (collision.transform.tag == IgnoreTag) return; // ���� ���� ���  WorldBoundary �� ������ ������ �� ������ 

            var destructable = transform.root.GetComponent<Destructible>(); // ������� ���������� ��� �������� ���� �� �� ������� �������� � Destructable

            if (destructable != null) // �������� � �������� �� , ����� � ������ ) // � ���� ���� Destructable ��
            {
                destructable.ApplyDamage((int)m_DamageConstatnt + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude)); // ������� ���� �� ������� ���������� � ����������� �� �������� 
            }         
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("shieldShip"))
            {
                var destructable = transform.root.GetComponent<Destructible>(); // ������� ���������� ��� �������� ���� �� �� ������� �������� � Destructable
                destructable.ResistDamage();
            }
        }

        private void OnTriggerExit2D(UnityEngine.Collider2D collision)
        {
            if (collision.gameObject.CompareTag("shieldShip"))
            {
                var destructable = transform.root.GetComponent<Destructible>();
                destructable.ResetHP();
            }
        }
    }
}