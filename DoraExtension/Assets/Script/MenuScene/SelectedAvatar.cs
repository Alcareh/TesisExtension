using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedAvatar : MonoBehaviour
{
    [Header("Selected Objects")]
    [SerializeField] private GameObject selected1;
    public Sprite selectedAvatar;
    public string selectedName;
    private GameObject selectedBefore;
    
    public void SelectedOne() //Hace el efecto del avatar seleccionado y a su vez guarda el sprite en la variable selectedAvatar
    {
        var item = EventSystem.current.currentSelectedGameObject;
        if (!selected1.activeSelf) //Primer click para que aparezca que avatar tiene seleccionado.
        {
            selected1.SetActive(true);
        }
        if (item.CompareTag("Avatar"))
        {
            item.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            selectedName = item.GetComponent<Image>().sprite.name;
            selectedAvatar = item.GetComponent<Image>().sprite;
            var localPosition = item.transform.localPosition;
            selected1.transform.localPosition = localPosition;
        }
        if (selectedBefore!=null)//Para evitar nullPointerException al no haber seleccionado nada antes
        {
            selectedBefore.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        selectedBefore = item;
    }
}
