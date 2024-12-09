using UnityEngine.UI;
using UnityEngine;

namespace SpaceShooter
{
    public class KillTExt : MonoBehaviour
    {
        [SerializeField] private Text m_Text;

        private float lastNumKills;
        private void Update()
        {
            int numKills = Player.Instance.NumKills;

            if(lastNumKills != numKills)
            {
                m_Text.text = "Kills : " + numKills.ToString();
                lastNumKills = numKills;
            }
        }
    }
}