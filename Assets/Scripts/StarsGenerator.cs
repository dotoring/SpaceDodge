using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsGenerator : MonoBehaviour
{
    public GameObject star;
    float spawnTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameMgr.gameState == GameState.play)
        {
            if (GameMgr.time < 10.0f)       //게임 시간 0초~10초 1레벨
            {
                if (spawnTime >= 1f)
                {
                    spawnStar();
                    spawnTime = 0.0f;
                }
            }
            else if (GameMgr.time < 20.0f)  //게임 시간 10초~20초 2레벨
            {
                if (spawnTime >= 0.5f)
                {
                    spawnStar();
                    spawnTime = 0.0f;
                }
            }
            else if (GameMgr.time < 30.0f)  //게임 시간 20초~30초 3레벨
            {
                if (spawnTime >= 0.3f)
                {
                    spawnStar();
                    spawnStar();
                    spawnTime = 0.0f;
                }
            }
            else                            //게임 시간 30초이후 4레벨
            {
                if (spawnTime >= 0.1f)
                {
                    spawnStar();
                    spawnStar();
                    spawnTime = 0.0f;
                }
            }

            spawnTime += Time.deltaTime;
        }
    }

    //star 스폰 함수
    void spawnStar()
    {
        float x = 0.0f;
        float y = 0.0f;

        float dis = 0.0f;

        GameObject obj = GameObject.Find("player");
        PlayerCtrl player = obj.GetComponent<PlayerCtrl>();

        if(player != null)
        {
            //플레이어와 star의 거리가 항상 1.5보다 멀게 스폰
            do
            {
                x = Random.Range(-8.0f, 9.0f);
                y = Random.Range(-5.0f, 6.0f);

                dis = Vector2.Distance(player.transform.position, new Vector2(x, y));

            } while (dis < 1.5);

        }
        GameObject go = Instantiate(star) as GameObject;
        go.transform.position = new Vector2(x, y);
    }
}
