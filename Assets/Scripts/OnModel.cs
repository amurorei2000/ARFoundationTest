using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnModel : MonoBehaviour
{
    public GameObject modeling;

    bool isOn = false;

    public void ToggleModeling()
    {
        isOn = !isOn;

        if(isOn)
        {
            modeling.SetActive(true);
        }
        else
        {
            modeling.SetActive(false);
        }
    }
}
