using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleButton : MonoBehaviour
{
    private bool isUsing;
    public bool IsUsing { get => isUsing; set => isUsing = value; }

    Transform objectToScale;

    Vector2 lastMousePos;

    private void OnMouseDown()
    {
        IsUsing = true;
    }

    private void OnMouseUp()
    {
        IsUsing = false;
    }

    private void Update()
    {
        if (IsUsing)
        {
            float mouseDifference = (Input.mousePosition.x - lastMousePos.x) * Time.deltaTime;

            objectToScale.localScale = new Vector3(
                Mathf.Clamp((objectToScale.localScale.x + mouseDifference), 0.1f, 2f),
                objectToScale.localScale.y, 
                objectToScale.localScale.z);

            lastMousePos = Input.mousePosition;
        }
    }

    public void SetObjectToScale(Transform transform)
    {
        objectToScale = transform;
    }


}
