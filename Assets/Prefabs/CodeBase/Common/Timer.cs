namespace Common
{
    public class Timer
    {
        private float m_CurrenyTime; // Текущее время

        public bool IsFinished => m_CurrenyTime <= 0; // Закончился таймер или нет 

        public Timer(float startTime)  // Метод принимающий значение стартТайм
        {
            Start(startTime);
        }

        public void Start(float startTime) // Метод при старте делаем текущее время равным стартТайм
        {
            m_CurrenyTime = startTime;
        }

        public void RemoveTime(float deltaTime) // Метод обновления таймера  заново вычитаем время 
        {
            if (m_CurrenyTime <= 0) return; // Если текущее время равно 0 то возвращаем текущее время 

            m_CurrenyTime -= deltaTime;
        }
    }
}
