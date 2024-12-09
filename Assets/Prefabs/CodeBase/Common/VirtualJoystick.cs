using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Common
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler  // Добавляем методы которые предоставляют интерфейсы нажатия джостика и отпускания 
    {
        [SerializeField] private Image m_JoyBack; // Фон джостика
        [SerializeField] private Image m_JoyStick; // Стикер , сам джостик 

        public Vector3 Value {  get; private set; }

        public void OnDrag(PointerEventData eventData)  // Код будет выполняться на UI элементах на которых этот скрипт 
        {
            Vector2 position = Vector2.zero; // Позиция равна 0 тоесть центр стика

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_JoyBack.rectTransform, eventData.position, eventData.pressEventCamera, out position); // Меняем позицию стика относительно области стика

            position.x = (position.x / m_JoyBack.rectTransform.sizeDelta.x); // Делим позицию стика на размер областики стика по Х
            position.y = (position.y / m_JoyBack.rectTransform.sizeDelta.y); // Делим позицию стика на размер областики стика по У

            position.x = position.x * 2 - 1; // Нормалихуем вектор от -1.0 до 1.0 по Х внутри области стика
            position.y = position.y * 2 - 1; // Нормалихуем вектор от -1.0 до 1.0 по У внутри области стика

            Value = new Vector3(position.x, position.y, 0);

            if (Value.magnitude > 1 ) // Нормализуем вектора по всему игровому экрану от -1.0 до 1.0 
            {
                Value = Value.normalized; // Нормализуем вектора по всему игровому экрану от -1.0 до 1.0 
            }

            float offsetX = m_JoyBack.rectTransform.sizeDelta.x / 2 - m_JoyStick.rectTransform.sizeDelta.x / 2; // задаем перемещение стику по Х
            float offsetY = m_JoyBack.rectTransform.sizeDelta.y / 2 - m_JoyStick.rectTransform.sizeDelta.y / 2; // задаем перемещение стику по У

            m_JoyStick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY); // Задаем границы стику по Х и У в пределах области стика 

            Debug.Log(Value);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData); // Запускаем событие при нажатии на стик 
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;
            m_JoyStick.rectTransform.anchoredPosition = Vector3.zero;  // Возвращаем стик в центр при отпускании стика 
        }
    }
}

