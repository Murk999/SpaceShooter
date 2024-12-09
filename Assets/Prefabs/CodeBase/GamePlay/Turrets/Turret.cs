using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode; // Мод какое уродие у нас взято
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties; // Ссылка на экземпляр класса

        private float m_RefireTimer; // Таймер повторного выстрела

        public bool CanFire => m_RefireTimer <= 0; // Свойство 

        private SpaceShip m_Ship; // Ссылка на корабль 

        private BotsShips m_BotShip; // Ссылка на корабль бота

        #region UNITY EVENT
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>(); // Получаем ссылку на корабль главный объект при старте
            m_BotShip = transform.root.GetComponent<BotsShips>();
        }

        private void Update()
        {
            if(m_RefireTimer > 0)
            m_RefireTimer -= Time.deltaTime;  // Уравневаем стрельбу к делтатайм
        }

        // Public API

        public void Fire()
        {
            if (m_TurretProperties == null) return; // Проверяем если снаряды = 0 

            if(m_RefireTimer > 0) return; // Проверяем если время перед выстрелом = 0

            if(m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            if (m_Ship.DrawShock(m_TurretProperties.ShockUsage) == false) return;
            //if(m_Ship.DrawHoming(m_TurretProperties.HomingUsage) == false) return;

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>(); // Создаем снаряды 
            
            projectile.transform.position = transform.position; // Создаем позицию снарядов , равной позиции корабля
            projectile.transform.up = transform.up; // Создаем направления снарядов по напрвылению куда смотрит корабль 

            projectile.SetParentShooter(m_Ship); // добавляем сюда метод что бы не наносился урон кораблю из которого стреляем
            projectile.SetParentShooter(m_BotShip); // добавляем сюда метод что бы не наносился урон кораблю из которого стреляем

            m_RefireTimer = m_TurretProperties.RateOfFire; // Даем значени скорострельности которое задается Пропертиес скрипте скриптбл обджект 
            {
                // SFX
            }
        }
        public void FireBots()
        {
            if (m_TurretProperties == null) return; // Проверяем если снаряды = 0 

            if (m_RefireTimer > 0) return; // Проверяем если время перед выстрелом = 0

            if (m_BotShip.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
            if (m_BotShip.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            if (m_BotShip.DrawShock(m_TurretProperties.ShockUsage) == false) return;

            Projectile projectile3 = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>(); // Создаем снаряды 

            projectile3.transform.position = transform.position; // Создаем позицию снарядов , равной позиции корабля
            projectile3.transform.up = transform.up; // Создаем направления снарядов по напрвылению куда смотрит корабль 

            projectile3.SetParentShooter(m_BotShip); // добавляем сюда метод что бы не наносился урон кораблю из которого стреляем

            m_RefireTimer = m_TurretProperties.RateOfFire; // Даем значение скорострельности которое задается Пропертиес скрипте скриптбл обджект 
            {
                // SFX
            }
        }

        public void FireShock()
        {
            if (m_TurretProperties == null) return; // Проверяем если снаряды = 0 

            if (m_RefireTimer > 0) return; // Проверяем если время перед выстрелом = 0

            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            if (m_Ship.DrawShock(m_TurretProperties.ShockUsage) == false) return;

            ShockMoveAsteroid projectile2 = Instantiate(m_TurretProperties.ShockMoveAsteroid).GetComponent<ShockMoveAsteroid>(); // Создаем снаряды 

            projectile2.transform.position = transform.position; // Создаем позицию снарядов , равной позиции корабля
            projectile2.transform.up = transform.up; // Создаем направления снарядов по напрвылению куда смотрит корабль 

            projectile2.SetParentShooter(m_Ship); // добавляем сюда метод что бы не наносился урон кораблю из которого стреляем

            m_RefireTimer = m_TurretProperties.RateOfFire; // Даем значени скорострельности которое задается Пропертиес скрипте скриптбл обджект 
            {
                // SFX
            }
        }

        public void AssignLoadout(TurretProperties props) // Метод назначения другие свойств оружию, например когда корабль подобрал бонус
        {
            if(m_Mode != props.Mode) return; // Если хотим свойство положить во вторичную пушку так нельзя поэтому првоеряем если Мод не равен Моду который задан 

            m_RefireTimer = 0;
            m_TurretProperties = props;
        }
        #endregion
    }
}