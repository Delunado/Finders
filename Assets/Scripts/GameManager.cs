using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PopulationController population;
    [SerializeField] Text genText;

    private bool gameIsStarted;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        genText.text = ("Gen: " + population.Gen); 
    }

    public void Create(GameObject obstacle)
    {
        if (!gameIsStarted)
            Instantiate(obstacle, Vector3.zero, Quaternion.identity);
    }

    public void SetGameState(bool value)
    {
        gameIsStarted = value;
    }

    public bool GameIsRunning() => gameIsStarted;

}
