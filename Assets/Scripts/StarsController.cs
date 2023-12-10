using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsController : MonoBehaviour
{
    float mvSpeed = 0.5f;
    GameObject player;
    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");

        //star가 스폰될 때 플레이어의 위치로 이동방향 설정
        dir = player.transform.position - this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //star 스폰 순간 플레이어를 향해 움직이도록
        transform.Translate(dir * mvSpeed * Time.deltaTime);

        //화면 밖으로 나가면 파괴
        if (transform.position.x < -9f || transform.position.x > 9f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -5.5f || transform.position.y > 5.5f)
        {
            Destroy(gameObject);
        }
    }
}
