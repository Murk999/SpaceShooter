using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using SpaceShooter;
using static UnityEngine.UI.CanvasScaler;
using static UnityEngine.RuleTile.TilingRuleOutput;
namespace Common
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private float m_Velocity; // �������� �������

        [SerializeField] private float m_Lifetime; // ����� ����� �������

        [SerializeField] private int m_Damage; // ����         

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab; // ������ ������

        [SerializeField] protected GameObject boomSplash;

        protected virtual void OnHit(Destructible destructible) { }
        protected virtual void OnHit(Collider2D collider2D) { }

        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos) { }
                    
        private float m_Timer; // ����� ����� ������� 

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity; // ������� ���������� ��� ������� = ������� ���������� �� �������� 
            Vector2 step = transform.up * stepLength; // ������ ����������� ������� ������ ����� ��������� �� ��� ������� � ������� 

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength); // ��������� ������ ������� ������� ����  �� ��� �� �� ����������� ��������

            if (hit)
            {
                OnHit(hit.collider);

                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>(); // ��������� ������ �� ����� ����������� � ��������� �������

                if (dest != null && dest != m_Parent) // ���� ���������� ����  ����������� �� �� ����� ���� 
                {
                    dest.ApplyDamage(m_Damage);  // �� ��������� ����

                    OnHit(dest); 
                }
                OnProjectileLifeEnd(hit.collider, hit.point); // ������� �� ���� ������               
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                OnProjectileLifeEnd(hit.collider, hit.point);

            transform.position += new Vector3(step.x, step.y, 0); // ������ ����������� ������� ������ ���������� ���� �� � � �� �
        }

       // Instantiate(boomSplash, transform.position, Quaternion.identity);
        protected Destructible m_Parent;

        public void SetParentShooter(Destructible parent) // ������ ����� , � ��������� ����������� ��� ������
        {
            m_Parent = parent; // ������ ������� = ���������� ������ ��� �� � �������� �������� ��� ��������� �� �� ����� �������
        }
    }
}