using UnityEngine.UI;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupShield : Powerup
    {
        public Shield shieldTimer;
        
        protected override void OnPickedUp(SpaceShip ship)
        {    
            shieldTimer.isCooldown = true;
            shieldTimer.ResetTimer();   
        }

        private void Start()
        {
            shieldTimer = GetComponent<Shield>();
            shieldTimer = FindObjectOfType<Shield>();
        }

        private void Update()
        {
            if (shieldTimer.isCooldown == true)
            {
                shieldTimer.ShieldTimer();
            }
        }
    }
 }