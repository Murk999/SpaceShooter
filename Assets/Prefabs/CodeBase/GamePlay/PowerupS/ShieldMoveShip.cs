using UnityEngine;

namespace SpaceShooter
{
    public class ShieldMoveShips : MonoBehaviour
    {
        public Transform target; // Цель к которой будем двигаться
        private SpaceShip ship; // Ссылка за кем следит камера

        public float speed; // Скорость персонажа в секунду 

        private void Start()
        {
            ship = FindObjectOfType<SpaceShip>();
            
          //  ship = GetComponent<SpaceShip>();
         //   target = GameObject.Find("ship").GetComponent<Transform>();
        }

        void Update()
        {
            float step = speed * Time.deltaTime; // Размер шага равен скорость * время кадра.
            if (ship != null)
            {                                    
                transform.position = Vector3.MoveTowards(ship.transform.position, target.position , step);        
            }
            return;
        }
    }
}