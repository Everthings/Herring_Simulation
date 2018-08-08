using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SurvivalData {

    //not restored
    private static float treeSurvivalRate = 0.97f;
    private static float shrubSurvivalRate = 0.97f;
    private static float culvertSurvivalRate = 0.97f;
    private static float riverSurvivalRate = 0.97f;

    //restored
    private static float[] treeAddedSurvivalRate = new float[] {0.98f, 0.982f, 0.985f, 0.987f, 0.989f, 0.991f, 0.992f, 0.994f, 0.995f, 0.999f};
    private static float shrubAddedSurvivalRate = 0.995f;
    private static float culvertRemovedSurvivalRate = 0.995f;
    private static float riverWindingSurvivalRate = 0.995f;

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
