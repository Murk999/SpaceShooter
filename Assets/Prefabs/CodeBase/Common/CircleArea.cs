using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common
{
    public class CircleAreas : MonoBehaviour
    {
        [SerializeField] private float m_Radius; //  Добавляем свойство радиус
        public float Radius => m_Radius;

        public Vector2 GetRandomInsideZone() // Получаем точку в центре окружности 
        {
            return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_Radius; // Берем позициию зоны и к вектору 2 приделываем
                                                                                                          // ЮнитиЕнджайн рандом умноженой на радиус
        }

#if UNITY_EDITOR

        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);
        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }
}

