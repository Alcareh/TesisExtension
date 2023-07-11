using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    // este script es para que haga algo parecido al 1, seleccionando en qué avatar está y lo ponga en profile.
    //y toca crear un nuevo botón de guardar y eso

    [Header("InventorySlots")] [SerializeField]
    private GameObject bgSlot;

    [SerializeField] private List<GameObject> avatarSlots;

    [Header("Scripts")]
    [SerializeField] private MenuManager menuManager;

    [SerializeField] private ChargeDataBND chargeDataBND;

    [Header("Variables Importantes")] 
    private string fondoAvatar;
    private string avatarUser;



    public void MostrarAvatares(string numeritoBack)
    {
        numeritoBack=(Int32.Parse(numeritoBack)-1).ToString();
        foreach (var avatarGroup in avatarSlots) //primero los apaga por si entran en dos cuentas diferentes
        {
            avatarGroup.SetActive(false);
        }

        for (int i = 0; i <= Int32.Parse(numeritoBack); i++) //Prende los que lleguen del backend y ordena el BG
        {
            avatarSlots[i].SetActive(true);
            avatarSlots[i].GetComponent<SelectedAvatar2>().ActivarOrden();
        }

        TraerActuales();
    }

    public void TraerActuales() //Entra a cada uno de los objetos y verifica cual es el que tiene puesto para mostrarlo con el selector
    {
        foreach (var actual in bgSlot.GetComponent<SelectedAvatar2>().contentObjects)
        {
            if (actual.GetComponent<Image>().sprite == menuManager.profileBG.GetComponent<Image>().sprite)
            {
                bgSlot.GetComponent<SelectedAvatar2>().CargarActuales(actual);
            }
        }

        foreach (var avatarGroup in avatarSlots)
        {
            if (avatarGroup.activeSelf)
            {
                foreach (var actual in avatarGroup.GetComponent<SelectedAvatar2>().contentObjects)
                {
                    if (actual.GetComponent<Image>().sprite == menuManager.profileAvatar.GetComponent<Image>().sprite)
                    {
                        avatarGroup.GetComponent<SelectedAvatar2>().CargarActuales(actual);
                    }
                }
            }
        }

    }

    public void ReiniciarSeleccion() //Al cambiar de container de los avatares se necesita reiniciar el selector
    {
        foreach (var avatarGroup in avatarSlots)
        {
            avatarGroup.GetComponent<SelectedAvatar2>().selectedName2 = "";
            avatarGroup.GetComponent<SelectedAvatar2>().selected2.SetActive(false);
            avatarGroup.GetComponent<SelectedAvatar2>().selectedAvatar2 = null;
            foreach (var avatar in avatarGroup.GetComponent<SelectedAvatar2>().contentObjects)
            {
                avatar.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    public void GuardarAvatar() //Cambia los valores viausles del avatar y manda los datos al backend.
    {
        //acá manda datos al backend.
        CheckBGAvatarLote();
        //chargeDataBND.SetFirstAvatar(bgSlot.GetComponent<SelectedAvatar2>().selectedName,);
        menuManager.profileBG.GetComponent<Image>().sprite = bgSlot.GetComponent<SelectedAvatar2>().selectedAvatar2;
        foreach (var avatarGroup in avatarSlots)
        {
            if (avatarGroup.GetComponent<SelectedAvatar2>().selectedAvatar2!=null)
            {
                menuManager.profileAvatar.GetComponent<Image>().sprite = avatarGroup.GetComponent<SelectedAvatar2>().selectedAvatar2;
            }
        }
    }

    public void CheckBGAvatarLote()
    {
        fondoAvatar = bgSlot.GetComponent<SelectedAvatar2>().selectedName2;
        foreach (var avatarGroup in avatarSlots)
        {
            if (avatarGroup.GetComponent<SelectedAvatar2>().selectedName2!="")
            {
                avatarUser = avatarGroup.GetComponent<SelectedAvatar2>().selectedName2;
            }
        }
        chargeDataBND.SetFirstAvatar(fondoAvatar,avatarUser);
    }
}
