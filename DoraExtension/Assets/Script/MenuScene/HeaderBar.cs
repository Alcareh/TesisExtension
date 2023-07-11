using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderBar : MonoBehaviour
{
    public void MoverBarra(Vector3 targetPosition,float cambio) //Empieza el movimiento de la barra dependiendo del target
    {
        StartCoroutine(LerpBarraMenu(targetPosition,cambio));
    }
    IEnumerator LerpBarraMenu(Vector3 targetPosition,float cambio) //Lerp para el efecto de movimiento
    {
        float time = 0;
        Vector3 startPosition = gameObject.transform.position;
        targetPosition = new Vector3(targetPosition.x+cambio, startPosition.y, startPosition.z);

        while (time < 0.2f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / 0.2f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
