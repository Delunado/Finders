using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] transformButtons;

    private void Start()
    {
        foreach (GameObject button in transformButtons)
        {
            button.GetComponent<ScaleButton>().SetObjectToScale(this.transform.parent.transform);
        }
    }

    private void OnMouseEnter()
    {
        foreach (GameObject button in transformButtons)
        {
            button.SetActive(true);
        } 
    }

    private void OnMouseExit()
    {
        foreach (GameObject button in transformButtons)
        {
            if (!button.GetComponent<ScaleButton>().IsUsing)
                button.SetActive(false);
        }
    }
}
