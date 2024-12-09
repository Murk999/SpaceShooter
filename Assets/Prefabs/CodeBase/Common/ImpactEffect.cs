using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float m_Lifetime; // Время жизни объекта 

        private float m_Timer; // таймер

        private void Update()
        {
            if (m_Timer < m_Lifetime) // Если таймер меньше времени Жизни объекта 
                m_Timer += Time.deltaTime; // То просто прибавляем таймдельтатайм 
            else
                Destroy(gameObject); // Если Таймер больше времени жизни , то уничтожаем объект 
        }
    }
}

