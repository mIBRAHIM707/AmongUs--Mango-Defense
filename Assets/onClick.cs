using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : MonoBehaviour
{
    [SerializeField] GameObject shopUI;

    private void OnMouseDown()
    {
        
        shopUI.SetActive(true);
       
    }

}
