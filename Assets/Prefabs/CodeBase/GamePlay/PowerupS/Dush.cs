using UnityEngine.UI;
using UnityEngine;

namespace SpaceShooter
{
    public class Dush : MonoBehaviour
    {
        public float cooldowns;

        [HideInInspector] public bool isCooldowns;

        public Image dushImage;

        private SpaceShip ship;

        private void Start()
        {
            ship = FindObjectOfType<SpaceShip>();
            dushImage = GetComponent<Image>();

            isCooldowns = false;

            gameObject.SetActive(false);
        }

        private void Update()
        {
            ship = GetComponent<SpaceShip>();

            dushImage = GetComponent<Image>();
        }

        public void DushTimer()
        {
            if (isCooldowns)
            {
                dushImage.fillAmount -= 1 / cooldowns * Time.deltaTime;
                if (dushImage.fillAmount <= 0)
                {
                    dushImage.fillAmount = 1;
                    isCooldowns = false;
                    gameObject.SetActive(false);
                }
            }
        }

        public void ResetTimer()
        {
            dushImage.fillAmount = 1;
            gameObject.SetActive(true);
        }
    }
}