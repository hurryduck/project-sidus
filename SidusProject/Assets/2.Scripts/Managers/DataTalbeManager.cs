using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTalbeManager : MonoBehaviour
{
    private static DataTalbeManager instance;
    public static DataTalbeManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DataTalbeManager>();
            return instance;
        }
    }

    private ReadyPanel_Data[] ReadyPanel_Datas;
    public ReadyPanel_Data GetData_ReadyPanel(int ChapterNum)
    {
        return ReadyPanel_Datas[ChapterNum];
    }

    private DialogData[] Dialog_Datas;
    public List<DialogData> GetData_Dialog(GameManager.ChapterType Chapter, string StartEnd)
    {
        int ChapterNum = (int)Chapter + 1;

        List<DialogData> returnData = new List<DialogData>();
        foreach (DialogData Data in Dialog_Datas)
        {
            string[] IDSplit = Data.ID.Split('_');
            if (IDSplit[0] == ChapterNum.ToString() && IDSplit[1] == StartEnd)
                returnData.Add(Data);
        }
        return returnData;
    }


    private void Awake()
    {
        LoadData_ReadyPanel();
        LoadData_Dialog();
    }

    private void LoadData_ReadyPanel()
    {
        // 준비창 데이터 불러오기
        List<Dictionary<string, object>> LoadData = CSVReader.Read("ReadyPanelDataTable");
        ReadyPanel_Datas = new ReadyPanel_Data[LoadData.Count];

        for (int i = 0; i < LoadData.Count; i++)
        {
            ReadyPanel_Data newData = new ReadyPanel_Data();
            newData.ChapterTitle = LoadData[i]["ChapterTitle"].ToString();
            newData.CharacterImage = LoadData[i]["CharacterImage"].ToString();
            newData.LimitTime = int.Parse(LoadData[i]["LimitTime"].ToString());
            newData.GoalScore = int.Parse(LoadData[i]["GoalScore"].ToString());

            ReadyPanel_Datas[i] = newData;
        }

    }

    private void LoadData_Dialog()
    {
        List<Dictionary<string, object>> LoadData = CSVReader.Read("DialogDataTable");
        Dialog_Datas = new DialogData[LoadData.Count];

        for (int i = 0; i < LoadData.Count; i++)
        {
            DialogData newData = new DialogData();
            newData.ID = LoadData[i]["ID"].ToString();
            switch (LoadData[i]["Type"])
            {
                #region
                case "Standard":
                    newData.Type = DialogData.DialogTypes.Standard;
                    break;

                case "Ask":
                    newData.Type = DialogData.DialogTypes.Ask;
                    break;

                case "Answer_1":
                    newData.Type = DialogData.DialogTypes.Answer_1;
                    break;

                case "Answer_End":
                    newData.Type = DialogData.DialogTypes.Answer_End;
                    break;
                    #endregion
            }
            newData.ActorName = LoadData[i]["ActorName"].ToString();
            newData.Face = int.Parse(LoadData[i]["Face"].ToString());
            newData.Speech = LoadData[i]["Speech"].ToString();

            Dialog_Datas[i] = newData;
        }
    }
}
