using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private ReticleScript reticleManager;

    private void OnMouseDown()
    {
        Debug.Log("Player clicked: " + this.gameObject.name);
        reticleManager.Selected(this.gameObject);
    }

    private void OnMouseUp()
    {
        Debug.Log("Player released: " + this.gameObject.name);
        reticleManager.Deselect();
    }
}
