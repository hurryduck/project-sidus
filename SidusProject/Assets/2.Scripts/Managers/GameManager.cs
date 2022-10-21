using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public PlayerData PlayerData;

    public enum ChapterType
    {
        #region 스테이지 이름
        Aries = 0,          // 양자리
        Taurus = 1,         // 황소자리
        Gemini = 2,         // 쌍둥이자리
        Cancer = 3,         // 게자리
        Leo = 4,            // 사자자리
        Virgo = 5,          // 처녀자리
        Libra = 6,          // 천칭자리
        Scorpio = 7,        // 전갈자리
        Sagittarius = 8,    // 궁수자리
        Capricorn = 9,      // 염소자리
        Aquarius = 10,       // 물병자리
        Pisces = 11,         // 물고기자리
        #endregion
    }
    public ChapterType CurrentChapter;
    public int CurrentStage;

    private void Awake()
    {
        var obj = FindObjectsOfType<GameManager>();

        if (obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

        LoadData();
    }

    public void SaveData()
    {
        // 지금까지의 변경사항을 저장한다.
        SaveLoadManager.DataSave(PlayerData, "Data");
    }

    public void LoadData()
    {
        if (SaveLoadManager.FileExists("Data"))
            PlayerData = SaveLoadManager.DataLoad<PlayerData>("Data");
        else
            PlayerData = new PlayerData();
    }

    public void ResetData()
    {
        PlayerData = new PlayerData();
        SaveLoadManager.DataSave(PlayerData, "Data");
    }
}

[System.Serializable]
public class PlayerData
{
    public string PlayerName;
    public int ClearChapterNum;
    public int[] ClearStageNum;
    public int[] TalkOnce;
    public bool[] AfterTalckOnce;

    public PlayerData()
    {
        PlayerName = "아무개";
        ClearChapterNum = 0;
        ClearStageNum = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        TalkOnce = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        AfterTalckOnce = new bool[12];
    }
}
