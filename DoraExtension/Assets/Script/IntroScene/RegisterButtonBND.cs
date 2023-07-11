using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class RegisterButtonBND : MonoBehaviour
{
    private string host;
    private WWWForm secureForm;
    public string nameDB;
    public string mail;
    public string password;
    public string securityqOp;
    public string securityqAn;
    
    [Header("Scripts")]
    public IntroManager introManager;
    private SesionManager mySesionManager;
    
    
    private void Start()
    {
        mySesionManager = FindObjectOfType<SesionManager>();
        this.host = mySesionManager.host;
    }

    public void  EnviarRegistro() //Trae los datos de los gameobjects a variables y los manda al Task
    {
        nameDB = introManager.nameDB.text;
        mail = introManager.mail.text;
        password = introManager.password1.text;
        securityqOp = introManager.securityqOp.value.ToString();
        securityqAn = introManager.securityqAn.text;
        
        StartCoroutine(VerificarExistencia(nameDB));
    }

    public IEnumerator  VerificarExistencia(string name)
    {
        secureForm = new WWWForm();
        secureForm.AddField("name", name);
        UnityWebRequest www = UnityWebRequest.Post(host+"user/getUserSignUp",secureForm);
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        var x = JsonUtility.FromJson<userClass>(temp);
        if (www.error == null)
        {
            introManager.toastPanel.transform.GetChild(0).GetComponent<TMP_Text>().text =
                "Elige un nombre de usuario diferente por favor";
            introManager.toastPanel.GetComponent<Animator>().SetTrigger("ActivarToast");
        }
        else
        {
            StartCoroutine(EnviarDatos());
        }
    }

    public IEnumerator EnviarDatos()
    {
        secureForm = new WWWForm();
        secureForm.AddField("name", nameDB);
        secureForm.AddField("mail", mail);
        secureForm.AddField("password", password);
        secureForm.AddField("securityqOp", securityqOp);
        secureForm.AddField("securityqAn", securityqAn);
        UnityWebRequest www = UnityWebRequest.Post(host+"user/auth/create",secureForm);
        yield return www.SendWebRequest();
        string temp = www.downloadHandler.text;
        var x = JsonUtility.FromJson<userClass>(temp);
        if (www.error == null)
        {
            //Debug.Log(temp);
            yield return new WaitForSeconds(1.0f); //1 segundo
            introManager.CorrectSignUp();
        }
        else
        {
            //Debug.Log(temp);
            introManager.toastPanel.transform.GetChild(0).GetComponent<TMP_Text>().text =
                "Error al conectar con el servidor";
            introManager.toastPanel.GetComponent<Animator>().SetTrigger("ActivarToast");
        }
        
    }
}
