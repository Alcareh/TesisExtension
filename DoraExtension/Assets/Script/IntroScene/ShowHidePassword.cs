using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHidePassword : MonoBehaviour
{
    [Header("User UX")] 
    [SerializeField] private TMPro.TMP_InputField passwordField;
    [SerializeField] private Image showHideButton;
    [SerializeField] private List<Sprite> eyes;
    

    public void ShowHidePasswordMethod() //Cambia el tipo de input para poder leer/ocultar contraseña además de cambiar ícono.
    {
        if (passwordField.contentType == TMP_InputField.ContentType.Password)
        {
            passwordField.contentType = TMP_InputField.ContentType.Standard;
            showHideButton.sprite = eyes[0];
            showHideButton.SetNativeSize();
        }
        else
        {
            passwordField.contentType = TMP_InputField.ContentType.Password;
            showHideButton.sprite = eyes[1];
            showHideButton.SetNativeSize();
        }
        passwordField.ForceLabelUpdate(); //Obliga a actualizar el campo.
    }
}
