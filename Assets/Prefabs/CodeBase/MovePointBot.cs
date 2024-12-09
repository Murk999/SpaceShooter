using UnityEngine;

namespace SpaceShooter
{
    public class MovePointBot : MonoBehaviour
    {
        public Transform[] moveSpots;

        public float speed;

        private int randomSpot;

        private float waitTime;
        public float startWaitTime;

        private BotsShips bots;

        // Start is called before the first frame update
        void Start()
        {
            waitTime = startWaitTime;

            randomSpot = Random.Range(0, moveSpots.Length);
        }

        // Update is called once per frame
        void Update()
        {          
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed  * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }  
        }
    }
}