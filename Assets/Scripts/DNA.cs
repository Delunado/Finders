using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public List<Vector2> genes = new List<Vector2>();

    /// <summary>
    /// This constructors sets total random genes to a DNA
    /// </summary>
    /// <param name="genomeLength"></param>
    public DNA(int genomeLength = 50)
    {
        for (int i = 0; i < genomeLength; i++)
        {
            genes.Add(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }

    /// <summary>
    /// This constructor sets genes to a DNA based on the parent and/or partner, including random mutations.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="partner"></param>
    /// <param name="mutationRate">How often mutates</param>
    public DNA(DNA parent, DNA partner, float mutationRate = 0.01f)
    {
        int biggestGenesCount = (parent.genes.Count >= partner.genes.Count) ? parent.genes.Count : partner.genes.Count;

        for (int i = 0; i < biggestGenesCount; i++)
        {
            float mutationChance = Random.Range(0.0f, 1.0f);

            if (mutationChance <= mutationRate)
            {
                genes.Add(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
            } else
            {
                int chance = Random.Range(0, 2);

                if (chance == 0)
                {
                    genes.Add(parent.genes[i]);
                } else
                {
                    genes.Add(partner.genes[i]);
                }
            }
        }
    }


}
