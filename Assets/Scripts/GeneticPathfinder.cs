﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticPathfinder : MonoBehaviour
{
    private DNA dna;
    public DNA Dna { get => dna; set => dna = value; }

    int pathIndex = 0;

    public Color deathColor;

    bool isIniciated = false;
    private bool hasFinished = false;
    public bool HasFinished { get => hasFinished; set => hasFinished = value; }
    bool hasTouchedSomething = false;

    Vector2 target;
    Vector2 nextPoint;

    Quaternion targetRotation;

    LineRenderer lr;
    List<Vector2> travelledPath = new List<Vector2>();

    public LayerMask obstacleLayer;

    //Caracteristicas
    public float creatureSpeed;
    public float pathMultiplier;
    public float rotationSpeed;

    public float Fitness
    {
        get {
            float dist = Vector2.Distance(transform.position, target);

            if (dist <= 0)
            {
                dist = Mathf.Epsilon;
            }

            RaycastHit2D[] obstacles = Physics2D.RaycastAll(transform.position, target, obstacleLayer);
            float obstacleModifier = 1f - (0.1f * obstacles.Length);
            return (60/dist) * (hasTouchedSomething ? 0.65f : 1f) * obstacleModifier; //The near you are, the bigger.
        }
    }

    public void InitCreature(DNA newDna, Vector2 target)
    {
        lr = GetComponent<LineRenderer>();
        Dna = newDna;
        this.target = target;
        nextPoint = transform.position;
        travelledPath.Add(nextPoint);
        isIniciated = true;
    }

    private void Update()
    {
        if (isIniciated && !HasFinished)
        {
            if (pathIndex == Dna.genes.Count)
            {
                StoppedEnd(); //Cambiar a final malo
            }

            if (Vector2.Distance(transform.position, target) < 0.5f)
            {
                TargetEnd(); //Cambiar a final bueno
            }

            if ((Vector2)transform.position == nextPoint)
            {
                nextPoint = (Vector2)transform.position + Dna.genes[pathIndex++] * pathMultiplier;
                travelledPath.Add(nextPoint);
                targetRotation = LookAt2D(nextPoint);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, nextPoint, creatureSpeed * Time.deltaTime);
            }

            if (transform.rotation != targetRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            RenderLine();
        }

        
    }

    private void StoppedEnd()
    {
        HasFinished = true;
    }

    private void TargetEnd()
    {
        HasFinished = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjetoMalo"))
        {
            HasFinished = true;
            hasTouchedSomething = true;
            GetComponent<SpriteRenderer>().color = deathColor;
        }
    }

    /// <summary>
    /// A function similar to LookAt, but a 2D version.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="angleOffset"></param>
    /// <returns></returns>
    public Quaternion LookAt2D(Vector2 target, float angleOffset = -90)
    {
        Vector2 fromTo = (target - (Vector2)transform.position).normalized;
        float zRotation = Mathf.Atan2(fromTo.y, fromTo.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, zRotation * angleOffset);
    }

    public void RenderLine()
    {
        List<Vector3> linePoints = new List<Vector3>();
        if (travelledPath.Count > 2)
        {
            for (int i = 0; i < travelledPath.Count - 1; i++)
            {
                linePoints.Add(travelledPath[i]);
            }
        } else
        {
            linePoints.Add(travelledPath[0]);
            linePoints.Add(transform.position);
        }

        lr.positionCount = linePoints.Count;
        lr.SetPositions(linePoints.ToArray());
    }
}
