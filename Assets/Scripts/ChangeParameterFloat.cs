using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeParameterFloat : MonoBehaviour
{
    private InputField input;
    public int min, max;
    public FloatSO parameter;

    private void Awake()
    {
        input = GetComponent<InputField>();
        input.text = parameter.value.ToString();
    }

    public void CheckCorrectValue()
    {
        float value = -1;
        float.TryParse(input.text, out value);

        if (value < min || value > max)
        {
            input.text = "Range: " + min + "-" + max;
        }
        else
        {
            if (!FindObjectOfType<GameManager>().GameIsRunning())
                parameter.value = value;
            else
                input.text = parameter.value.ToString();
        }
    }
}
