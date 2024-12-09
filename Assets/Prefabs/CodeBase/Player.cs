using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player> // ������ ����� ������� ����������� �� ���-�� ������ � ������� ������� 
    {
        public static SpaceShip SelectedSpaceShip; // ������ �� ������� ������� ������ �����

        [SerializeField] private int m_NumLivers; // ���������� ������
        
        [SerializeField] private SpaceShip m_PlayerShipPrefab; // ������ �� ������ 
        public SpaceShip ActiveShip => m_Ship;

        private CameraController m_CameraController; // ������ �� ������ 
        private MovementControllers m_MovementController; // ������ �� �������� 
        private Transform m_SpawnPoint;

        public CameraController FollowCamera => m_CameraController;

        public void Construct(CameraController followCamera, MovementControllers shipInputController, Transform spawnPoint)
        {
            m_CameraController = followCamera;
            m_MovementController = shipInputController;
            m_SpawnPoint = spawnPoint;
        }

        private SpaceShip m_Ship; // ������ �� ������� �� �����

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
            //  m_Ship.EventOnDeath.AddListener(OnShopDeath); // ��� ������ ��������� �� ���� �� ������ 
        }
       
        private void OnShopDeath()
        {
            
            m_NumLivers--;  // ��� ������ �������� ����� 

            if (m_NumLivers > 0) // ���� ��� ���� ����� �� ��������� ������
                Respawn();         // ����� �������� ������ 
        }

        private void Respawn() // ������ ������ 
        {
            var newPlayerShip = Instantiate(ShipPrefab, m_SpawnPoint.position, m_SpawnPoint.rotation); // ������� ������ ������� 

            m_Ship = newPlayerShip.GetComponent<SpaceShip>(); // �������� ��������� ( ����������� ���������� �� ������� SpaceShip ) 

            m_CameraController.SetTarget(m_Ship.transform); // �������� ������ m_Ship ��� �� ��� �� ��� ������ 
            m_MovementController.SetTargetShip(m_Ship); // �������� ���������� ������ ������� 

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