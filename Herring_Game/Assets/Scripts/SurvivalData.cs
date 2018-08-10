using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SurvivalData {

    //not restored
    private static float treeShrubSurvivalRate = 0.97f;
    private static float culvertSurvivalRate = 0.97f;
    private static float riverSurvivalRate = 0.97f;

    //restored
    private static float[] treeAddedSurvivalRateIncrease = new float[] {0.015f, 0.016f, 0.017f, 0.018f, 0.019f, 0.02f, 0.022f, 0.024f, 0.026f, 0.029f};
    private static float shrubAddedSurvivalRateIncrease = 0.025f;
    private static float culvertRemovedSurvivalRate = 0.995f;
    private static float riverWindingSurvivalRate = 0.99f;

    public static float UnrestoredSurvivalTreesShrubs
    {
        get
        {
            return treeShrubSurvivalRate;
        }
        set
        {
            treeShrubSurvivalRate = value;
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
            return treeAddedSurvivalRateIncrease;
        }
        set
        {
            treeAddedSurvivalRateIncrease = value;
        }
    }

    public static float RestoredSurvivalShrubs
    {
        get
        {
            return shrubAddedSurvivalRateIncrease;
        }
        set
        {
            shrubAddedSurvivalRateIncrease = value;
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
