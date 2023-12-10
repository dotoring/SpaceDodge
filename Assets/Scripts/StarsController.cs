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

        //star�� ������ �� �÷��̾��� ��ġ�� �̵����� ����
        dir = player.transform.position - this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //star ���� ���� �÷��̾ ���� �����̵���
        transform.Translate(dir * mvSpeed * Time.deltaTime);

        //ȭ�� ������ ������ �ı�
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
