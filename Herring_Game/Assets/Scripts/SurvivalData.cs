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
    private static float[] treeAddedSurvivalRate = new float[] {0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 0.95f, 0.97f, 0.975f, 0.99f, 0.995f};
    private static float shrubAddedSurvivalRate = 0.99f;
    private static float culvertRemovedSurvivalRate = 0.99f;
    private static float riverWindingSurvivalRate = 0.99f;

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
