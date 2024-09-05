using System.Collections;
using UnityEngine;
using System.IO;

public class ReadData : BaseManager<ReadData>
{

    public string[] ReadDataFromTxt(string filepath)
    {
        TextAsset DataText = Resources.Load("Data/"+filepath) as TextAsset;
        if (DataText != null)
        {
            string datatext = DataText.text;
            string[] data = datatext.Split("\n");
            foreach(var item in data)
            {
                //Debug.Log(item);
            }
            return data;
        }
        else
        {
            Debug.LogError("Failed to load file: " + filepath);
            return null;
        }
    }
        
    
}