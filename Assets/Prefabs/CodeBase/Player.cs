using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player> // Скрипт плеер котоырй ответсвенен за кол-во жизней и респавн корабля 
    {
        public static SpaceShip SelectedSpaceShip; // Ссылка на корабль который выбрал игрок

        [SerializeField] private int m_NumLivers; // Количество жизней
        
        [SerializeField] private SpaceShip m_PlayerShipPrefab; // ссылка на префаб 
        public SpaceShip ActiveShip => m_Ship;

        private CameraController m_CameraController; // Ссылка на камеру 
        private MovementControllers m_MovementController; // Ссылка на котролер 
        private Transform m_SpawnPoint;

        public CameraController FollowCamera => m_CameraController;

        public void Construct(CameraController followCamera, MovementControllers shipInputController, Transform spawnPoint)
        {
            m_CameraController = followCamera;
            m_MovementController = shipInputController;
            m_SpawnPoint = spawnPoint;
        }

        private SpaceShip m_Ship; // ссылка на корабли на сцене

        private int m_Score;

        private int m_NumKills;

        public int Score => m_Score;
        public int NumKills => m_NumKills;
        public int NumLives => m_NumLivers;

        public SpaceShip ShipPrefab
        {
            get
            {
                if(SelectedSpaceShip == null)
                {
                    return m_PlayerShipPrefab;
                }
                else
                {
                    return SelectedSpaceShip;
                }
            }
        }

        private void Start()
        {
            Respawn();
            //  m_Ship.EventOnDeath.AddListener(OnShopDeath); // При старте првоеряем не умер ли объект 
        }
       
        private void OnShopDeath()
        {
            
            m_NumLivers--;  // при смерти отнимаем жизнь 

            if (m_NumLivers > 0) // Если еще есть жизни то респавним игрока
                Respawn();         // метод респавна игрока 
        }

        private void Respawn() // Ресавн Игрока 
        {
            var newPlayerShip = Instantiate(ShipPrefab, m_SpawnPoint.position, m_SpawnPoint.rotation); // Создаем префаб корабля 

            m_Ship = newPlayerShip.GetComponent<SpaceShip>(); // Получяем компанент ( завполнение переменной из скрипта SpaceShip ) 

            m_CameraController.SetTarget(m_Ship.transform); // Передаем камере m_Ship что бы она за ним следил 
            m_MovementController.SetTargetShip(m_Ship); // Передаем управление новому кораблю 

            m_Ship.EventOnDeath.AddListener(OnShopDeath);
        }

        public void AddKill()
        {
            m_NumKills += 1;
        }

        public void AddScore(int num)
        {
            m_Score += num;
        }
    }
}