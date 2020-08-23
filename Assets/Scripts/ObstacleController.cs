using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleController : MonoBehaviour
{
    bool isClicked;
    bool onDestroyer;
    bool isPlayStarted;

    private UnityAction listenerBlockObstacleEditor;

    private void Awake()
    {
        listenerBlockObstacleEditor = new UnityAction(ChangeCanEdit);
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventsNames.startPlayMode, listenerBlockObstacleEditor);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventsNames.startPlayMode, listenerBlockObstacleEditor);
    }

    public void ChangeCanEdit()
    {
        isPlayStarted = !isPlayStarted;
    }

    private void Start()
    {
        isPlayStarted = false;
        isClicked = false;
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseUp()
    {
        if (onDestroyer)
        {
            Destroy(gameObject);
        }

        isClicked = false;
    }

    private void Update()
    {
        if (isClicked && !isPlayStarted)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 10);

            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            onDestroyer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            onDestroyer = false;
        }
    }
}
