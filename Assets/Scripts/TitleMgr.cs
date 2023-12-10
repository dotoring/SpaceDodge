using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    public Button startBtn;
    public Button rankingBtn;
    public Button quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        RankingMgr.LoadRanking();

        if(startBtn != null)
        {
            startBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });
        }

        if(rankingBtn != null)
        {
            rankingBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("RankingScene");
            });
        }

        if(quitBtn != null)
        {
            quitBtn.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
