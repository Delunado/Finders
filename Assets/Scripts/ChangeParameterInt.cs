using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeParameterInt : MonoBehaviour
{
    private InputField input;
    public int min, max;
    public IntSO parameter;

    private void Awake()
    {
        input = GetComponent<InputField>();
        input.text = parameter.value.ToString();
    }

    public void CheckCorrectValue()
    {
        int value = -1;
        int.TryParse(input.text, out value);

        if (value < min || value > max)
        {
            input.text = "Range: " + min + "-" + max;
        } else
        {
            if (!FindObjectOfType<GameManager>().GameIsRunning())
                parameter.value = value;
            else
                input.text = parameter.value.ToString();
        }
    }

}
