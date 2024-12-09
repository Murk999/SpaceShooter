using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;  // Ссылка за кем следит камера

        [SerializeField] private float m_InterpolationLinear; // Скорость интерполяции 

        [SerializeField] private float m_InterpolationAngular; // Угловая скорость интерполяции 

        [SerializeField] private float m_CameraZOffset; // Смещение по Оси Зет

        [SerializeField] private float m_ForwardOffset; // Смещение по направлению

        private void FixedUpdate()
        {
            if (m_Target == null) return; // Проверяем не равны ди объекты 0 всегда, что бы небыло ошибки проверяем жива ли цель и камера 

            Vector2 camPos = transform.position; // Позиция камеры
            Vector2 targetPos = m_Target.position + m_Target.transform.up * m_ForwardOffset; // Смещение камеры относительно корабля 
            Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, m_InterpolationLinear * Time.deltaTime); // Новая позиция камеры относительно движения корабля, привязанная к кораблю линейная интерпояция

            transform.position = new Vector3(newCamPos.x, newCamPos.y, m_CameraZOffset); // Задаем позицию камеры 

            if (m_InterpolationAngular > 0) // Проверяем скорость повората
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, m_Target.rotation,
                                                                               m_InterpolationAngular * Time.deltaTime); // Делаем плавный поворот с помощью квантериона и сИнтерполяции поворота 
            }
        }

        public void SetTarget(Transform newTarget) // Создаем публичный метод который позволяет задать новую позицию для слежения для камеры
        {
            m_Target = newTarget;
        }
    }
}