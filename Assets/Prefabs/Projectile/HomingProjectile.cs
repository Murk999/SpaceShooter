using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class HomingProjectile : MonoBehaviour
    {
        [SerializeField] private float m_Force;
        [SerializeField] private float m_Radius;

        [SerializeField] private Projectile m_Projectile;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Projectile") == false) return;

            if (collision.attachedRigidbody == null) return;

            Projectile projectile = collision.attachedRigidbody.GetComponent<Projectile>();

            if (projectile == null) return;

            Vector2 dir = transform.position - collision.transform.position;

            float dist = dir.magnitude;

            if (dist < m_Radius)
            {
                Vector2 force = dir.normalized * m_Force * (dist / m_Radius);
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
    }
#endif
}
