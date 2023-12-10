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

    //�ð� ����� ���� ������ ����
    int rankDESC(CRanking a, CRanking b)
    {
        return -a.record.CompareTo(b.record);
    }

    // Start is called before the first frame update
    void Start()
    {
        //�� ���۽� ��ŷ�ҷ�����
        LoadRanking();

        //��ŷ ����Ʈ ���� ����
        rankingList.Sort(rankDESC);

        //10�� ���� ��ŷ ����
        if(rankingList.Count >= 11)
        {
            rankingList.RemoveAt(10);
        }

        //��ŷ ȭ�鿡 ��Ÿ����
        RefreshRanking();

        //�ٽ��ϱ� ��ư
        if (playBtn != null)
        {
            playBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });
        }

        //Ÿ��Ʋ�� ������ ��ư
        if (backBtn != null)
        {
            backBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("TitleScene");
            });
        }

        //��ŷ �ʱ�ȭ ��ư
        if (clearBtn != null)
        {
            clearBtn.onClick.AddListener(ClearRanking);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��ŷ ���ÿ� �����ϴ� �Լ�
    public static void SaveRanking()
    {
        PlayerPrefs.DeleteAll();    //���� ���� ������ ����
        PlayerPrefs.SetInt("rank_Count", rankingList.Count);    //��ŷ �� ����

        CRanking node;
        string keyBuff = "";

        //��ŷ �� ��ŭ ���ÿ� ��� ����
        for (int i = 0; i < rankingList.Count;i++)
        {
            node = rankingList[i];
            keyBuff = string.Format("Rank_{0}_Name", i);
            PlayerPrefs.SetString(keyBuff, node.name);
            keyBuff = string.Format("Rank_{0}_Record", i);
            PlayerPrefs.SetFloat(keyBuff, node.record);
        }
    }

    //���ÿ� ����� ��ŷ �ҷ����� �Լ�
    public static void LoadRanking()
    {
        rankingList.Clear();    //������ ��ŷ ����Ʈ �ʱ�ȭ

        //���ÿ� ����� ��ŷ �� �ҷ�����
        int rankCount = PlayerPrefs.GetInt("rank_Count");
        if(rankCount < 0)
        {
            return;
        }

        CRanking node;
        string keyBuff = "";

        //���ÿ� ����� ��ŷ �� ��ŭ ��ŷ ����Ʈ�� ��� �ֱ�
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

    //��ŷ �ʱ�ȭ �Լ�
    void ClearRanking()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("RankingScene");
    }

    //ȭ�鿡 ��ŷ ��Ÿ���� �Լ�
    void RefreshRanking()
    {
        for (int i = 0; i < rankingList.Count; i++)
        {
            names[i].text = rankingList[i].name;
            scores[i].text = rankingList[i].record.ToString("N3");
        }
    }
}
