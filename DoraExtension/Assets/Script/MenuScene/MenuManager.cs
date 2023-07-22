using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using Int32 = System.Int32;

public class MenuManager : MonoBehaviour
{
    [Header("Header Objects")]
    [SerializeField] private GameObject headerBar;
    [SerializeField] public TMP_Text inicioText;
    [SerializeField] public TMP_Text logrosText;
    [SerializeField] public TMP_Text inventarioText;
    [SerializeField] public TMP_Text progresoText;
    [SerializeField] public GameObject toastPanel;

    [Header("Primera vez avatar")] 
    [SerializeField] private GameObject welcomeOnce;
    [SerializeField] private TMP_Text userNameText1;
    [SerializeField] private GameObject bgContainer;
    [SerializeField] private GameObject avatarContainer;

    [Header("Profile Info")] 
    [SerializeField] private GameObject fakeProfile;
    [SerializeField] public GameObject profileBG;
    [SerializeField] public GameObject profileAvatar;
    [SerializeField] public TMP_Text profileName;
    [SerializeField] public GameObject profileLvlImg;
    [SerializeField] public TMP_Text profileLvlTxt;
    [SerializeField] public GameObject profileProgressImg;
    [SerializeField] public TMP_Text profileProgressTxt;
    [SerializeField] public GameObject profileHabilityImg;
    [SerializeField] public TMP_Text profileHabilityTxt;
    [SerializeField] public TMP_Text profilePoints;
    [SerializeField] public GameObject profileNotify;
    [SerializeField] public List<Sprite> spritesBG;
    [SerializeField] public List<Sprite> spritesAvatar;
    [SerializeField] public List<Sprite> spritesGoal;
    [SerializeField] public List<String> nameLvl;

    
    [Header("Header View")] 
    [SerializeField] private GameObject homeMenu;
    [SerializeField] private GameObject homeMenuOnce;
    [SerializeField] private GameObject homeGoals;
    [SerializeField] private GameObject homeInventory;
    [SerializeField] private GameObject homeProgress;
    [SerializeField] private GameObject[] notifyPanel;
    [SerializeField] private GameObject notifyGroup;
    [SerializeField] private GameObject congratPanel;
    [SerializeField] private TMP_Text congratPanelTxt;
    [SerializeField] private GameObject logOutPanel;
    
    [Header("Goal View")] 
    [SerializeField] private List<GameObject> goalsOff;
    [SerializeField] private List<GameObject> goalsOn;
    
    [Header("Meno View")] 
    [SerializeField] private List<GameObject> tierList;
    
    [Header("Progress View")] 
    [SerializeField] private List<GameObject> progressItem;

    [Header("Referenncias")] 
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private ChargeDataBND chargeDataBND;

    [Header("Práctica")] 
    [SerializeField] private GameObject startPracticePanel;
    [SerializeField] private GameObject endPracticePanel;

    public void Inicio()//Cambia a menú inicio
    {
        headerBar.GetComponent<HeaderBar>().MoverBarra(inicioText.transform.position, -10f);
        //Check si es la primera vez que ingresa prender WelcomeOnceContainer, de lo contrario solo el container de Inicio
        Desactivar();
        if (chargeDataBND.newAccount)
        {
            homeMenuOnce.SetActive(true);
        }
        else
        {
            homeMenu.SetActive(true);
        }
    }
    
    public void Logros()//Cambia a menú logros
    {
        headerBar.GetComponent<HeaderBar>().MoverBarra(logrosText.transform.position, -9.5f);
        Desactivar();
        homeGoals.SetActive(true);
        ChargeGoals();
    }
    
    public void Inventario()//Cambia a menú inventario
    {
        headerBar.GetComponent<HeaderBar>().MoverBarra(inventarioText.transform.position, -9f);
        Desactivar();
        homeInventory.SetActive(true);
        inventoryManager.MostrarAvatares(chargeDataBND.nivel);
    }
    
    public void Progreso()//Cambia a menú retos
    {
        headerBar.GetComponent<HeaderBar>().MoverBarra(progresoText.transform.position, -9f);
        Desactivar();
        homeProgress.SetActive(true);
        ChargeProgress();
    }

    public void Desactivar()
    {
        homeMenu.SetActive(false);
        homeMenuOnce.SetActive(false);
        homeGoals.SetActive(false);
        homeInventory.SetActive(false);
        homeProgress.SetActive(false);
    }
   
    public void AceptarAvatarInicio(){
        if (bgContainer.GetComponent<SelectedAvatar>().selectedAvatar!=null&&avatarContainer.GetComponent<SelectedAvatar>().selectedAvatar!=null)
        {
            chargeDataBND.SetFirstAvatar(bgContainer.GetComponent<SelectedAvatar>().selectedName,avatarContainer.GetComponent<SelectedAvatar>().selectedName);
            foreach (var picture in spritesBG)
            {
                if (bgContainer.GetComponent<SelectedAvatar>().selectedName==picture.name)
                {
                    profileBG.GetComponent<Image>().sprite = picture;
                }
            }
            foreach (var picture in spritesAvatar)
            {
                if (avatarContainer.GetComponent<SelectedAvatar>().selectedName==picture.name)
                {
                    profileAvatar.GetComponent<Image>().sprite = picture;
                }
            }
            welcomeOnce.SetActive(false);
            ChargeHeader();
        }else{ //Muestra mensaje de error en toast.
            toastPanel.transform.GetChild(0).GetComponent<TMP_Text>().text =
                "Selecciona un fondo y un avatar.";
            toastPanel.GetComponent<Animator>().SetTrigger("ActivarToast");
        }
    }

    public void NotifyButton() //Apagar y prender el botón de notificaciones
    {
        if (notifyGroup.activeSelf)
        {
            if (profileNotify.activeSelf){
                chargeDataBND.TurnOffNotification(chargeDataBND._id);
            }
            notifyGroup.SetActive(false);
        }
        else
        { 
            notifyGroup.SetActive(true);
        }
    }

    public void CongratsDone()
    {
        chargeDataBND.TurnOffNotification2(chargeDataBND._id);
    }
    
    public void ProfileButton() //Apagar y prender el botón de notificaciones
    {
        if (!logOutPanel.activeSelf)
        {
            logOutPanel.SetActive(true);
        }
        else
        {
            logOutPanel.SetActive(false);
        }
    }

    public void FirstChargeData(bool newAcc)
    {
        if (newAcc)
        {
            welcomeOnce.SetActive(true);
            homeMenuOnce.SetActive(true);
            userNameText1.text = "Bienvenid@ " + chargeDataBND.nameDB;
        }
        else
        {
            homeMenu.SetActive(true);
            ChargeHeader();
            ChargeMenu();
        }
    }

    public void ChargeHeader()
    {
        if (fakeProfile.activeSelf)
        {
            fakeProfile.SetActive(false);
        }

        foreach (var picture in spritesBG)
        {
            if (chargeDataBND.fondoAvatar==picture.name)
            {
                profileBG.GetComponent<Image>().sprite = picture;
            }
        }
        foreach (var picture in spritesAvatar)
        {
            if (chargeDataBND.avatarUser==picture.name)
            {
                profileAvatar.GetComponent<Image>().sprite = picture;
            }
        }
        profileName.text = chargeDataBND.nameDB;
        for (int i = chargeDataBND.logros.Length - 1; i >= 0; i--)
        {
            if (chargeDataBND.logros[i])
            {
                profileLvlImg.GetComponent<Image>().sprite = spritesGoal[i+1];
                break;
            }
            
        }
        //profileLvlImg.GetComponent<Image>().sprite = spritesGoal[Int32.Parse(chargeDataBND.logros)];
        profileLvlTxt.text = nameLvl[Int32.Parse(chargeDataBND.nivel) - 1];
        profileProgressImg.GetComponent<Image>().fillAmount = float.Parse(chargeDataBND.progreso)/100;
        profileProgressTxt.text = chargeDataBND.progreso;
        profileHabilityImg.GetComponent<Image>().fillAmount = float.Parse(chargeDataBND.habilidad)/100;
        profileHabilityTxt.text = chargeDataBND.habilidad;
        profilePoints.text = chargeDataBND.puntos.ToString();
        
        for (int i = 0; i < chargeDataBND.logros.Length; i++)
        {
            if (chargeDataBND.notify[i])
            {
                profileNotify.SetActive(true);
                break;
            }
        }
        
        if (profileNotify.activeSelf) // Si está prendido el botón rojo cambia la imagen del logro en notifiación
        {
            for (int i = 0; i < chargeDataBND.notify.Length; i++)
            {
                if (chargeDataBND.notify[i])
                {
                    notifyPanel[i].SetActive(true);
                    notifyPanel[i].transform.GetChild(1).GetComponent<Image>().sprite = spritesGoal[i+1];
                    notifyPanel[i].transform.GetChild(2).GetComponent<TMP_Text>().text = "Ganaste un nuevo logro";
                }
                
            }
        }

        if (chargeDataBND.notify2)
        {
            congratPanel.SetActive(true);
            congratPanelTxt.text=nameLvl[Int32.Parse(chargeDataBND.nivel) - 1];
        }
    }

    public void ChargeMenu()
    {
        var data = chargeDataBND.tier.items;
        for (int i = 0; i < data.Length; i++)
        {
            tierList[i].SetActive(true);
            foreach (var picture in spritesBG)
            {
                if (data[i].fondoAvatar==picture.name)
                {
                    tierList[i].transform.GetChild(1).GetComponent<Image>().sprite = picture;
                }
            }
            
            foreach (var picture in spritesAvatar)
            {
                if (data[i].avatarUser==picture.name)
                {
                    tierList[i].transform.GetChild(2).GetComponent<Image>().sprite = picture;
                }
            }

            tierList[i].transform.GetChild(3).GetComponent<TMP_Text>().text = data[i].name;
            tierList[i].transform.GetChild(4).GetComponent<TMP_Text>().text = data[i].puntos.ToString();
            
            TimeSpan time = TimeSpan.FromSeconds(data[i].tiempo);
            string text = time.ToString(@"mm\:ss");
            tierList[i].transform.GetChild(5).GetComponent<TMP_Text>().text = (text+" min");
        }
    }
    public void ChargeGoals()
    {
        for (int i = 0; i < chargeDataBND.logros.Length; i++)
        {
            if (chargeDataBND.logros[i])
            {
                goalsOff[i].SetActive(false);
                goalsOn[i].SetActive(true);
            }
        }
    } 
    public void ChargeProgress()
    {
        var data = chargeDataBND.finalProgress.progresses;
        for (int i = 0; i < data.Length; i++)
        {
            progressItem[i].SetActive(true);
            progressItem[i].transform.GetChild(6).GetComponent<TMP_Text>().text = data[i].fecha;
            progressItem[i].transform.GetChild(7).GetComponent<TMP_Text>().text = data[i].lvl;
            progressItem[i].transform.GetChild(8).GetComponent<TMP_Text>().text = data[i].puntos.ToString();
            TimeSpan time = TimeSpan.FromSeconds(data[i].tiempo);
            string text = time.ToString(@"mm\:ss");
            progressItem[i].transform.GetChild(9).GetComponent<TMP_Text>().text = (text+" min");
        }
    }

    public void IniciarPractica()
    {
        StartCoroutine(PracticaEnProceso());
    }

    public IEnumerator PracticaEnProceso()
    {
        yield return new WaitForSeconds(120.0f);
        startPracticePanel.SetActive(false);
        endPracticePanel.SetActive(true);
    }
    
    

}
