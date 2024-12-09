using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundElement : MonoBehaviour
    {
        [Range(0.0f, 4.0f)]
        [SerializeField] private float m_ParallaxPower; // сила паралакс эффекта 

        [SerializeField] private float m_TextureScale;

        private Material m_QuadMaterial; // Ссылка на материал 
        private Vector2 m_InitialOffset; // позиция неба 

        private void Start()
        {
            m_QuadMaterial = GetComponent<MeshRenderer>().material; // Получаем ссылку на метериал 
            m_InitialOffset = UnityEngine.Random.insideUnitCircle; // Генерируем случайную точку в рамках единичной окружности 

            m_QuadMaterial.mainTextureScale = Vector2.one * m_TextureScale;
        }

        private void Update()
        {
            Vector2 offset = m_InitialOffset;

            offset.x += transform.position.x / transform.localScale.x / m_ParallaxPower;
            offset.y += transform.position.y / transform.localScale.y / m_ParallaxPower;

            m_QuadMaterial.mainTextureOffset = offset;
        }
    }
}