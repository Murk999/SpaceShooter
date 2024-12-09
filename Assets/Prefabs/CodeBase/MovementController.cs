using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class MovementControllers : MonoBehaviour
    {
        public enum ControlMode // Создаем енам перечесление какие есть вводы , клавиатура и мобильынй ввод
        {
            Keyboard,
            Mobile
        }

        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship; // Синтаксический сахар с помощью => вместо {} 
    
        [SerializeField] private ControlMode m_ControlMode; // Приватная переменная которая показывает как обращаться с клавы и джостика

        public void Construct(VirtualGamePads1 virtualGamePad)
        {
            m_VirtualGamePad = virtualGamePad;
        }

        private SpaceShip m_TargetShip;

        private VirtualGamePads1 m_VirtualGamePad;

        private void Start()
        {
            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_VirtualGamePad.VirtualJoystick.gameObject.SetActive(false);
                m_VirtualGamePad.MobileFirePrimary.gameObject.SetActive(false);
                m_VirtualGamePad.MobileFireSecondary.gameObject.SetActive(false);
            }                                     
            else
            {
                m_VirtualGamePad.VirtualJoystick.gameObject.SetActive(true);
                m_VirtualGamePad.MobileFirePrimary.gameObject.SetActive(true);
                m_VirtualGamePad.MobileFireSecondary.gameObject.SetActive(true);
            }
            
            ControlMobile();

            /*   if (Application.isMobilePlatform)
               {

                  m_ControlMode = ControlMode.Keyboard;
                  m_MobileJoystick.gameObject.SetActive(true);
               }
               else
               {
                   m_ControlMode = ControlMode.Keyboard;

                   m_MobileJoystick.gameObject.SetActive(true);
               }
              */
        }

        private void Update() // Все управление прописываем в Апдейте 
        {
            if (m_TargetShip == null) return;

            if (m_ControlMode == ControlMode.Keyboard) // Если ввод с клавы то метод управления с клавы
                ControlKeyboard();
            else if (m_ControlMode == ControlMode.Mobile)
                ControlMobile();
        }

        private void ControlMobile()
        {
            Vector3 dir = m_VirtualGamePad.VirtualJoystick.Value; // Получаем направлнеи джойстика 

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up); // Если направление карабля  = направлению стика то ДОТ(пересечение) будет равен 1
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right); // Сколярное произведение поворота = направлению стика то ДОТ(пересечение) будет равен 1

            if (m_VirtualGamePad.MobileFirePrimary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (m_VirtualGamePad.MobileFireSecondary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = Mathf.Max(0, dot); // Беремемаксимальное значение между пересчением ДОТ что бы у нас небыло отрицательного значения , что бы не двигались противоположно 
            m_TargetShip.TorqueControl = -dot2; // Минусуем пересечение что бы корабль не поворачивался в противоположную сторону от стика
        }

        private void ControlKeyboard()
        {
           float thrust = 0;

           float torque = 0;

            if (Input.GetKey(KeyCode.W))
            {
                thrust = 1.0f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                thrust = -1.0f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                torque = 1.0f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                torque = -1.0f;
            }

            if(Input.GetKey(KeyCode.Space)) // Если нажимаем на пробел то стреляем обычной пушкой
            {
                m_TargetShip.Fire(TurretMode.Primary); // Мод обычная пушка
            }
            if (Input.GetKey(KeyCode.X)) // Если нажимаем на Х то стреляем второй  пушкой
            {
                m_TargetShip.Fire(TurretMode.Secondary); // Вторая пушка
            }
            if (Input.GetKey(KeyCode.C)) // Если нажимаем на Х то стреляем второй  пушкой
            {
                m_TargetShip.FireShock(TurretMode.Shock); // Вторая пушка
            }

            m_TargetShip.ThrustControl = thrust; // Наследуемся из SHip и присваеваем значение контролерам 
            m_TargetShip.TorqueControl = torque; // Наследуемся из SHip и присваеваем значение контролерам 
        }

       /* public void SetTargetShip(SpaceShip ship)
        {
            m_TargetShip = ship;
        }*/
    }
}