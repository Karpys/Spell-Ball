using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiEnnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Ennemy;

    void OnGUI()
    {
        /*
        if (GUI.Button(new Rect(10, 150, 200, 50), "Disable Ennemy Movement"))
        {
            if (Ennemy)
            {
                Ennemy.GetComponent<IA_BasicEnemy>().CanMove = !Ennemy.GetComponent<IA_BasicEnemy>().CanMove;
                Ennemy.GetComponent<IA_BasicEnemy>().UpdateMovement();
            }
        }
        */
    }
}
