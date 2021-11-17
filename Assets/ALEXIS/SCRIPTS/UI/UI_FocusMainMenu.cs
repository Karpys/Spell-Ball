using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_FocusMainMenu : MonoBehaviour
{
    Manager_MainMenu refMenu;

    [Header("BUTTONS")]
    public GameObject play;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(play);

        play.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        refMenu = FindObjectOfType<Manager_MainMenu>();
        refMenu.UIIndex = 0;
    }

}
