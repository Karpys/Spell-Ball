using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ChooseOptions : MonoBehaviour
{

    [Header("BUTTONS")]
    public GameObject buttonGraph;

    Manager_MainMenu refMenu;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonGraph);

        refMenu = FindObjectOfType<Manager_MainMenu>();
        refMenu.UIIndex = 1;
    }

}
