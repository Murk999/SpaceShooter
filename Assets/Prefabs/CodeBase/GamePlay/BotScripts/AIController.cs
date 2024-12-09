using UnityEngine;
using Common;

namespace SpaceShooter
{
    [RequireComponent(typeof(BotsShips))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehabiour // Какое поведения коробля
        {
            Null, // Нет поведения 
            Patrol // Патрулирует корабль 
        }

        [SerializeField] private AIBehabiour m_AIBehaviour; // Переменная хранит тип поведения, что бы можно было добавить типы поведения 

        [SerializeField] private AIPointPatrols m_PatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;  // Скорость перемещения 

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular; // Скорость вращения 

        [SerializeField] private float m_RandomSelectMovePointTime; // Переменная изменения позиции

        [SerializeField] private float m_FindNewTargetTime;  // Изменение цели 

        [SerializeField] private float m_ShootDelay;  // Изменение стрельбы 

        [SerializeField] private float m_EvadeRayLength;   // Длина Рейкаста 

        private BotsShips m_SpaceShip;  // Ссылка на корабль 

        private Vector3 m_MovePosition;  // Вектор3 мувепозишион движение корабля , куда двигается корабль 

        private Destructible m_SelectedTarget; // Выбранная цель 

        private Timer m_RandomizeDirectionTimer; // Добавляем ссылку на скрипт таймер 
        private Timer m_FireTimer; // Добавляем ссылку на скрипт таймер 
        private Timer m_FindNewTargetTimer; // Добавляем ссылку на скрипт таймер 
        
        
        private float m_waitTime;
        public float m_startWaitTime;
        public Transform[] moveSpots;
        private int randomSpot;

        private void Start()
        {   
            m_SpaceShip = GetComponent<BotsShips>();
        
            InitTimers(); // Включаем таймеры

            m_waitTime = m_startWaitTime;
            randomSpot = Random.Range(0, moveSpots.Length);
        }

        private void Update()
        {
            UpdateTimers(); // Обновляем таймеры

            UpdateAI(); // Добавляем метод в апдейта проверка что делает корабль 
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
            if (m_AIBehaviour == AIBehabiour.Patrol) // Проверяем если поведение БОта патрулирование 
            {
                UpdateBehaviourPatrol();
            }
        }

        public void UpdateBehaviourPatrol() // Обновляем поведение корабля патруля БОТА 
        {
            ActionFindNewMovePosition(); // Поиск новой позиции 
            ActionContolShip(); // Управление тягой корабля 
            ActionFindNewAttackTarget(); // Поиск новой цели для атаки
            ActionFire(); // Бот стреляет
            ActionEvadeCollision(); // Уворот бота от препятсвий 
        }

        private void ActionFindNewMovePosition() // Поиск новой позиции 
        {
            if (m_AIBehaviour == AIBehabiour.Patrol) // Првоеряем поведение бота
            {
                if (m_SelectedTarget != null) // Если выбраный таргет есть на сцене
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
                        
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius; // Если дистанция от точки патрулирования меньше чем радиус , значит мы в круге 

                        if (isInsidePatrolZone == true) // Если мы в зоне
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
                            m_MovePosition = m_PatrolPoint.transform.position; // Если мы не в зоне , то направление в зону
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
        private void ActionContolShip() // Управление тягой корабля 
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;

            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;

        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship) // Статичный метод который принимает 2 параметра это целевая позиция  и позиция самого корабля
        {
            // Перевод в локальные координаты 
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward); // Получаем угол между двумя векторами 

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE; // Скорость поворота

            return -angle;
        }
        private void ActionFindNewAttackTarget() // Поиск новой цели для атаки 
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {               
                {
                    m_SelectedTarget = FindNearestDestructibleTarget();

                    m_FindNewTargetTimer.Start(m_ShootDelay);
                }
            }
        }
        private void ActionFire() // Бот стреляет
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

        public Destructible FindNearestDestructibleTarget() // поиск ближайшей цели , ближайшего пути 
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

        private void SetPatrolBehaviour(AIPointPatrols point) // Если бот замменил помедение или заспавнился новый бот то мы должны задать ему стартовые параметры 
        {
            m_AIBehaviour = AIBehabiour.Patrol;
            m_PatrolPoint = point;
        }

        #endregion
    }
}