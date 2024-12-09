using UnityEngine;

namespace SpaceShooter
{
    public class DontDestroys : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}