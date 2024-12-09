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
        /// Масса для автоматической установки в риджибади Ригид
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Толкающая вперед сила.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Максимальная линейная скорость 
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Максимальная вращательная скорость. В градусах/сек
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// Сохраненная ссылка на риджитбади Ригид
        /// </summary>
        private Rigidbody2D m_Rigid;

        public  float MaxLinearVelocity => m_MaxLinearVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;
        public Sprite PrewiewImage => m_PreviewImage;

        #region Public API

        /// <summary>
        /// Управление линейной тягой. -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой. -1.0 до +1.0
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
        /// Метод добавления сил кораблю для движения 
        /// </summary>
        private void UpdateRigidBody() // Код перемещения и вращения 
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        #endregion

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode) // Добавляем метод стрельбы с парраметрами мода какая турель у нас стоит 
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        public void FireShock(TurretMode mode2) // Добавляем метод стрельбы с парраметрами мода какая турель у нас стоит 
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode2)
                {
                    m_Turrets[i].FireShock();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy; // Максимальный показатель энергии
        [SerializeField] private int m_MaxAmmo; // Максимальное кол-во патронов
        [SerializeField] private int m_EnergyRegenPerSecond; // Сколько энергии востанавливается в секунду
        [SerializeField] private int m_MaxDush; // Максимальное ускорение
        [SerializeField] private int m_MaxShock;
        [SerializeField] private int m_MaxHoming;

        public GameObject shield;
        public GameObject dushVishual;

        private float m_PrimaryEnergy; // Текущая энергия
        private int m_SecondaryAmmo; // Текущее кол-во патронов
        private float m_PrimaryShock;
        private float m_SecondaryHoming;

        public void AddEnegy(int e) // Метод добавления энергии 
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy); // ограничение мин и максимального показателя энергии
        }

        public void AddAmmo(int ammo) // Метод добавления патронов
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo); // ограничение мин и максимального кол-ва патронов
        }

        public void AddShock(int shock) // Метод добавления патронов
        {
            m_PrimaryShock = Mathf.Clamp(m_PrimaryShock + shock, 0, m_MaxAmmo); // ограничение мин и максимального кол-ва патронов
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

        private void InitOffensive() // Метод инициализируем все эти переменные при старте, метод добавляем в старт 
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

        public bool DrawAmmo(int count) // отнимаем патроны , инт каунт ( количество ) 
        {
            if(count == 0) return true;

            if(m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }

        public bool DrawEnergy(int count) // отнимаем патроны , инт каунт ( количество ) 
        {
            if (count == 0) return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }
            return false;
        }

        public bool DrawShock(int count) // отнимаем патроны , инт каунт ( количество ) 
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
                m_Turrets[i].AssignLoadout(props); // Создали цикл с помощью которого можем менять оружие сколько захотим
            }
        }
    }
}