using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary"; // Столкновения что бы с невидимыми объектами не наносило урон 

        [SerializeField] private float m_VelocityDamageModifier; // Переменная модификации урона зависящей от скорости 

        [SerializeField] private float m_DamageConstatnt; // Урон который 100% будет получен 

        private void OnCollisionEnter2D(Collision2D collision) // Проверка столкновения 
        {
            if (collision.transform.tag == IgnoreTag) return; // Если есть Тег  WorldBoundary то скрипт нечего не делает 

            var destructable = transform.root.GetComponent<Destructible>(); // Создаем переменную для проверки есть ли на объекте привязка к Destructable

            if (destructable != null) // Отнимаем у текущего ХП , число в дамаге ) // И если есть Destructable то
            {
                destructable.ApplyDamage((int)m_DamageConstatnt + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude)); // Наносим урон по формуле меняющийся в зависимости от скорости 
            }         
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("shieldShip"))
            {
                var destructable = transform.root.GetComponent<Destructible>(); // Создаем переменную для проверки есть ли на объекте привязка к Destructable
                destructable.ResistDamage();
            }
        }

        private void OnTriggerExit2D(UnityEngine.Collider2D collision)
        {
            if (collision.gameObject.CompareTag("shieldShip"))
            {
                var destructable = transform.root.GetComponent<Destructible>();
                destructable.ResetHP();
            }
        }
    }
}