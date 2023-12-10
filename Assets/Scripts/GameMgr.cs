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

    //�̱���
    public static GameMgr Inst = null;

    void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CurLife = MaxLife;  //�ִ�ü�� ��ŭ ü�� ����
        if (RankingMgr.rankingList.Count != 0)
        {
            bestRecord = RankingMgr.rankingList[0].record;  //�ְ��� ��ŷ 1���� ������� ����
        }

        //���� ���۽� �غ����
        gameState = GameState.ready;
        Time.timeScale = 0;

        //���÷���
        if(replayBtn != null)
        {
            replayBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });
        }

        //�޴��� ���ư���
        if(menuBtn != null)
        {
            menuBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("TitleScene");
            });
        }

        //��ŷ ���
        if(recordOkBtn != null)
        {
            recordOkBtn.onClick.AddListener(AddRank);
        }

        //��ŷ����
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
        //�غ� ���¿���
        if(gameState == GameState.ready)
        {
            if(time > 0.0f)
            {
                time = 0.0f;
            }
        }

        //���� �÷��� ���¿���
        if(gameState == GameState.play)
        {
            helpText.SetActive(false);
            Time.timeScale = 1;
            time += Time.deltaTime;
            clock.text = time.ToString("N3");
        }

        //���� ���� ���¿���
        if(gameState == GameState.gameover)
        {
            Time.timeScale = 0;

            curRecord = time;
            if(curRecord > bestRecord)
            {
                bestRecord = curRecord;
            }

            curRecordText.text = "�̹���� : " + curRecord.ToString("N3");
            bestRecordText.text = "�ְ��� : " + bestRecord.ToString("N3");

            //���ӿ��� �ǳ� �ѱ�
            gameOverPanel.SetActive(true);
        }


    }

    //�÷��̾ �¾��� �� �Լ�
    public void Hitted()
    {
        CurLife--;
        lives[CurLife].SetActive(false);    //������ �ϳ��� ���ֱ�
    }

    //��ŷ ����Լ�
    void AddRank()
    {
        //�̸� �Է¾����� �� �۵�x
        if (nameInputField.text == "")
        {
            return;
        }

        string name = nameInputField.text.Trim();
        RankingMgr.rankingList.Add(new CRanking(name, curRecord));  //��ŷ����Ʈ�� ��� �߰�

        //��ŷ����Ʈ�� �߰� �� ��ŷ��� UI ��� ���� �� ��ŷ���� ��ư Ȱ��ȭ
        recordHelpText.text = "��ϵǾ����ϴ�!";
        nameText.gameObject.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        recordOkBtn.gameObject.SetActive(false);
        goRankingBtn.gameObject.SetActive(true);
        RankingMgr.SaveRanking();
    }
}
