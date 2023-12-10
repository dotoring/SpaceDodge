using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingMgr : MonoBehaviour
{
    public static List<CRanking> rankingList = new List<CRanking>();

    public Button playBtn = null;
    public Button backBtn = null;
    public Button clearBtn = null;

    public Text[] names = null;
    public Text[] scores = null;

    //시간 기록이 높은 순으로 정렬
    int rankDESC(CRanking a, CRanking b)
    {
        return -a.record.CompareTo(b.record);
    }

    // Start is called before the first frame update
    void Start()
    {
        //씬 시작시 랭킹불러오기
        LoadRanking();

        //랭킹 리스트 순위 정렬
        rankingList.Sort(rankDESC);

        //10위 밖의 랭킹 삭제
        if(rankingList.Count >= 11)
        {
            rankingList.RemoveAt(10);
        }

        //랭킹 화면에 나타내기
        RefreshRanking();

        //다시하기 버튼
        if (playBtn != null)
        {
            playBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });
        }

        //타이틀로 나가기 버튼
        if (backBtn != null)
        {
            backBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("TitleScene");
            });
        }

        //랭킹 초기화 버튼
        if (clearBtn != null)
        {
            clearBtn.onClick.AddListener(ClearRanking);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //랭킹 로컬에 저장하는 함수
    public static void SaveRanking()
    {
        PlayerPrefs.DeleteAll();    //기존 로컬 데이터 삭제
        PlayerPrefs.SetInt("rank_Count", rankingList.Count);    //랭킹 수 저장

        CRanking node;
        string keyBuff = "";

        //랭킹 수 만큼 로컬에 기록 저장
        for (int i = 0; i < rankingList.Count;i++)
        {
            node = rankingList[i];
            keyBuff = string.Format("Rank_{0}_Name", i);
            PlayerPrefs.SetString(keyBuff, node.name);
            keyBuff = string.Format("Rank_{0}_Record", i);
            PlayerPrefs.SetFloat(keyBuff, node.record);
        }
    }

    //로컬에 저장된 랭킹 불러오기 함수
    public static void LoadRanking()
    {
        rankingList.Clear();    //기존의 랭킹 리스트 초기화

        //로컬에 저장된 랭킹 수 불러오기
        int rankCount = PlayerPrefs.GetInt("rank_Count");
        if(rankCount < 0)
        {
            return;
        }

        CRanking node;
        string keyBuff = "";

        //로컬에 저장된 랭킹 수 만큼 랭킹 리스트에 기록 넣기
        for(int i = 0; i < rankCount; i++)
        {
            node = new CRanking();
            keyBuff = string.Format("Rank_{0}_Name", i);
            node.name = PlayerPrefs.GetString(keyBuff, "");
            keyBuff = string.Format("Rank_{0}_Record", i);
            node.record = PlayerPrefs.GetFloat(keyBuff, 0.0f);

            rankingList.Add(node);
        }
    }

    //랭킹 초기화 함수
    void ClearRanking()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("RankingScene");
    }

    //화면에 랭킹 나타내기 함수
    void RefreshRanking()
    {
        for (int i = 0; i < rankingList.Count; i++)
        {
            names[i].text = rankingList[i].name;
            scores[i].text = rankingList[i].record.ToString("N3");
        }
    }
}
