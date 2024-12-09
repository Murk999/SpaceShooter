using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ограничитель позиции . Работает в связке со скриптом LevelBoundary если такой есть на сцене.
    /// Кидается на объект который нужно ограничить 
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return; // Проверка если наш синглтон есть на сцене , то нечего не делаем 

            var lb = LevelBoundary.Instance; // Создаем ссылку на левелбаундари
            var r = lb.Radius; // Ссылка на радиус уровня 

            if(transform.position.magnitude > r) // Проверяем если наша позиция больше радиуса уровная то выполняем проверку и следующий код 
            {
                if(lb.LimitMode == LevelBoundary.Mode.Limit) // Если мод лимит то
                {
                    transform.position = transform.position.normalized * r; // То мы просто останавливаем объект
                }
                if (lb.LimitMode == LevelBoundary.Mode.Teleport) // если мод телепорт то
                {
                    transform.position = -transform.position.normalized * r; // То перемещаем наш объект в противоположную сторону 
                }
            }
        }
    }
}