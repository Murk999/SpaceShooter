using UnityEngine;

namespace SpaceShooter
{
    public class AIPointPatrols : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;
        public Transform[] moveSpots;
        private int randomSpot;

        private static readonly Color GizmosColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmosColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }
    }
}