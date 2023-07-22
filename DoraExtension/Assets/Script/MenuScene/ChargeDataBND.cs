using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class ChargeDataBND : MonoBehaviour
{
    private SesionManager mySesionManager;
    private WWWForm secureForm;
    
    [Header("Sesion")]
    public bool Sesion;
    public string host;
    public string _id;
    public string nameDB;
    
    [Header("Data")]
    public bool newAccount;
    public string fondoAvatar;
    public string avatarUser;
    public string progreso;
    public string habilidad;
    public string nivel;
    public int puntos;
    public bool[] notify;
    public bool notify2;
    public bool[] logros;
    public tierClass tier;
    public progressClass finalProgress;

    [Header("Referenncias")] 
    [SerializeField] private MenuManager menuManager;

    [SerializeField] private CSVManager csvManager;

    private void Start()
    {
        mySesionManager = FindObjectOfType<SesionManager>();
        this.Sesion = mySesionManager.Sesion;
        this.host = mySesionManager.host;
        this._id = mySesionManager._id;
        this.nameDB = mySesionManager.nameDB;
        TraerUser(_id);
    }

    public void TraerUser(string _id)
    {
        StartCoroutine(ConsultaBD(_id));
        StartCoroutine(Progress(_id));
        StartCoroutine(Tier());
    }
    
    public IEnumerator ConsultaBD(string _id)
    {
        secureForm = new WWWForm();
        secureForm.AddField("_id", _id);
        UnityWebRequest www = UnityWebRequest.Post(host+"user/userByID",secureForm);
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        var x = JsonUtility.FromJson<userClass>(temp);
        if (www.error == null)
        {
            Debug.Log(temp);
            newAccount = x.newAccount;
            fondoAvatar = x.fondoAvatar;
            avatarUser= x.avatarUser;
            progreso= x.progreso;
            habilidad= x.habilidad;
            nivel= x.nivel;
            puntos= x.puntos;
            notify= x.notify;
            notify2= x.notify2;
            logros= x.logros;
        }
        else
        {
            Debug.Log(temp);
            menuManager.toastPanel.transform.GetChild(0).GetComponent<TMP_Text>().text =
                "Error al conectar con el servidor";
            menuManager.toastPanel.GetComponent<Animator>().SetTrigger("ActivarToast");
        }
    }
    
    public IEnumerator Tier()
    {
        secureForm = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Get(host+"game/tier");
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        tier = JsonUtility.FromJson<tierClass>(temp);
        if (www.error == null)
        {
            Debug.Log(temp);
            menuManager.FirstChargeData(newAccount);
        }
        else
        {
            Debug.Log(temp);
            menuManager.toastPanel.transform.GetChild(0).GetComponent<TMP_Text>().text =
                "Error al conectar con el servidor";
            menuManager.toastPanel.GetComponent<Animator>().SetTrigger("ActivarToast");
        }
    }
    public IEnumerator Progress(string _id)
    {
        secureForm = new WWWForm();
        secureForm.AddField("user", _id);
        UnityWebRequest www = UnityWebRequest.Post(host+"game/progress",secureForm);
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        finalProgress= JsonUtility.FromJson<progressClass>(temp);
        if (www.error == null)
        {
            Debug.Log(temp);
        }
        else
        {
            Debug.Log(temp);
            menuManager.toastPanel.transform.GetChild(0).GetComponent<TMP_Text>().text =
                "Error al conectar con el servidor";
            menuManager.toastPanel.GetComponent<Animator>().SetTrigger("ActivarToast");
        }
    }
    
    public void SetFirstAvatar(string fondoAvatar,string avatarUser)
    {
        StartCoroutine(UpdateAvatar(fondoAvatar,avatarUser));
    }
    
    public IEnumerator UpdateAvatar(string fondoAvatar,string avatarUser)
    {
        secureForm = new WWWForm();
        secureForm.AddField("_id", _id);
        secureForm.AddField("fondoAvatar", fondoAvatar);
        secureForm.AddField("avatarUser", avatarUser);
        secureForm.AddField("newAccount","false");
        UnityWebRequest www = UnityWebRequest.Post(host+"user/updateData",secureForm);
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        var x = JsonUtility.FromJson<userClass>(temp);
        if (www.error == null)
        {
            Debug.Log(temp);
        }
        else
        {
            menuManager.toastPanel.transform.GetChild(0).GetComponent<TMP_Text>().text =
                 "Error al conectar con el servidor";
            menuManager.toastPanel.GetComponent<Animator>().SetTrigger("ActivarToast");
        }
    }
    
    public void TurnOffNotification(string _id)
    {
        StartCoroutine(TurnOffNotify(_id));
    }
    
    public IEnumerator TurnOffNotify(string _id)
    {
        secureForm = new WWWForm();
        secureForm.AddField("_id", _id);
        secureForm.AddField("notify.0", "false");
        secureForm.AddField("notify.1", "false");
        secureForm.AddField("notify.2", "false");
        secureForm.AddField("notify.3", "false");
        secureForm.AddField("notify.4", "false");
        UnityWebRequest www = UnityWebRequest.Post(host+"user/updateData",secureForm);
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        var x = JsonUtility.FromJson<userClass>(temp);
        if (www.error == null)
        {
           // Debug.Log(temp);
        }
        else
        {
            //Debug.Log(temp);
        }
    }
    
    public void TurnOffNotification2(string _id)
    {
        StartCoroutine(TurnOffNotify2(_id));
    }
    
    public IEnumerator TurnOffNotify2(string _id)
    {
        secureForm = new WWWForm();
        secureForm.AddField("_id", _id);
        secureForm.AddField("notify2", "false");
        UnityWebRequest www = UnityWebRequest.Post(host+"user/updateData",secureForm);
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        var x = JsonUtility.FromJson<userClass>(temp);
        if (www.error == null)
        {
            // Debug.Log(temp);
        }
        else
        {
            //Debug.Log(temp);
        }
    }
    
    [ContextMenu("Leer Json22")]
    public void SaveCSV()
    {
        
        DateTime dt=DateTime.Now;
        var fecha = dt.ToString("dd-MM-yyyy"); 
        StartCoroutine(SaveCSV2(fecha));
    }
    
    public IEnumerator SaveCSV2(string fecha)
    {
        secureForm = new WWWForm();
        
        secureForm.AddField("artro", csvManager.file1);
        secureForm.AddField("palpa", csvManager.file2);
        secureForm.AddField("target", csvManager.file3);
        secureForm.AddField("timer", csvManager.file4);
        secureForm.AddField("fecha", fecha);
        secureForm.AddField("user", _id);
        UnityWebRequest www = UnityWebRequest.Post(host+"data/create",secureForm); 
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        var x = JsonUtility.FromJson<userClass>(temp);
        if (www.error == null)
        {
            // Debug.Log(temp);
        }
        else
        {
            //Debug.Log(temp);
        }
    }
    public void LogOut()
    {
        Destroy(mySesionManager);
        SceneManager.LoadScene("Intro");
    }
    
}
