using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedAvatar2 : MonoBehaviour
{
  
  [Header("Static BG")]
  [SerializeField] private List<GameObject> bgList;
  
  [Header("Selected Objects")]
  [SerializeField] public GameObject selected2;
  [SerializeField] public List<GameObject> contentObjects;
  public Sprite selectedAvatar2;
  public string selectedName2;
  private GameObject selectedBefore;
  
  [Header("Scripts")] [SerializeField] private InventoryManager inventoryManager;

  public void ActivarOrden() //Ordena los BGFijos
  {
    if (!gameObject.CompareTag("BgSlots"))
    {
      foreach (var staticBG in bgList)
      {
        staticBG.GetComponent<BaseBGAvatar>().ordenarFondos();
      }
    }
  }

  public void CargarActuales(GameObject actual) //Al entrar al inventario selecciona automáticamente los datos actuales
  {
    selected2.SetActive(true);
    if (actual.CompareTag("Avatar"))
    {
      actual.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
      selectedName2 = actual.GetComponent<Image>().sprite.name;
      selectedAvatar2 = actual.GetComponent<Image>().sprite;
      var localPosition = actual.transform.localPosition;
      selected2.transform.localPosition = localPosition;
    }

    if (selectedBefore!=null)//Para evitar nullPointerException al no haber seleccionado nada antes
    {
      selectedBefore.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
    selectedBefore = actual;
  }
  public void SelectedTwo()
  {
    var item = EventSystem.current.currentSelectedGameObject;
    if (!selected2.activeSelf) //Esto toca cambiarlo por el que ya tenga
    {
      inventoryManager.ReiniciarSeleccion();
      selected2.SetActive(true);
    }
    if (item.CompareTag("Avatar"))
    {
      item.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
      selectedName2 = item.GetComponent<Image>().sprite.name;
      selectedAvatar2 = item.GetComponent<Image>().sprite;
      var localPosition = item.transform.localPosition;
      selected2.transform.localPosition = localPosition;
    }
    if (selectedBefore!=null)//Para evitar nullPointerException al no haber seleccionado nada antes
    {
      selectedBefore.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
    selectedBefore = item;
  }
}
