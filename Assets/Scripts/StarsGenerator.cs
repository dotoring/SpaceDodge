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
            if (GameMgr.time < 10.0f)       //���� �ð� 0��~10�� 1����
            {
                if (spawnTime >= 1f)
                {
                    spawnStar();
                    spawnTime = 0.0f;
                }
            }
            else if (GameMgr.time < 20.0f)  //���� �ð� 10��~20�� 2����
            {
                if (spawnTime >= 0.5f)
                {
                    spawnStar();
                    spawnTime = 0.0f;
                }
            }
            else if (GameMgr.time < 30.0f)  //���� �ð� 20��~30�� 3����
            {
                if (spawnTime >= 0.3f)
                {
                    spawnStar();
                    spawnStar();
                    spawnTime = 0.0f;
                }
            }
            else                            //���� �ð� 30������ 4����
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

    //star ���� �Լ�
    void spawnStar()
    {
        float x = 0.0f;
        float y = 0.0f;

        float dis = 0.0f;

        GameObject obj = GameObject.Find("player");
        PlayerCtrl player = obj.GetComponent<PlayerCtrl>();

        if(player != null)
        {
            //�÷��̾�� star�� �Ÿ��� �׻� 1.5���� �ְ� ����
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
