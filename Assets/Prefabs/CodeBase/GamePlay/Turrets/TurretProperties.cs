using UnityEngine;

namespace SpaceShooter
{
    public enum TurretMode  // Моды , то какая у нас турель , главная или вторичная
    {
        Primary,
        Secondary,
        Shock
        // Homing
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject // Наследуемся от скриптбл обджекта sealed изолированый класс, не можем наследоваться от него 
    {
        [SerializeField] private TurretMode m_Mode; // ссылка на мод какая турель 
        public TurretMode Mode => m_Mode;

        [SerializeField] private Projectile m_ProjectilePrefab; // Ссылка на снаряд
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        [SerializeField] private ShockMoveAsteroid m_ShockMoveAsteroid;

        public ShockMoveAsteroid ShockMoveAsteroid => m_ShockMoveAsteroid;

        //[SerializeField] private HomingProjectile m_Homing;

        //public HomingProjectile HomingGan => m_Homing;

        [SerializeField] private float m_RateOfFire; // Скорострельность туррели, задержка в секунду 
        public float RateOfFire => m_RateOfFire;

        [SerializeField] private int m_EnergyUsage; // Сколько енергии тратится если енергитическая пушка 
        public int EnergyUsage => m_EnergyUsage;

        [SerializeField] private int m_AmmoUsage; // Сколько патронов тратится если у нас пулемет
        public int AmmoUsage => m_AmmoUsage;

        [SerializeField] private int m_ShockUsage;
        public int ShockUsage => m_ShockUsage;

        //[SerializeField] private int m_HomingUsage;
        //public int HomingUsage => m_ShockUsage;

        [SerializeField] private AudioClip m_LaunchSFX; // Ссылка на звук который будет воспроизводится при запуске снаряда 
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}