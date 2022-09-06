using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private  GameObject[] SetActiveTrueObjs;
    [SerializeField]
    private  GameObject[] SetActiveFalseObjs;

    public void _SaveName()
    {
        GameManager.Instance.PlayerData.PlayerName = FindObjectOfType<InputField>().text;
        GameManager.Instance.SaveData();
    }

    public void _SaveData()
    {
        GameManager.Instance.SaveData();
    }

    public void _SetActiveTrue()
    {
        foreach (GameObject Obj in SetActiveTrueObjs)
            Obj.SetActive(true);
    }

    public void _SetActiveFalse()
    {
        foreach (GameObject Obj in SetActiveFalseObjs)
            Obj.SetActive(false);
    }

    public static void _LoadSceneName(string LoadSceneName)
    {
        SceneManager.LoadScene(LoadSceneName);
    }

    public void _SetCurrentChapter(string ChapterName)
    {
        switch (ChapterName)
        {
            #region ц╘ем
            case "Aries":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Aries;
                break;

            case "Taurus":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Taurus;
                break;

            case "Gemini":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Gemini;
                break;

            case "Cancer":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Cancer;
                break;

            case "Leo":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Leo;
                break;

            case "Virgo":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Virgo;
                break;

            case "Libra":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Libra;
                break;

            case "Scorpio":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Scorpio;
                break;

            case "Sagittarius":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Sagittarius;
                break;

            case "Capricorn":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Capricorn;
                break;

            case "Aquarius":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Aquarius;
                break;

            case "Pisces":
                GameManager.Instance.CurrentChapter = GameManager.ChapterType.Pisces;
                break;
                #endregion
        }
    }

    public void _PauseGame(bool IsPaused)
    {
        InGameManager.Instance.IsPaused = IsPaused;
        if (IsPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
