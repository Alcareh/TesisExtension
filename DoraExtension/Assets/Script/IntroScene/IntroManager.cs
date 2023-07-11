using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [Header("SignIn Menu")] 
    [SerializeField] public TMPro.TMP_Dropdown securityqOp;
    [SerializeField] private TMPro.TMP_Text warningText1;
    [SerializeField] private TMPro.TMP_Text warningText2;
    [SerializeField] private TMPro.TMP_Text warningText3;
    [SerializeField] private TMPro.TMP_Text warningText4;
    [SerializeField] private TMPro.TMP_Text warningText5;
    [SerializeField] private TMPro.TMP_Text warningText6;
    [SerializeField] public TMPro.TMP_InputField nameDB;
    [SerializeField] public TMPro.TMP_InputField mail;
    [SerializeField] public TMPro.TMP_InputField password1;
    [SerializeField] public TMPro.TMP_InputField password2;
    [SerializeField] public TMPro.TMP_InputField securityqAn;
    [SerializeField] public GameObject toastPanel;
    
    [Header("LogIn Menu")] 
    [SerializeField] public TMPro.TMP_InputField nameDBLogin;
    [SerializeField] public TMPro.TMP_InputField passwordLogin;
    
    [Header("LogIn Panels")]
    [SerializeField] private GameObject loginPanel;
    [Header("SignIn Panels")]
    [SerializeField] private GameObject signUpPanel;
    [SerializeField] private GameObject signUpDonePanel;
    [Header("Recover Panels")]
    [SerializeField] public GameObject recoverPanel;
    [SerializeField] public GameObject recover1;
    [SerializeField] public GameObject recover2;
    [SerializeField] public TMPro.TMP_Dropdown securityqOpRecover;
    [SerializeField] public TMPro.TMP_InputField mailRecover;
    [SerializeField] public TMPro.TMP_InputField securityqAnRecover;
    [SerializeField] public TMPro.TMP_Text nameRecover;
    [SerializeField] public TMPro.TMP_InputField password1Recover;
    [SerializeField] public TMPro.TMP_InputField password2Recover;
    [SerializeField] private TMPro.TMP_Text warningText7;
    [SerializeField] private TMPro.TMP_Text warningText8;
    [SerializeField] private TMPro.TMP_Text warningText9;
    [SerializeField] private TMPro.TMP_Text warningText10;
    [SerializeField] private TMPro.TMP_Text warningText11;

    [Header("RecoverData")] 
    [SerializeField] public string _idRecover;
    [SerializeField] public string nameDBRecover;
    


    [Header("Scripts")]
    public RegisterButtonBND registerButtonBND;
    public RecoverButtonBND recoverButtonBND;
    public UpdateButtonBND updateButtonBND;

    
    public void CheckParameters() //Valida que los campos estén llenos y de manera correcta.
    {
        List<TMP_Text> listica = new List<TMP_Text>(); //Lista para los warnings porque hay 2 clases
        listica.Add(warningText1);
        listica.Add(warningText2);
        listica.Add(warningText3);
        listica.Add(warningText4);
        listica.Add(warningText5);
        listica.Add(warningText6);
        if (CheckName(nameDB,warningText1) &&
            VerifyEmailAddress(mail,warningText2) &&
            CheckPasswords(password1,password2,warningText3,warningText4,listica) &&
            CheckSecurityQOp(securityqOp,warningText5) &&
            CheckSecurityQAn(securityqAn,warningText6))
        {
            //Sitodo es correcto envía los datos al backend
            registerButtonBND.EnviarRegistro();
        }
    }
 public void CheckAccountRecover() //Valida que los campos estén llenos y de manera correcta.
    {
        if (VerifyEmailAddress(mailRecover,warningText7) &&
            CheckSecurityQOp(securityqOpRecover,warningText8) &&
            CheckSecurityQAn(securityqAnRecover,warningText9))
        {
            //Sitodo es correcto envía los datos al backend recover 1 (mail, y preguntas de seguridad)
            recoverButtonBND.CheckInfo();
        }
    } 
 public void CheckAccountRecover2() //Valida que los campos estén llenos y de manera correcta.
 {
        List<TMP_Text> listica = new List<TMP_Text>();
        listica.Add(warningText7);
        listica.Add(warningText8);
        listica.Add(warningText8);
        listica.Add(warningText10);
        listica.Add(warningText11);
        if (CheckPasswords(password1Recover,password2Recover,warningText10,warningText11,listica))
        {
            //Sitodo es correcto envía los datos al backend recover 2 (envia password y hace update)
            updateButtonBND.CheckInfo();
        }
    }

    public bool CheckName(TMP_InputField checkName,TMP_Text warningText) //verifica el nombre y gestiona el warning
    {
        if (checkName.text.Length != 0)
        {
            warningText.gameObject.SetActive(false);
            return true;
        }
        warningText.gameObject.SetActive(true);
        return false;
    }
    
    public bool VerifyEmailAddress(TMP_InputField mail,TMP_Text warningText) //verifica el mail y gestiona el warning
    {
        string[] atCharacter;
        string [] dotCharacter;
        atCharacter = mail.text.Split("@"[0]);
        if(atCharacter.Length == 2){
            dotCharacter = atCharacter[1].Split("."[0]);
            if(dotCharacter.Length >= 2){
                if(dotCharacter[dotCharacter.Length - 1].Length == 0){
                    warningText.gameObject.SetActive(true);
                    return false;
                }
                else{
                    warningText.gameObject.SetActive(false);
                    return true;
                }
            }
            else{
                warningText.gameObject.SetActive(true);
                return false;
            }
        }
        else{
            warningText.gameObject.SetActive(true);
            return false;
        }
    }

    
//[ContextMenu("Check Password")] para checkear en play
//verifica las contraseñas y gestiona los warnings
    public bool CheckPasswords(TMP_InputField pass1,TMP_InputField pass2,TMP_Text warningTexta,TMP_Text warningTextb,List<TMP_Text> listica) // Verifica que el texto de las contraseñas esté escrito 2 veces igual y cambia colores.
    {
        if (pass1.text!=pass2.text)
        {
            var yellowColor = new Color32(249, 230, 91, 255);
            pass1.GetComponent<Image>().color = yellowColor;
            pass2.GetComponent<Image>().color = yellowColor;
            pass1.transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = yellowColor;
            pass2.transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = yellowColor;
            pass1.transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = yellowColor;
            pass2.transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = yellowColor;
            pass1.transform.GetChild(1).GetComponent<Image>().color = yellowColor;
            pass2.transform.GetChild(1).GetComponent<Image>().color= yellowColor;
            warningTexta.gameObject.SetActive(true);
            warningTextb.gameObject.SetActive(true);
            warningTexta.text = "Las contraseñas no coinciden.";
            warningTextb.text = "Las contraseñas no coinciden.";
            return false;
        }

        if (pass1.text.Length <5 || pass2.text.Length < 5)
        {
            warningTexta.gameObject.SetActive(true);
            warningTextb.gameObject.SetActive(true);
            warningTexta.text = "Selecciona una contraseña más segura.";
            warningTextb.text = "Selecciona una contraseña más segura.";
            return false;
        }
        CleanAll(listica,pass1,pass2);
        return true;
    }
    
    //verifica el dropdown y gestiona el warning
    public bool CheckSecurityQOp(TMP_Dropdown securityOp,TMP_Text warningText) //Verifica que el dropdown no se encuentre en opción vacía.
    {
        if (securityOp.value == 0)
        {
            warningText.gameObject.SetActive(true);
            return false;
        }
        warningText.gameObject.SetActive(false);
        return true;
        
    }
    //verifica la respuesta del dropdown y gestiona el warning
    public bool CheckSecurityQAn(TMP_InputField securityAn,TMP_Text warningText)
    {
        if (securityAn.text.Length != 0)
        {
            warningText.gameObject.SetActive(false);
            return true;
        }
        warningText.gameObject.SetActive(true);
        return false;
    }

//[ContextMenu("Check Cleaner")]para checkear en play
    public void CleanAll(List<TMP_Text> listica, TMP_InputField pass1, TMP_InputField pass2) //Limpia todos los errores y vuelve los colores a la normalidad.
    {
        foreach (var text in listica)
        {
            text.gameObject.SetActive(false);
        }
       var whiteColor = new Color32(236, 245, 248, 255);
       pass1.GetComponent<Image>().color = whiteColor;
       pass2.GetComponent<Image>().color = whiteColor;
        pass1.transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = whiteColor;
        pass2.transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = whiteColor;
        pass1.transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = whiteColor;
        pass2.transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = whiteColor;
        pass1.transform.GetChild(1).GetComponent<Image>().color = whiteColor;
        pass2.transform.GetChild(1).GetComponent<Image>().color = whiteColor;
    }

    //Registro correcto, apaga paneles
    public void CorrectSignUp()
    {
        signUpPanel.SetActive(false);
        signUpDonePanel.SetActive(true);
    }

    //Reinicio de recuperar contraseña
    public void RecoverPasswordOff()
    {
        recover1.SetActive(true);
        recover2.SetActive(false);
        recoverPanel.SetActive(false);
        loginPanel.SetActive(true);
    }
    
}
