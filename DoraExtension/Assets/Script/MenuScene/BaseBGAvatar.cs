using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBGAvatar : MonoBehaviour
{
 [SerializeField] private GameObject avatar;
 
 public void ordenarFondos()  //Cada fondo de avatar va a tener que acomodarse en el respetivo avatar
 {
     gameObject.transform.localPosition = avatar.transform.localPosition;
 }
}
