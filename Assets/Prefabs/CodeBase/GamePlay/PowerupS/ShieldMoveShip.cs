using UnityEngine;

namespace SpaceShooter
{
    public class ShieldMoveShips : MonoBehaviour
    {
        public Transform target; // ���� � ������� ����� ���������
        private SpaceShip ship; // ������ �� ��� ������ ������

        public float speed; // �������� ��������� � ������� 

        private void Start()
        {
            ship = FindObjectOfType<SpaceShip>();
            
          //  ship = GetComponent<SpaceShip>();
         //   target = GameObject.Find("ship").GetComponent<Transform>();
        }

        void Update()
        {
            float step = speed * Time.deltaTime; // ������ ���� ����� �������� * ����� �����.
            if (ship != null)
            {                                    
                transform.position = Vector3.MoveTowards(ship.transform.position, target.position , step);        
            }
            return;
        }
    }
}