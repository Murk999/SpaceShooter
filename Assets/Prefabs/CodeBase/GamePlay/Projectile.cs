using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class Projectile : ProjectileBase
    {
        protected override void OnHit(Destructible destructible)
        {
            if (m_Parent == Player.Instance.ActiveShip) // ��������� ���� ��� ��� ������� ���� ��� ������� 
            {
                Player.Instance.AddScore(destructible.ScoreValue); // ��������� ���� , ������� ����� �� ����������

                if (destructible is BotsShips)  // ���� ��������� �������� ����������
                {
                    if(destructible.HitPoints <= 0)
                    Player.Instance.AddKill();
                }
            }
        }

        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject, 0);
            Instantiate(boomSplash, transform.position, Quaternion.identity);
        }
    }
}