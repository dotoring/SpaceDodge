using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    ready,
    play,
    gameover,
}

public class GameMgr : MonoBehaviour
{
    public Text clock;

    public static float time = 0.0f;
    static float bestRecord = 0.0f;
    float curRecord = 0.0f;
    int MaxLife = 3;
    [HideInInspector] public int CurLife;

    public GameObject helpText = null;
    public GameObject[] lives = null;

    [Header("------GameOver------")]
    public GameObject gameOverPanel = null;
    public Text recordHelpText = null;
    public Text nameText = null;
    public InputField nameInputField = null;
    public Button recordOkBtn = null;
    public Button goRankingBtn = null;
    public Button replayBtn = null;
    public Button menuBtn = null;
    public Text bestRecordText = null;
    public Text curRecordText = null;

    public static GameState gameState;

    //싱글턴
    public static GameMgr Inst = null;

    void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CurLife = MaxLife;  //최대체력 만큼 체력 설정
        if (RankingMgr.rankingList.Count != 0)
        {
            bestRecord = RankingMgr.rankingList[0].record;  //최고기록 랭킹 1위의 기록으로 설정
        }

        //게임 시작시 준비상태
        gameState = GameState.ready;
        Time.timeScale = 0;

        //리플레이
        if(replayBtn != null)
        {
            replayBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });
        }

        //메뉴로 돌아가기
        if(menuBtn != null)
        {
            menuBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("TitleScene");
            });
        }

        //랭킹 등록
        if(recordOkBtn != null)
        {
            recordOkBtn.onClick.AddListener(AddRank);
        }

        //랭킹보기
        if(goRankingBtn != null)
        {
            goRankingBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("RankingScene");
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        //준비 상태에서
        if(gameState == GameState.ready)
        {
            if(time > 0.0f)
            {
                time = 0.0f;
            }
        }

        //게임 플레이 상태에서
        if(gameState == GameState.play)
        {
            helpText.SetActive(false);
            Time.timeScale = 1;
            time += Time.deltaTime;
            clock.text = time.ToString("N3");
        }

        //게임 오버 상태에서
        if(gameState == GameState.gameover)
        {
            Time.timeScale = 0;

            curRecord = time;
            if(curRecord > bestRecord)
            {
                bestRecord = curRecord;
            }

            curRecordText.text = "이번기록 : " + curRecord.ToString("N3");
            bestRecordText.text = "최고기록 : " + bestRecord.ToString("N3");

            //게임오버 판넬 켜기
            gameOverPanel.SetActive(true);
        }


    }

    //플레이어가 맞았을 때 함수
    public void Hitted()
    {
        CurLife--;
        lives[CurLife].SetActive(false);    //라이프 하나씩 꺼주기
    }

    //랭킹 등록함수
    void AddRank()
    {
        //이름 입력안했을 때 작동x
        if (nameInputField.text == "")
        {
            return;
        }

        string name = nameInputField.text.Trim();
        RankingMgr.rankingList.Add(new CRanking(name, curRecord));  //랭킹리스트에 기록 추가

        //랭킹리스트에 추가 후 랭킹등록 UI 모두 꺼준 뒤 랭킹보기 버튼 활성화
        recordHelpText.text = "기록되었습니다!";
        nameText.gameObject.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        recordOkBtn.gameObject.SetActive(false);
        goRankingBtn.gameObject.SetActive(true);
        RankingMgr.SaveRanking();
    }
}
