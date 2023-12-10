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
        Application.targetFrameRate = 60; //실행 프레임 속도 60프레임으로 고정
        QualitySettings.vSyncCount = 0; //모니터 주사율 고정

        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))    //왼쪽 방향키 눌렀을 때
        {
            if (GameMgr.gameState == GameState.ready)
            {
                GameMgr.gameState = GameState.play; //게임 상태 play로 변환
            }

            animator.SetInteger("DirState", 2);     //왼쪽 이동 애니메이션
            spriteRenderer.flipX = false;

            horizontalSpeed -= 0.01f;               //왼쪽 방향 속도 증가
            if (horizontalSpeed == 0f)              //캐릭터가 멈출수 없게 설정
            {
                horizontalSpeed = -0.01f;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))   //오른쪽 방향키 눌렀을 때
        {
            if (GameMgr.gameState == GameState.ready)
            {
                GameMgr.gameState = GameState.play; //게임 상태 play로 변환
            }

            animator.SetInteger("DirState", 2);     //오른쪽 이동 애니메이션
            spriteRenderer.flipX = true;

            horizontalSpeed += 0.01f;               //오른쪽 방향 속도 증가
            if (horizontalSpeed == 0f)              //캐릭터가 멈출수 없게 설정
            {
                horizontalSpeed = 0.01f;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))      //위쪽 방향키 눌렀을 때
        {
            if (GameMgr.gameState == GameState.ready)
            {
                GameMgr.gameState = GameState.play; //게임 상태 play로 변환
            }

            animator.SetInteger("DirState", 1);     //위쪽 이동 애니메이션

            verticalSpeed += 0.01f;                 //위쪽 방향 속도 증가
            if (verticalSpeed == 0f)                //캐릭터가 멈출수 없게 설정
            {
                verticalSpeed = 0.01f;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))    //아래쪽 방향키 눌렀을 때
        {
            if (GameMgr.gameState == GameState.ready)
            {
                GameMgr.gameState = GameState.play; //게임 상태 play로 변환
            }

            animator.SetInteger("DirState", 0);     //아래쪽 이동 애니메이션

            verticalSpeed -= 0.01f;                 //아래쪽 방향 속도 증가
            if (verticalSpeed == 0f)                //캐릭터가 멈출수 없게 설정
            {
                verticalSpeed = -0.01f;
            }
        }

        this.transform.Translate(horizontalSpeed, 0, 0);    //좌우 방향 속도만큼 계속 이동
        this.transform.Translate(0, verticalSpeed, 0);      //상하 방향 속도만큼 계속 이동

        //화면 밖으로 나가면 사망 처리
        if (transform.position.x > 9 || transform.position.x < -9 ||
            transform.position.y > 4.9 || transform.position.y < -5.4)
        {
            GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어가 운석에 맞았을 때
        GameMgr.Inst.Hitted();
        if (GameMgr.Inst.CurLife <= 0)  //체력이 0이되면 사망
        {
            GameOver();
        }
        Destroy(other.gameObject);

    }

    //사망 처리 함수
    void GameOver()
    {
        Destroy(gameObject);
        GameMgr.gameState = GameState.gameover;
    }
}
