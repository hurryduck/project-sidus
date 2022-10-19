using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogScript : MonoBehaviour
{
    [SerializeField] private GameObject Ask_1;
    [SerializeField] private GameObject Ask_2;
    [HideInInspector] public int SelectNum = 0;

    [HideInInspector] public List<DialogData> Datas;
    [HideInInspector] public int OderNum;
    public Image Actor;
    [SerializeField] private Text ActorName;
    [SerializeField] private Sprite[] ActorFace;
    [SerializeField] private Text ActorSpeech;

    [HideInInspector] public bool IsBefore;

    [SerializeField] private Tutorial IngameTutorial;

    private bool IsSkip = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsSkip = true;
        }
    }

    public IEnumerator Dialog()
    {
        if (!IsBefore)
            Datas = DataTalbeManager.Instance.GetData_Dialog(GameManager.Instance.CurrentChapter, "Start");
        else
            Datas = DataTalbeManager.Instance.GetData_Dialog(GameManager.Instance.CurrentChapter, "End");


        for (OderNum = 0; OderNum < Datas.Count; OderNum++)
        {
            if (Datas[OderNum].Type == DialogData.DialogTypes.Ask)
                yield return StartCoroutine(Ask());

            switch (SelectNum)
            {
                case 0:
                    yield return StartCoroutine(Acting(Datas[OderNum].ActorName, Datas[OderNum].Face, Datas[OderNum].Speech));
                    break;

                case 1:
                    yield return StartCoroutine(Acting(Datas[OderNum].ActorName, Datas[OderNum].Face, Datas[OderNum].Speech));
                    OderNum++;
                    break;

                case 2:
                    OderNum++;
                    yield return StartCoroutine(Acting(Datas[OderNum].ActorName, Datas[OderNum].Face, Datas[OderNum].Speech));
                    break;
            }

            if (Datas[OderNum].Type == DialogData.DialogTypes.Answer_End)
                SelectNum = 0;

        }

        if (!IsBefore)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("5.Ingame");
            GameManager.Instance.PlayerData.TalkOnce[(int)GameManager.Instance.CurrentChapter] = 1;
        }
        else
        {
            SceneManager.LoadScene("2.Lobby");
            GameManager.Instance.PlayerData.TalkOnce[(int)GameManager.Instance.CurrentChapter] = 2;
        }
    }

    private IEnumerator Ask()
    {
        Ask_1.GetComponent<AskButton>().SetActive(true, this);
        Ask_2.GetComponent<AskButton>().SetActive(true, this);

        while (true)
        {
            if (SelectNum > 0)
            {
                Ask_1.SetActive(false);
                Ask_2.SetActive(false);
                IsSkip = false;
                break;
            }
            yield return null;
        }
    }

    private IEnumerator Acting(string actorName, int actorFace, string actorSpeech)
    {
        if(actorName == "Tutorial")
        {
            StartCoroutine(IngameTutorial.StartTutorial());
        }
        else
        {
            if (actorName == "Player")
                ActorName.text = GameManager.Instance.PlayerData.PlayerName;
            else
                ActorName.text = actorName;

            switch (actorName)
            {
                #region
                case "쿠말":
                    Actor.sprite = Resources.Load<Sprite>("Sprites/Character/S_C_Kumal_Standing" + actorFace.ToString());
                    break;

                case "타우루스":
                    Actor.sprite = Resources.Load<Sprite>("Sprites/Character/S_C_Taurus_Standing" + actorFace.ToString());
                    break;


                    #endregion
            }

            string[] SpeechSplit = actorSpeech.Split('_');
            if (SpeechSplit.Length > 1)
            {
                actorSpeech = null;
                for (int i = 0; i < SpeechSplit.Length; i++)
                {
                    if (SpeechSplit[i] == "Player")
                        actorSpeech += GameManager.Instance.PlayerData.PlayerName;
                    else
                        actorSpeech += SpeechSplit[i];
                }
            }

            string writerText = "";
            for (int i = 0; i < actorSpeech.Length; i++)
            {
                if (IsSkip)
                {
                    ActorSpeech.text = actorSpeech;
                    IsSkip = false;
                    break;
                }

                writerText += actorSpeech[i];
                ActorSpeech.text = writerText;
                yield return new WaitForSeconds(0.05f);
            }

            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                    break;
                yield return null;
            }

            IsSkip = false;
        }
    }
}
