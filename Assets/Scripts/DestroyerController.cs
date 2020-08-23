using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerController : MonoBehaviour
{
    SpriteRenderer sprRenderer;

    Color normalColor;
    public Color destroyColor;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        normalColor = sprRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjetoMalo"))
        {
            sprRenderer.color = destroyColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjetoMalo"))
        {
            sprRenderer.color = normalColor;
        }
    }
}
