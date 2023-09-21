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
        #region �������� �̸�
        Aries = 0,          // ���ڸ�
        Taurus = 1,         // Ȳ���ڸ�
        Gemini = 2,         // �ֵ����ڸ�
        Cancer = 3,         // ���ڸ�
        Leo = 4,            // �����ڸ�
        Virgo = 5,          // ó���ڸ�
        Libra = 6,          // õĪ�ڸ�
        Scorpio = 7,        // �����ڸ�
        Sagittarius = 8,    // �ü��ڸ�
        Capricorn = 9,      // �����ڸ�
        Aquarius = 10,       // �����ڸ�
        Pisces = 11,         // �������ڸ�
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
        // ���ݱ����� ��������� �����Ѵ�.
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
        PlayerName = "주인공";
        ClearChapterNum = 0;
        ClearStageNum = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        TalkOnce = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        AfterTalckOnce = new bool[12];
    }
}
