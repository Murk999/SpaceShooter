using UnityEngine;

namespace SpaceShooter
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Prefab")]  // —сылки  на префабы создани€ на сцене
        [SerializeField] private CameraController m_FollowCameraPrefab;
        [SerializeField] private Player m_PlayerPrefab;
        [SerializeField] private MovementControllers m_ShipInputControllerPrefab; // ShipInputContoller 
        [SerializeField] private VirtualGamePads1 m_VirtualGamePadPrefab;
        [SerializeField] private Transform m_SpawnPoint;

        public Player Spawn()
        {
            CameraController followCamera = Instantiate(m_FollowCameraPrefab);
            VirtualGamePads1 virtualGamePad = Instantiate(m_VirtualGamePadPrefab);

            MovementControllers shipInputController = Instantiate(m_ShipInputControllerPrefab);
            shipInputController.Construct(virtualGamePad);

            Player player = Instantiate(m_PlayerPrefab);
            player.Construct(followCamera, shipInputController, m_SpawnPoint);

            return player;
        }
    }  
}