using Common;
using UnityEditor;
using UnityEngine;

namespace SpaceShooter
{
    public class ShockMoveAsteroid : MonoBehaviour
    {
        [SerializeField] private float m_Velocity; // Скорость снаряда

        [SerializeField] private float m_Lifetime; // Время жизни снаряда

        [SerializeField] private int m_Damage; // Урон 

        [SerializeField] private float m_Radius;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab; // Эффект взрыва

        [SerializeField] private GameObject boomSplash;

        [SerializeField] private GameObject shockGravityWell;

        public float Radius => m_Radius;

        private float m_Timer; // Время жизни эффекта 

        private void Start()
        {

            
        }

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity; // Создаем переменную шаг снаряда = Времени умноженому на скорость 

            Vector2 step = transform.up * stepLength; // Задаем направление снаряда всегда прямо умноженое на шаг снаряда в секунду 

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength); // Добавляем рекаст который смотрит есть  ли кто то по направлению стрельбы

            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>(); // Добавляем ссылку на поиск димстрактбл у корневого объекта

                if (dest != null && dest != m_Parent) // Есть декстратбл есть  дестрактебл не на самом себе 
                {
                    dest.ApplyDamage(m_Damage);  // то нананосим урон
                }
                OnProjectileLifeEnd(hit.collider, hit.point); //Наносим по кому попали 
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0); // Задаем направление снаряда равное переменной степ по Х и по У
                                                                  //  transform.position = Vector2.MoveTowards(transform.position, asteroid.transform.position, stepLength);

        }
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos) // Метод что мы делаем при попадании и кт ополучает урон 
        {
            Destroy(gameObject);
            Instantiate(boomSplash, transform.position, Quaternion.identity);
            Instantiate(shockGravityWell, transform.position, Quaternion.identity);
        }

        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent) // Делаем метод , и добавляем дестрактблу имя парент
        {
            m_Parent = parent; // Делаем мПарент = дестрактбл парент что бы в проверки проверит что дестарктб не на самом снаряде
        }

        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }
}