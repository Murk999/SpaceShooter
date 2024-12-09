using UnityEngine;

[DisallowMultipleComponent] // Нельзя дважды создать 
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour // Абстрактный класс не можем перетащить куда либо 
{
    [Header("Singleton")]
    [SerializeField] private bool m_DoNotDestryOnLoad;

    public static T Instance { get; private set; }

    protected virtual void Awake() // при первом старте проверяем
    {
        if (Instance != null) // // Если Объект уже есть на сцене
        {
            Debug.LogWarning("MonoSingleton: object of type already exists,instanse will be destroyed = " + typeof(T).Name);
            Destroy(this);  // Уничтожаем объект
            return; // Выходим из метода
        }

        Instance = this as T;

        if (m_DoNotDestryOnLoad)
            DontDestroyOnLoad(gameObject); // Не даю уничтожить объект , что бы пропускать сквозь сцены 
    }
}