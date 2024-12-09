using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

namespace Common
{
    /// <summary>
    /// ������������ ������ �� �����. ��� ��� ����� ����� �������� ��.
    /// </summary>
    public class Destructible : Entity // Destructible �������� ����� �� ��������  Entity
    {
        // ������ ���������
        #region Properties 
        /// <summary>
        /// ������ ���������� �����������
        /// </summary>
        [SerializeField] private bool m_Indestructible; // �������� �������������� ������ 
        public bool IsIndestructible => m_Indestructible;
        /// <summary>
        /// ��������� ������� ��������  ��.
        /// </summary
        [SerializeField] private int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;

        /// <summary>
        /// ������� �������� �������� ��.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;
        #endregion

        // ������ �������
        #region Unity Event
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints; // ������� ��  = ��������� �� ��� ������ 

            transform.SetParent(null);
        }
        #endregion
        // ������ ��������� ���
        #region Public API
        /// <summary>
        /// ���������� ������ � �������
        /// </summary>
        /// <param name="damage"> ���� ��������� ������� </param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return; // ���� ������ �������������� �� ���������� ��������

            m_CurrentHitPoints -= damage; // �������� � �������� �� , ����� � ������ 
            
            if (m_CurrentHitPoints <= 0) // ���� �� ������ ��� ����� ����, �� �������� ����� ����������� ������� 
                OnDeath();
        }
        #endregion

        public void ResistDamage()
        {
            m_CurrentHitPoints = 999999999;
        }

        public void ResetHP()
        {
            m_CurrentHitPoints = 100;
        }

        public void AsteroidCrash()
        {

        }
        /// <summary>
        /// ���������������� ������� ����������� �������, ����� �� ���� ����.
        /// </summary>
        protected virtual void OnDeath() // ����� ����������� ������� 
        {
            Destroy(gameObject, 1f);
            if (m_EventOnDeath != null)
            {
                m_EventOnDeath.Invoke(); // �������� ������� � ��������� � ������� ? �� null 
            }
        }

        private static HashSet<Destructible> m_AllDestructibles; // ������(����) ������ ������� ������ � ���� ��� ������������ ������� 

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles; // ��������� ������� �� ����� ������ ��������� ������ �� ������� ���������

        protected virtual void OnEnable()
        {
            if(m_AllDestructibles == null) // ����� � ��� ���������� ������             
                m_AllDestructibles = new HashSet<Destructible>(); // ��������� ���� �� ������� , �� ��������� ����������� ����� �� ������ 
            
            m_AllDestructibles.Add(this); // ������ � ����� �������� ��������� 
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }
        /// <summary>
        /// �������� ������ 
        /// </summary>
        public const int TeamIdNeutral = 0; // ����������� �������

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] public UnityEvent m_EventOnDeath; // ������� ������� 
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;  
    }
}
