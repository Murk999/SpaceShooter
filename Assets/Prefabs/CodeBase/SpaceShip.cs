using System.Collections;
using SpaceShooter;
using Common;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [SerializeField] private Sprite m_PreviewImage;

        /// <summary>
        /// ����� ��� �������������� ��������� � ��������� �����
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// ��������� ������ ����.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// ��������� ����.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// ������������ �������� �������� 
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// ������������ ������������ ��������. � ��������/���
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// ����������� ������ �� ���������� �����
        /// </summary>
        private Rigidbody2D m_Rigid;

        public  float MaxLinearVelocity => m_MaxLinearVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;
        public Sprite PrewiewImage => m_PreviewImage;

        #region Public API

        /// <summary>
        /// ���������� �������� �����. -1.0 �� +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// ���������� ������������ �����. -1.0 �� +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity Event

        protected override void Start()
        {
            base.Start();
            m_Rigid = GetComponent<Rigidbody2D>();
            
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitOffensive();
        }
       
        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }

        /// <summary>
        /// ����� ���������� ��� ������� ��� �������� 
        /// </summary>
        private void UpdateRigidBody() // ��� ����������� � �������� 
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        #endregion

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode) // ��������� ����� �������� � ������������ ���� ����� ������ � ��� ����� 
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        public void FireShock(TurretMode mode2) // ��������� ����� �������� � ������������ ���� ����� ������ � ��� ����� 
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode2)
                {
                    m_Turrets[i].FireShock();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy; // ������������ ���������� �������
        [SerializeField] private int m_MaxAmmo; // ������������ ���-�� ��������
        [SerializeField] private int m_EnergyRegenPerSecond; // ������� ������� ���������������� � �������
        [SerializeField] private int m_MaxDush; // ������������ ���������
        [SerializeField] private int m_MaxShock;
        [SerializeField] private int m_MaxHoming;

        public GameObject shield;
        public GameObject dushVishual;

        private float m_PrimaryEnergy; // ������� �������
        private int m_SecondaryAmmo; // ������� ���-�� ��������
        private float m_PrimaryShock;
        private float m_SecondaryHoming;

        public void AddEnegy(int e) // ����� ���������� ������� 
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy); // ����������� ��� � ������������� ���������� �������
        }

        public void AddAmmo(int ammo) // ����� ���������� ��������
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo); // ����������� ��� � ������������� ���-�� ��������
        }

        public void AddShock(int shock) // ����� ���������� ��������
        {
            m_PrimaryShock = Mathf.Clamp(m_PrimaryShock + shock, 0, m_MaxAmmo); // ����������� ��� � ������������� ���-�� ��������
        }

        public void AddHoming(int homing)
        {
            m_SecondaryHoming = Mathf.Clamp(m_SecondaryHoming + homing, 0, m_MaxHoming);
        }

        public void AddShield(Vector3 position)
        {
            GameObject shieldShip = Instantiate(shield, position, Quaternion.identity);
                 
            Destroy(shieldShip, 15);
          
        }

        public void AddDush(int dush)
        {
            m_MaxLinearVelocity = Mathf.Clamp(m_MaxLinearVelocity + dush, 0, m_MaxDush);
            DushVishual(transform.position);
            StartCoroutine(DushEnd());
        }

        IEnumerator DushEnd()
        {
            yield return new WaitForSeconds(10);
            m_MaxLinearVelocity = Mathf.Clamp(m_MaxLinearVelocity = 5, 0, m_MaxDush);
        }

        public void DushVishual(Vector3 position)
        {
            GameObject dushShip = Instantiate(dushVishual, position, Quaternion.identity);

            Destroy(dushShip, 10);
        }

        private void InitOffensive() // ����� �������������� ��� ��� ���������� ��� ������, ����� ��������� � ����� 
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
            m_PrimaryShock = m_MaxShock;
            m_SecondaryHoming = m_MaxHoming;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy,0, m_MaxEnergy);
        }

        public bool DrawAmmo(int count) // �������� ������� , ��� ����� ( ���������� ) 
        {
            if(count == 0) return true;

            if(m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }

        public bool DrawEnergy(int count) // �������� ������� , ��� ����� ( ���������� ) 
        {
            if (count == 0) return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }
            return false;
        }

        public bool DrawShock(int count) // �������� ������� , ��� ����� ( ���������� ) 
        {
            if (count == 0) return true;

            if (m_PrimaryShock >= count)
            {
                m_PrimaryShock -= count;
                return true;
            }
            return false;
        }

        public bool DrawHoming(int count)
        {
            if(count == 0) return true;
            if(m_SecondaryHoming >= count)
            {
                m_SecondaryHoming -= count;
                return true;
            }
            return false;
        }

        public void AssignWeapon(TurretProperties props) 
        {
            for(int i = 0; i <  m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props); // ������� ���� � ������� �������� ����� ������ ������ ������� �������
            }
        }
    }
}