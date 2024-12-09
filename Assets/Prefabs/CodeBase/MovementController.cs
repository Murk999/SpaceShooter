using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class MovementControllers : MonoBehaviour
    {
        public enum ControlMode // ������� ���� ������������ ����� ���� ����� , ���������� � ��������� ����
        {
            Keyboard,
            Mobile
        }

        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship; // �������������� ����� � ������� => ������ {} 
    
        [SerializeField] private ControlMode m_ControlMode; // ��������� ���������� ������� ���������� ��� ���������� � ����� � ��������

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

        private void Update() // ��� ���������� ����������� � ������� 
        {
            if (m_TargetShip == null) return;

            if (m_ControlMode == ControlMode.Keyboard) // ���� ���� � ����� �� ����� ���������� � �����
                ControlKeyboard();
            else if (m_ControlMode == ControlMode.Mobile)
                ControlMobile();
        }

        private void ControlMobile()
        {
            Vector3 dir = m_VirtualGamePad.VirtualJoystick.Value; // �������� ���������� ��������� 

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up); // ���� ����������� �������  = ����������� ����� �� ���(�����������) ����� ����� 1
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right); // ��������� ������������ �������� = ����������� ����� �� ���(�����������) ����� ����� 1

            if (m_VirtualGamePad.MobileFirePrimary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (m_VirtualGamePad.MobileFireSecondary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = Mathf.Max(0, dot); // ������������������ �������� ����� ����������� ��� ��� �� � ��� ������ �������������� �������� , ��� �� �� ��������� �������������� 
            m_TargetShip.TorqueControl = -dot2; // �������� ����������� ��� �� ������� �� ������������� � ��������������� ������� �� �����
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

            if(Input.GetKey(KeyCode.Space)) // ���� �������� �� ������ �� �������� ������� ������
            {
                m_TargetShip.Fire(TurretMode.Primary); // ��� ������� �����
            }
            if (Input.GetKey(KeyCode.X)) // ���� �������� �� � �� �������� ������  ������
            {
                m_TargetShip.Fire(TurretMode.Secondary); // ������ �����
            }
            if (Input.GetKey(KeyCode.C)) // ���� �������� �� � �� �������� ������  ������
            {
                m_TargetShip.FireShock(TurretMode.Shock); // ������ �����
            }

            m_TargetShip.ThrustControl = thrust; // ����������� �� SHip � ����������� �������� ����������� 
            m_TargetShip.TorqueControl = torque; // ����������� �� SHip � ����������� �������� ����������� 
        }

       /* public void SetTargetShip(SpaceShip ship)
        {
            m_TargetShip = ship;
        }*/
    }
}