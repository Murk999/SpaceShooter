using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode; // ��� ����� ������ � ��� �����
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties; // ������ �� ��������� ������

        private float m_RefireTimer; // ������ ���������� ��������

        public bool CanFire => m_RefireTimer <= 0; // �������� 

        private SpaceShip m_Ship; // ������ �� ������� 

        private BotsShips m_BotShip; // ������ �� ������� ����

        #region UNITY EVENT
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>(); // �������� ������ �� ������� ������� ������ ��� ������
            m_BotShip = transform.root.GetComponent<BotsShips>();
        }

        private void Update()
        {
            if(m_RefireTimer > 0)
            m_RefireTimer -= Time.deltaTime;  // ���������� �������� � ���������
        }

        // Public API

        public void Fire()
        {
            if (m_TurretProperties == null) return; // ��������� ���� ������� = 0 

            if(m_RefireTimer > 0) return; // ��������� ���� ����� ����� ��������� = 0

            if(m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            if (m_Ship.DrawShock(m_TurretProperties.ShockUsage) == false) return;
            //if(m_Ship.DrawHoming(m_TurretProperties.HomingUsage) == false) return;

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>(); // ������� ������� 
            
            projectile.transform.position = transform.position; // ������� ������� �������� , ������ ������� �������
            projectile.transform.up = transform.up; // ������� ����������� �������� �� ����������� ���� ������� ������� 

            projectile.SetParentShooter(m_Ship); // ��������� ���� ����� ��� �� �� ��������� ���� ������� �� �������� ��������
            projectile.SetParentShooter(m_BotShip); // ��������� ���� ����� ��� �� �� ��������� ���� ������� �� �������� ��������

            m_RefireTimer = m_TurretProperties.RateOfFire; // ���� ������� ���������������� ������� �������� ���������� ������� �������� ������� 
            {
                // SFX
            }
        }
        public void FireBots()
        {
            if (m_TurretProperties == null) return; // ��������� ���� ������� = 0 

            if (m_RefireTimer > 0) return; // ��������� ���� ����� ����� ��������� = 0

            if (m_BotShip.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
            if (m_BotShip.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            if (m_BotShip.DrawShock(m_TurretProperties.ShockUsage) == false) return;

            Projectile projectile3 = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>(); // ������� ������� 

            projectile3.transform.position = transform.position; // ������� ������� �������� , ������ ������� �������
            projectile3.transform.up = transform.up; // ������� ����������� �������� �� ����������� ���� ������� ������� 

            projectile3.SetParentShooter(m_BotShip); // ��������� ���� ����� ��� �� �� ��������� ���� ������� �� �������� ��������

            m_RefireTimer = m_TurretProperties.RateOfFire; // ���� �������� ���������������� ������� �������� ���������� ������� �������� ������� 
            {
                // SFX
            }
        }

        public void FireShock()
        {
            if (m_TurretProperties == null) return; // ��������� ���� ������� = 0 

            if (m_RefireTimer > 0) return; // ��������� ���� ����� ����� ��������� = 0

            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            if (m_Ship.DrawShock(m_TurretProperties.ShockUsage) == false) return;

            ShockMoveAsteroid projectile2 = Instantiate(m_TurretProperties.ShockMoveAsteroid).GetComponent<ShockMoveAsteroid>(); // ������� ������� 

            projectile2.transform.position = transform.position; // ������� ������� �������� , ������ ������� �������
            projectile2.transform.up = transform.up; // ������� ����������� �������� �� ����������� ���� ������� ������� 

            projectile2.SetParentShooter(m_Ship); // ��������� ���� ����� ��� �� �� ��������� ���� ������� �� �������� ��������

            m_RefireTimer = m_TurretProperties.RateOfFire; // ���� ������� ���������������� ������� �������� ���������� ������� �������� ������� 
            {
                // SFX
            }
        }

        public void AssignLoadout(TurretProperties props) // ����� ���������� ������ ������� ������, �������� ����� ������� �������� �����
        {
            if(m_Mode != props.Mode) return; // ���� ����� �������� �������� �� ��������� ����� ��� ������ ������� ��������� ���� ��� �� ����� ���� ������� ����� 

            m_RefireTimer = 0;
            m_TurretProperties = props;
        }
        #endregion
    }
}