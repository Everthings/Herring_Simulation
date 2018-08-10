using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatisticsData{

    private static int treeShrubKilled = 0;
    private static int culvertKilled = 0;
    private static int riverKilled = 0;

    private static int[] herringByYear = new int[] { 30000};

    public static void resetKilled()
    {
        treeShrubKilled = 0;
        culvertKilled = 0;
        riverKilled = 0;
    }

    public static void resetAll()
    {
        treeShrubKilled = 0;
        culvertKilled = 0;
        riverKilled = 0;

        herringByYear = new int[] { 30000 };
    }

    public static int treeShrubKills
    {
        get
        {
            return treeShrubKilled;
        }
        set
        {
            treeShrubKilled = value;
        }
    }

    public static int culvertKills
    {
        get
        {
            return culvertKilled;
        }
        set
        {
            culvertKilled = value;
        }
    }

    public static int riverKills
    {
        get
        {
            return riverKilled;
        }
        set
        {
            riverKilled = value;
        }
    }

    public static int[] herringPopulation
    {
        get
        {
            return herringByYear;
        }
        set
        {
            herringByYear = value;
        }
    }

}
