using UnityEngine.UI;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupDush : Powerup
    {
        public Dush dushTimer;

        protected override void OnPickedUp(SpaceShip ship)
        {
            dushTimer.isCooldowns = true;
            dushTimer.ResetTimer();
        }

        private void Start()
        {
            dushTimer = GetComponent<Dush>();
            dushTimer = FindAnyObjectByType<Dush>();
        }

        public void Update()
        {
            if (dushTimer.isCooldowns == true)
            {
                dushTimer.DushTimer();
            }
        }
    }
}