using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))] // Что бы на бонусе всегда был колайдер что бы считать соприкосновение 
    public abstract class Powerup : MonoBehaviour // Абстрактный класс мы не можем добавить на объект
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>(); // Создаем ссылку на корневой объект корабля 

            if (ship != null && Player.Instance.ActiveShip)  // Если корабль столкнулся с бонусом уничтожаем бонус
            {
                OnPickedUp(ship);   

                Destroy(gameObject);
            }
        }
        protected abstract void OnPickedUp(SpaceShip ship); // Метод где проверяем  что именно корабль коснулся бонуса
    }
}