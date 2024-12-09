using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class Restart : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.F1) == true) // Првоеряем если нажали F1 , запускаем Reset
            {
                Reset();
            }
        }

        private void Reset()  // Создаем перезагрузку сцены 
        {
            // FindObjectOfType<PickUp1>().CoinNull();
          //  PlayerPrefs.DeleteAll();  // Удаляем старую сцену
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // загружаем новую сцену заново 
        }
    }
}

