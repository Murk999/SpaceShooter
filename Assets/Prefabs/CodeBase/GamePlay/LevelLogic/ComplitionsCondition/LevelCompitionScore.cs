using UnityEngine;

namespace SpaceShooter
{
    public class LevelCompitionScore : LevelCondition
    {
        [SerializeField] private int m_Score;
        public override bool IsCompleted
        {
            get
            {
                if(Player.Instance.ActiveShip == null) return false;

                if (Player.Instance.Score >= m_Score)
                {
                    return true;
                }

                return false;
            }
        }
    }
}