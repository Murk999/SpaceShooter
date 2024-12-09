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
            if (Input.GetKeyUp(KeyCode.F1) == true) // ��������� ���� ������ F1 , ��������� Reset
            {
                Reset();
            }
        }

        private void Reset()  // ������� ������������ ����� 
        {
            // FindObjectOfType<PickUp1>().CoinNull();
          //  PlayerPrefs.DeleteAll();  // ������� ������ �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ��������� ����� ����� ������ 
        }
    }
}

