using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using SpaceShooter;
using static UnityEngine.UI.CanvasScaler;
using static UnityEngine.RuleTile.TilingRuleOutput;
namespace Common
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private float m_Velocity; // Скорость снаряда

        [SerializeField] private float m_Lifetime; // Время жизни снаряда

        [SerializeField] private int m_Damage; // Урон         

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab; // Эффект взрыва

        [SerializeField] protected GameObject boomSplash;

        protected virtual void OnHit(Destructible destructible) { }
        protected virtual void OnHit(Collider2D collider2D) { }

        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos) { }
                    
        private float m_Timer; // Время жизни эффекта 

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity; // Создаем переменную шаг снаряда = Времени умноженому на скорость 
            Vector2 step = transform.up * stepLength; // Задаем направление снаряда всегда прямо умноженое на шаг снаряда в секунду 

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength); // Добавляем рекаст который смотрит есть  ли кто то по направлению стрельбы

            if (hit)
            {
                OnHit(hit.collider);

                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>(); // Добавляем ссылку на поиск димстрактбл у корневого объекта

                if (dest != null && dest != m_Parent) // Есть декстратбл есть  дестрактебл не на самом себе 
                {
                    dest.ApplyDamage(m_Damage);  // то нананосим урон

                    OnHit(dest); 
                }
                OnProjectileLifeEnd(hit.collider, hit.point); // Наносим по кому попали               
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                OnProjectileLifeEnd(hit.collider, hit.point);

            transform.position += new Vector3(step.x, step.y, 0); // Задаем направление снаряда равное переменной степ по Х и по У
        }

       // Instantiate(boomSplash, transform.position, Quaternion.identity);
        protected Destructible m_Parent;

        public void SetParentShooter(Destructible parent) // Делаем метод , и добавляем дестрактблу имя парент
        {
            m_Parent = parent; // Делаем мПарент = дестрактбл парент что бы в проверки проверит что дестарктб не на самом снаряде
        }
    }
}
