using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CSVManager : MonoBehaviour
{
    public string file1;
    public string file2;
    public string file3;
    public string file4;
    
    [ContextMenu("Leer Json")]
    public void BuscarCSV()
    {
        var path = getPath();
        file1 = ReadFileAtLocation(path,"ArtroTxt.csv");
        file2 =  ReadFileAtLocation(path,"PalpaTxt.csv");
        file3 = ReadFileAtLocation(path,"TargetText.csv");
        file4 = ReadFileAtLocation(path,"TimerText.csv");
    }
    
    
    public string ReadFileAtLocation(string path,string name)
    {
        var file = "";
        if (File.Exists(path + name))
        {
            FileStream fileStream = new FileStream(path+name, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fileStream);
            file = read.ReadToEnd();
        }
        return file;
    }
    
    private string getPath ()
    { 
        return "E:/Alejo/Documents/GitHub/Tesis/calibracion HoloOptypp/Assets/DATA_CSV/";
    }    
}

