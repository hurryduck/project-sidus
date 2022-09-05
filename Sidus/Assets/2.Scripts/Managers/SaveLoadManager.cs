using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
    // JsonUtility�� ����Ͽ�, ��ȯ�� JSON(string)�� BinaryFormatter�� �̿��� ���̳ʸ� ���·� ��ȯ�ϰ�, ���Ͽ� �����Ѵ�.
    // ������ �ҷ����� ���: BinaryFormatter�� ����� JSON���� ��ȯ�ϰ�, JsonUtility�� ����� Data�� ��ȯ�Ѵ�.

    public static bool FileExists(string _fileName)
    {
        string path = Application.persistentDataPath + "/" + _fileName;
        return File.Exists(path);
    }

    public static void DataSave<T>(T data, string _fileName)
    {
        try
        {
            string json = JsonUtility.ToJson(data);

            if (json.Equals("{}"))
            {
                Debug.Log("" + "json null");
                return;
            }
            string path = Application.persistentDataPath + "/" + _fileName;

            FileStream fileStream = new FileStream(path, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(fileStream, json);
            fileStream.Close();
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
        }
    }

    public static T DataLoad<T>(string _fileName)
    {
        string path = Application.persistentDataPath + "/" + _fileName;

        try
        {
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                string json = formatter.Deserialize(stream) as string;

                T data = JsonUtility.FromJson<T>(json);

                stream.Close();

                Debug.Log(json);
                return data;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
        }
        return default;
    }
}
