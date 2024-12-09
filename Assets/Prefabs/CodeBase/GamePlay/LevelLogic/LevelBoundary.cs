using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingletonBase<LevelBoundary>
    {
        [SerializeField] private float m_Radius; // Создаем переменую радиус мира
        public float Radius => m_Radius;

        public enum Mode // Делаем границу лимитироаной или телепортирующей в противоположную точку
        {
            Limit,
            Teleport
        }

        [SerializeField] private Mode m_LimitMode; // Создаем актуальный режим ограничения 
        public Mode LimitMode => m_LimitMode;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position,transform.forward, m_Radius);
        }
#endif
    }
}