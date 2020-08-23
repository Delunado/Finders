using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationController : MonoBehaviour
{
    List<GeneticPathfinder> population = new List<GeneticPathfinder>();

    public GameObject creaturePrefab;
    public IntSO populationSize;
    public FloatSO cutoff; //Percentage about how many will die or live.
    public IntSO survivorKeep;

    public IntSO genomeLength;

    public Transform spawnPoint;
    public Transform end;

    private int gen;
    public int Gen { get => gen; set => gen = value; }

    public FloatSO mutationRate;

    private bool hasStarted;

    private void Update()
    {
        if (hasStarted)
        {
            if (!HasActiveIndividuals())
            {
                NextGeneration();
            }
        }
    }

    private void InitPopulation()
    {
        for (int i = 0; i < populationSize.value; i++)
        {
            GameObject creature = Instantiate(creaturePrefab, spawnPoint.position, Quaternion.identity);
            creature.GetComponent<GeneticPathfinder>().InitCreature(new DNA(genomeLength.value), end.position);
            population.Add(creature.GetComponent<GeneticPathfinder>());
        }
    }

    void NextGeneration()
    {
        int survivorsCut = Mathf.RoundToInt(populationSize.value * cutoff.value);
        List<GeneticPathfinder> survivors = new List<GeneticPathfinder>();

        //Here, we get the best survivors of the generation
        for (int i = 0; i < survivorsCut; i++)
        {
            survivors.Add(GetFittest());
        }

        //Then, we destroy all the population
        for (int i = 0; i < population.Count; i++)
        {
            Destroy(population[i].gameObject);
        }

        population.Clear();

        //We create some individuals using the best survivors DNA
        if (survivorKeep.value > survivors.Count)
        {
            survivorKeep.value = survivors.Count;
        }

        for (int i = 0; i < survivorKeep.value; i++)
        {
            GameObject creature = Instantiate(creaturePrefab, spawnPoint.position, Quaternion.identity);
            creature.GetComponent<GeneticPathfinder>().InitCreature(survivors[i].Dna, end.position);
            population.Add(creature.GetComponent<GeneticPathfinder>());
        }

        //Repopulating
        while (population.Count < populationSize.value)
        {
            for (int i = 0; i < survivors.Count; i++)
            {
                GameObject creature = Instantiate(creaturePrefab, spawnPoint.position, Quaternion.identity);
                // Here we are using the DNA of the 10 best individuals of the previous generation.
                creature.GetComponent<GeneticPathfinder>().InitCreature(new DNA(survivors[i].Dna, survivors[Random.Range(0, 10)].Dna, mutationRate.value), end.position); 
                population.Add(creature.GetComponent<GeneticPathfinder>());

                if (population.Count  >= populationSize.value)
                {
                    break;
                }
            }
        }

        //We destroy the survivors.
        for (int i = 0; i < survivors.Count; i++)
        {
            Destroy(survivors[i].gameObject);
        }

        survivors.Clear();

        Gen++;
    }

    GeneticPathfinder GetFittest()
    {
        float maxFitness = float.MinValue;
        int index = 0;

        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].Fitness > maxFitness)
            {
                index = i;
                maxFitness = population[i].Fitness;
            }
        }

        GeneticPathfinder fittest = population[index];
        population.Remove(fittest);
        return fittest;
    }

    bool HasActiveIndividuals()
    {
       for (int i = 0; i < population.Count; i++)
       {
            if (!population[i].HasFinished)
            {
                return true;
            }
       }

       return false;
    }

    public void StartSimulation()
    {
        if (!hasStarted)
        {
            EventManager.TriggerEvent(EventsNames.startPlayMode);

            GameManager gm = FindObjectOfType<GameManager>();
            gm.SetGameState(true);

            hasStarted = true;
            Gen = 1;
            InitPopulation();
        }
    }

    public void StopSimulation()
    {
        if (hasStarted)
        {
            hasStarted = false;

            EventManager.TriggerEvent(EventsNames.startPlayMode);

            GameManager gm = FindObjectOfType<GameManager>();
            gm.SetGameState(false);

            //We destroy and clean the population
            for (int i = 0; i < population.Count; i++)
            {
                Destroy(population[i].gameObject);
            }

            population.Clear();

            Gen = 0;
        }
    }
}
