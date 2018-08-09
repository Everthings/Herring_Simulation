using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SurvivalData {

    //not restored
    private static float treeSurvivalRate = 0.984f;
    private static float shrubSurvivalRate = 0.983f;
    private static float culvertSurvivalRate = 0.982f;
    private static float riverSurvivalRate = 0.984f;

    //restored
    private static float[] treeAddedSurvivalRate = new float[] {0.983f, 0.984f, 0.988f, 0.992f, 0.994f, 0.995f, 0.996f, 0.997f, 0.998f, 0.999f};
    private static float shrubAddedSurvivalRate = 0.997f;
    private static float culvertRemovedSurvivalRate = 0.9989f;
    private static float riverWindingSurvivalRate = 0.9975f;

    public static float UnrestoredSurvivalTrees
    {
        get
        {
            return treeSurvivalRate;
        }
        set
        {
            treeSurvivalRate = value;
        }
    }

    public static float UnrestoredSurvivalShrubs
    {
        get
        {
            return shrubSurvivalRate;
        }
        set
        {
            shrubSurvivalRate = value;
        }
    }

    public static float UnrestoredSurvivalCulverts
    {
        get
        {
            return culvertSurvivalRate;
        }
        set
        {
            culvertSurvivalRate = value;
        }
    }

    public static float UnrestoredSurvivalRiver
    {
        get
        {
            return riverSurvivalRate;
        }
        set
        {
            riverSurvivalRate = value;
        }
    }

    public static float[] RestoredSurvivalTrees
    {
        get
        {
            return treeAddedSurvivalRate;
        }
        set
        {
            treeAddedSurvivalRate = value;
        }
    }

    public static float RestoredSurvivalShrubs
    {
        get
        {
            return shrubAddedSurvivalRate;
        }
        set
        {
            shrubAddedSurvivalRate = value;
        }
    }

    public static float RestoredSurvivalCulverts
    {
        get
        {
            return culvertRemovedSurvivalRate;
        }
        set
        {
            culvertRemovedSurvivalRate = value;
        }
    }

    public static float RestoredSurvivalRiver
    {
        get
        {
            return riverWindingSurvivalRate;
        }
        set
        {
            riverWindingSurvivalRate = value;
        }
    }
}
