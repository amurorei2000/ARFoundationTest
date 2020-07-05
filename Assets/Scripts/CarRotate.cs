using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotate : MonoBehaviour
{
    public float rotSpeed = 100f;
    float my = 0;
        
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer("Model")))
            {
                float x = Input.GetAxis("Mouse X");
                my += x * rotSpeed * Time.deltaTime;

                transform.eulerAngles += new Vector3(0, -my, 0);
            }
        }
    }
}
