using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabManager : MonoBehaviour
{

    [Header("Páneles")] 
    [SerializeField] public GameObject[] paneles;
    [SerializeField] public  GameObject panelSelected;
    [Header("TMPro Text Inputs")] 
    [SerializeField] public TMP_InputField[] panel1Fields;
    [SerializeField] public TMP_InputField[] panel2Fields;
    [SerializeField] public TMP_InputField[] panel3Fields;
    
    [Header("Scripts")] 
    [SerializeField] public LoginButtonBND loginButtonBND;
    [SerializeField] public IntroManager introManager;


    public int inputSelected;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift) ||
            Input.GetKeyDown(KeyCode.Tab)&&Input.GetKey(KeyCode.RightShift))
        {
            SelectPanel();
            inputSelected--;
            if (panelSelected==paneles[0]|| panelSelected==paneles[2]){
                if (inputSelected < 0)
                {
                    inputSelected = 1;
                }
            }else if (panelSelected == paneles[1]){
                if (inputSelected < 0)
                {
                    inputSelected = 4;
                }
            }

            SelectInputField(panelSelected);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            SelectPanel();
            inputSelected++;
            if (panelSelected==paneles[0] || panelSelected==paneles[2]){
                if (inputSelected > 1){
                    inputSelected = 0;
                }
            }else if (panelSelected == paneles[1])
            {
                if (inputSelected > 4){
                    inputSelected = 0;
                }
            }

            SelectInputField(panelSelected);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            SelectPanel();
            SelectButton(panelSelected);
        }
    }
    
    
    public void SelectInputField(GameObject panel)
    {
        if (panel==paneles[0])
        {
            switch (inputSelected)
            {
                case 0:
                    panel1Fields[0].Select();
                    break;
                case 1:
                    panel1Fields[1].Select();
                    break;
            }
        }else if (panel == paneles[1])
        {
            switch (inputSelected)
            {
                case 0:
                    panel2Fields[0].Select();
                    break;
                case 1:
                    panel2Fields[1].Select();
                    break;
                case 2:
                    panel2Fields[2].Select();
                    break;
                case 3:
                    panel2Fields[3].Select();
                    break;
                case 4:
                    panel2Fields[4].Select();
                    break;
            }
        }else if (panel==paneles[2])
        {
            switch (inputSelected)
            {
                case 0:
                    panel3Fields[0].Select();
                    break;
                case 1:
                    panel3Fields[1].Select();
                    break;
            }
        }
    }

    public void SelectButton(GameObject panel)
    {
        if (panel == paneles[0])
        {
            loginButtonBND.EnviarSesion();
        }else if (panel == paneles[1])
        {
            introManager.CheckParameters();
        }else if (panel == paneles[2])
        {
            introManager.CheckAccountRecover();
        }
    }

    public void SelectPanel()
    {
        foreach (var panel in paneles)
        {
            if (panel.activeSelf)
            {
                panelSelected= panel;
            }
        }
    }
}


