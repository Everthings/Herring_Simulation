using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatisticsData{

    private static int shrubKilled = 0;
    private static int treeKilled = 0;
    private static int culvertKilled = 0;
    private static int riverKilled = 0;

    private static int[] herringByYear = new int[] { 30000};

    public static void resetKilled()
    {
        shrubKilled = 0;
        treeKilled = 0;
        culvertKilled = 0;
        riverKilled = 0;
    }

    public static void resetAll()
    {
        shrubKilled = 0;
        treeKilled = 0;
        culvertKilled = 0;
        riverKilled = 0;
    }

    public static int treeKills
    {
        get
        {
            return treeKilled;
        }
        set
        {
            treeKilled = value;
        }
    }

    public static int shrubKills
    {
        get
        {
            return shrubKilled;
        }
        set
        {
            shrubKilled = value;
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
