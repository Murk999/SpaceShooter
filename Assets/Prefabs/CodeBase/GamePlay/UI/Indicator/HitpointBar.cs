using UnityEngine.UI;
using UnityEngine;

namespace SpaceShooter
{
    public class HitpointBars : MonoBehaviour
    {
        [SerializeField] private Image m_Image;

        private float lastHitPoints;
        private void Update()
        {
            float hitPoints = m_Image.fillAmount = (float) Player.Instance.ActiveShip.HitPoints / (float) Player.Instance.ActiveShip.MaxHitPoints;
            if(hitPoints != lastHitPoints)
            {
                m_Image.fillAmount = hitPoints;
                lastHitPoints = hitPoints;
            }
        }
    }
}