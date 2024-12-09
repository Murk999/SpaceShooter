using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class Shield : MonoBehaviour
    {
        public float cooldown;

        [HideInInspector] public bool isCooldown;

        public Image shieldImage;

        private SpaceShip ship;
        
        private void Start()
        {
            ship = FindObjectOfType<SpaceShip>();
            shieldImage = GetComponent<Image>();

            isCooldown = false;

            gameObject.SetActive(false);
        }
        private void Update()
        {
            ship = GetComponent<SpaceShip>();

            shieldImage = GetComponent<Image>();
        }

        public void ShieldTimer()
        {
            if (isCooldown)
            {
                shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime;
                if (shieldImage.fillAmount <= 0)
                {
                    shieldImage.fillAmount = 1;
                    isCooldown = false;
                    gameObject.SetActive(false);
                }
            }
        }
    
        public void ResetTimer()
        {
            shieldImage.fillAmount = 1;
            gameObject.SetActive(true);
        }
    }
}