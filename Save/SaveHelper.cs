using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveHelper : MonoBehaviour
{

    public static SaveHelper instance;

    private string destination;

    private void Awake()
    {
        Singleton();
        destination = Application.persistentDataPath + "/player.dat";
    }

    public string GetJsonFromFile()
    {
        string line;
        string json = "";
        try
        {
            FileStream fileStream = File.OpenRead(destination);
            StreamReader streamReader = new(fileStream);

            while ((line = streamReader.ReadLine()) != null)
            {
                json += line;
            }

            fileStream.Close();
        }
        catch (Exception)
        {
            json = "";
        }
        return json;
    }



    public void SaveJsonToFile(string json)
    {

        FileStream fileStream = File.OpenWrite(destination);
        StreamWriter streamWriter = new(fileStream);
        Debug.Log(destination);
        streamWriter.WriteLine(json);
        streamWriter.Close();
        fileStream.Close();
    }

    private void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }


}
