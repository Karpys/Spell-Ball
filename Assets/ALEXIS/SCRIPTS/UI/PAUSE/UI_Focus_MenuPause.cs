using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Focus_MenuPause : MonoBehaviour
{
    UI_MenuPause refMenu;

    [Header("BUTTONS")]
    public GameObject play;


    private void OnEnable()
    {
        play.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(play);



        refMenu = FindObjectOfType<UI_MenuPause>();
        refMenu.UIIndex = 0;
    }

}
