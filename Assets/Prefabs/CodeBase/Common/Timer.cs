namespace Common
{
    public class Timer
    {
        private float m_CurrenyTime; // ������� �����

        public bool IsFinished => m_CurrenyTime <= 0; // ���������� ������ ��� ��� 

        public Timer(float startTime)  // ����� ����������� �������� ���������
        {
            Start(startTime);
        }

        public void Start(float startTime) // ����� ��� ������ ������ ������� ����� ������ ���������
        {
            m_CurrenyTime = startTime;
        }

        public void RemoveTime(float deltaTime) // ����� ���������� �������  ������ �������� ����� 
        {
            if (m_CurrenyTime <= 0) return; // ���� ������� ����� ����� 0 �� ���������� ������� ����� 

            m_CurrenyTime -= deltaTime;
        }
    }
}
