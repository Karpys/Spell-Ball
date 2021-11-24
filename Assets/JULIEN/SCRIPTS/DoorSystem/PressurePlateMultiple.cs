using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateMultiple : MonoBehaviour
{
    [SerializeField] private DoorWithMultipleButton _door;

    private Material originalMaterial;

    public ColorEnum playerColorNeeded;

    private void Awake()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
    }

    public void SetUsedRune(bool isUsed)
    {
        if (isUsed)
        {
            Material newMat = new Material(originalMaterial.shader);
            newMat.CopyPropertiesFromMaterial(originalMaterial);
            switch (playerColorNeeded)
            {
                case ColorEnum.RED:
                    newMat.color = Color.red;
                    break;
                case ColorEnum.BLEU:
                    newMat.color = Color.blue;
                    break;
                case ColorEnum.GREEN:
                    newMat.color = Color.green;
                    break;
                case ColorEnum.ORANGE:
                    newMat.color = Color.HSVToRGB(0.1f, 1f, 1f);;
                    break;
                default:
                    newMat.color = Color.white;
                    break;
            }

            GetComponent<MeshRenderer>().material = newMat;
        }
        else
        {
            GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _door.OnTriggerEnterEvent(other, this);
    }

    private void OnTriggerExit(Collider other)
    {
        _door.OnTriggerExitEvent(other, this);
    }

    private void OnTriggerStay(Collider other)
    {
        _door.OnTriggerStayEvent(other, this);
    }
}
