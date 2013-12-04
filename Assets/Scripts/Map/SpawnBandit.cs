using UnityEngine;
using System.Collections;

namespace Engine.Map
{
    public class SpawnBandit : MonoBehaviour
    {
        public GameObject bandit;

        float Timer;
        const float SpawnTime = 10;
        public int maxBanditCount;
        public RectangleBounding[] forestPositions;
        [HideInInspector]
        public int banditCount;

        void Start()
        {
            banditCount = 0;

            forestPositions = new RectangleBounding[4];
            forestPositions[0] = new RectangleBounding()
            {
                leftUp = new Vector2(-8, 5),
                rightUp = new Vector2(-6.4f, 4.8f),
                leftDown = new Vector2(-8, 2.6f),
                rightDown = new Vector2(-5, 3.6f)
            };
            forestPositions[1] = new RectangleBounding()
            {
                leftUp = new Vector2(0.45f, 4),
                rightUp = new Vector2(6.8f, 4.6f),
                leftDown = new Vector2(1.4f, 2f),
                rightDown = new Vector2(7.6f, 0.8f)
            };
            forestPositions[2] = new RectangleBounding()
            {
                leftUp = new Vector2(2.2f, -1.4f),
                rightUp = new Vector2(3.8f, -1.2f),
                leftDown = new Vector2(3.2f, -1.8f),
                rightDown = new Vector2(2.2f, -1.8f)
            };
            forestPositions[3] = new RectangleBounding()
            {
                leftUp = new Vector2(-4.6f, -4.7f),
                rightUp = new Vector2(-2f, -4.8f),
                leftDown = new Vector2(-4f, -5.2f),
                rightDown = new Vector2(-2.8f, -5.2f)
            };
        }

        void Update()
        {

            if (banditCount < maxBanditCount)
            {
                Timer += Time.deltaTime * 0.5f;
                if (Timer > SpawnTime)
                {
                    Timer = 0;
                    banditCount++;
                    AddBandit();
                }
            }
        }

        void AddBandit()
        {
            if (forestPositions.Length > 0)
            {
                int index = Random.Range(0, forestPositions.Length);

                Vector3 position = EngineHelper.PointOnRectangle(forestPositions[index]);
                Instantiate(bandit, position, Quaternion.identity);
            }
            else
            {
                Instantiate(bandit, new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0), Quaternion.identity);
            }
        }
    }
}