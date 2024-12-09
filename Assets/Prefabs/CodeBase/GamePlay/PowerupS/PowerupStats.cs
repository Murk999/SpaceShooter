using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            AddShield,
            AddDush,
            AddShock
        }

        [SerializeField] private EffectType m_EffectType;

        [SerializeField] private float m_Value;

        private SpaceShip m_Ship;
        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
                ship.AddEnegy((int) m_Value);

            if (m_EffectType == EffectType.AddAmmo)
                ship.AddAmmo((int) m_Value);

            if (m_EffectType == EffectType.AddShield)
            {
                ship.AddShield(transform.position);
            }

            if(m_EffectType == EffectType.AddDush)
            {
                ship.AddDush((int)m_Value);
            }

            if (m_EffectType == EffectType.AddShock)
            {
                ship.AddShock((int)m_Value);
            }
        }
    }
}