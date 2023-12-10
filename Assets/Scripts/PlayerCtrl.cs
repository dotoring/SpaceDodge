using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    float horizontalSpeed = 0.0f;
    float verticalSpeed = 0.0f;

    Animator animator;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //���� ������ �ӵ� 60���������� ����
        QualitySettings.vSyncCount = 0; //����� �ֻ��� ����

        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))    //���� ����Ű ������ ��
        {
            if (GameMgr.gameState == GameState.ready)
            {
                GameMgr.gameState = GameState.play; //���� ���� play�� ��ȯ
            }

            animator.SetInteger("DirState", 2);     //���� �̵� �ִϸ��̼�
            spriteRenderer.flipX = false;

            horizontalSpeed -= 0.01f;               //���� ���� �ӵ� ����
            if (horizontalSpeed == 0f)              //ĳ���Ͱ� ����� ���� ����
            {
                horizontalSpeed = -0.01f;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))   //������ ����Ű ������ ��
        {
            if (GameMgr.gameState == GameState.ready)
            {
                GameMgr.gameState = GameState.play; //���� ���� play�� ��ȯ
            }

            animator.SetInteger("DirState", 2);     //������ �̵� �ִϸ��̼�
            spriteRenderer.flipX = true;

            horizontalSpeed += 0.01f;               //������ ���� �ӵ� ����
            if (horizontalSpeed == 0f)              //ĳ���Ͱ� ����� ���� ����
            {
                horizontalSpeed = 0.01f;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))      //���� ����Ű ������ ��
        {
            if (GameMgr.gameState == GameState.ready)
            {
                GameMgr.gameState = GameState.play; //���� ���� play�� ��ȯ
            }

            animator.SetInteger("DirState", 1);     //���� �̵� �ִϸ��̼�

            verticalSpeed += 0.01f;                 //���� ���� �ӵ� ����
            if (verticalSpeed == 0f)                //ĳ���Ͱ� ����� ���� ����
            {
                verticalSpeed = 0.01f;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))    //�Ʒ��� ����Ű ������ ��
        {
            if (GameMgr.gameState == GameState.ready)
            {
                GameMgr.gameState = GameState.play; //���� ���� play�� ��ȯ
            }

            animator.SetInteger("DirState", 0);     //�Ʒ��� �̵� �ִϸ��̼�

            verticalSpeed -= 0.01f;                 //�Ʒ��� ���� �ӵ� ����
            if (verticalSpeed == 0f)                //ĳ���Ͱ� ����� ���� ����
            {
                verticalSpeed = -0.01f;
            }
        }

        this.transform.Translate(horizontalSpeed, 0, 0);    //�¿� ���� �ӵ���ŭ ��� �̵�
        this.transform.Translate(0, verticalSpeed, 0);      //���� ���� �ӵ���ŭ ��� �̵�

        //ȭ�� ������ ������ ��� ó��
        if (transform.position.x > 9 || transform.position.x < -9 ||
            transform.position.y > 4.9 || transform.position.y < -5.4)
        {
            GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //�÷��̾ ��� �¾��� ��
        GameMgr.Inst.Hitted();
        if (GameMgr.Inst.CurLife <= 0)  //ü���� 0�̵Ǹ� ���
        {
            GameOver();
        }
        Destroy(other.gameObject);

    }

    //��� ó�� �Լ�
    void GameOver()
    {
        Destroy(gameObject);
        GameMgr.gameState = GameState.gameover;
    }
}
