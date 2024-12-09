using UnityEngine;
using Common;

namespace SpaceShooter
{
    [RequireComponent(typeof(BotsShips))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehabiour // ����� ��������� �������
        {
            Null, // ��� ��������� 
            Patrol // ����������� ������� 
        }

        [SerializeField] private AIBehabiour m_AIBehaviour; // ���������� ������ ��� ���������, ��� �� ����� ���� �������� ���� ��������� 

        [SerializeField] private AIPointPatrols m_PatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;  // �������� ����������� 

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular; // �������� �������� 

        [SerializeField] private float m_RandomSelectMovePointTime; // ���������� ��������� �������

        [SerializeField] private float m_FindNewTargetTime;  // ��������� ���� 

        [SerializeField] private float m_ShootDelay;  // ��������� �������� 

        [SerializeField] private float m_EvadeRayLength;   // ����� �������� 

        private BotsShips m_SpaceShip;  // ������ �� ������� 

        private Vector3 m_MovePosition;  // ������3 ������������ �������� ������� , ���� ��������� ������� 

        private Destructible m_SelectedTarget; // ��������� ���� 

        private Timer m_RandomizeDirectionTimer; // ��������� ������ �� ������ ������ 
        private Timer m_FireTimer; // ��������� ������ �� ������ ������ 
        private Timer m_FindNewTargetTimer; // ��������� ������ �� ������ ������ 
        
        
        private float m_waitTime;
        public float m_startWaitTime;
        public Transform[] moveSpots;
        private int randomSpot;

        private void Start()
        {   
            m_SpaceShip = GetComponent<BotsShips>();
        
            InitTimers(); // �������� �������

            m_waitTime = m_startWaitTime;
            randomSpot = Random.Range(0, moveSpots.Length);
        }

        private void Update()
        {
            UpdateTimers(); // ��������� �������

            UpdateAI(); // ��������� ����� � ������� �������� ��� ������ ������� 
/*
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, m_NavigationLinear * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (m_waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    m_waitTime = m_startWaitTime;
                }
                else
                {
                    m_waitTime -=Time.deltaTime;
                }
            }
*/
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehabiour.Patrol) // ��������� ���� ��������� ���� �������������� 
            {
                UpdateBehaviourPatrol();
            }
        }

        public void UpdateBehaviourPatrol() // ��������� ��������� ������� ������� ���� 
        {
            ActionFindNewMovePosition(); // ����� ����� ������� 
            ActionContolShip(); // ���������� ����� ������� 
            ActionFindNewAttackTarget(); // ����� ����� ���� ��� �����
            ActionFire(); // ��� ��������
            ActionEvadeCollision(); // ������ ���� �� ���������� 
        }

        private void ActionFindNewMovePosition() // ����� ����� ������� 
        {
            if (m_AIBehaviour == AIBehabiour.Patrol) // ��������� ��������� ����
            {
                if (m_SelectedTarget != null) // ���� �������� ������ ���� �� �����
                {
                    m_MovePosition = m_SelectedTarget.transform.position;

                    transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, m_NavigationLinear * Time.deltaTime);

                    if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
                    {
                        if (m_waitTime <= 0)
                        {
                            randomSpot = Random.Range(0, moveSpots.Length);
                            m_waitTime = m_startWaitTime;
                        }
                        else
                        {
                            m_waitTime -= Time.deltaTime;
                        }
                    }
                }
                else
                {
                    if (m_PatrolPoint != null)
                    {
                        
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius; // ���� ��������� �� ����� �������������� ������ ��� ������ , ������ �� � ����� 

                        if (isInsidePatrolZone == true) // ���� �� � ����
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position; // ���� �� �� � ���� , �� ����������� � ����
                        }
                        
                    }
                }
            }
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }
        private void ActionContolShip() // ���������� ����� ������� 
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;

            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;

        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship) // ��������� ����� ������� ��������� 2 ��������� ��� ������� �������  � ������� ������ �������
        {
            // ������� � ��������� ���������� 
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward); // �������� ���� ����� ����� ��������� 

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE; // �������� ��������

            return -angle;
        }
        private void ActionFindNewAttackTarget() // ����� ����� ���� ��� ����� 
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {               
                {
                    m_SelectedTarget = FindNearestDestructibleTarget();

                    m_FindNewTargetTimer.Start(m_ShootDelay);
                }
            }
        }
        private void ActionFire() // ��� ��������
        {
            if (m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);

                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }

        public Destructible FindNearestDestructibleTarget() // ����� ��������� ���� , ���������� ���� 
        {
            float maxDist = 8;
            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<BotsShips>() == m_SpaceShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            return potentialTarget;
        }

        #region Timers

        private void InitTimers() 
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        private void UpdateTimers() 
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        private void SetPatrolBehaviour(AIPointPatrols point) // ���� ��� �������� ��������� ��� ����������� ����� ��� �� �� ������ ������ ��� ��������� ��������� 
        {
            m_AIBehaviour = AIBehabiour.Patrol;
            m_PatrolPoint = point;
        }

        #endregion
    }
}