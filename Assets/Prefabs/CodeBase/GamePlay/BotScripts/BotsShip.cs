using Common;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BotsShips : Destructible
    {
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
                    m_Turrets[i].FireBots();
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

        private float m_PrimaryEnergy; // ������� �������
        private int m_SecondaryAmmo; // ������� ���-�� ��������
        private float m_PrimaryShock;

        public void AddEnegy(int e) // ����� ���������� ������� 
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy); // ����������� ��� � ������������� ���������� �������
        }

        public void AddAmmo(int ammo) // ����� ���������� ��������
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo); // ����������� ��� � ������������� ���-�� ��������
        }

        private void InitOffensive() // ����� �������������� ��� ��� ���������� ��� ������, ����� ��������� � ����� 
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
            m_PrimaryShock = m_MaxShock;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        public bool DrawAmmo(int count) // �������� ������� , ��� ����� ( ���������� ) 
        {
            if (count == 0) return true;

            if (m_SecondaryAmmo >= count)
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

        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props); // ������� ���� � ������� �������� ����� ������ ������ ������� �������
            }
        }
    }
}


