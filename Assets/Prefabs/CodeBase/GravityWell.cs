using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWells : MonoBehaviour
    {
        [SerializeField] private float m_Force; // Сила притяжения 
        [SerializeField] private float m_Radius; // Зона откуда начинается притяжение

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return; // Если объект не имеет риджетбади то он не притягивается 

            Vector2 dir = transform.position - collision.transform.position; // задаем направление , из позиции к позиции колизии( притягиваем )

            float dist = dir.magnitude; // Создаем переменную дистанция которая равна направлению 

            if (dist < m_Radius) // Проверяем если дистанция меньше радиуса тогда мы
            {
                Vector2 force = dir.normalized * m_Force * (dist / m_Radius); // Тогда мы создаем новое движение ( усиление притяжения , чем ближе к объекту тем сильнее притяжение 
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force); // Прибавляем силу к Риджитбади
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }

    #endif
    }
}