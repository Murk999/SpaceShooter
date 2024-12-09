using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common
{
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool m_Hold;
        public bool IsHold => m_Hold;
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            m_Hold = true;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            m_Hold = false;
        }
    }
}

