using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultyData{

    // easy = 0, climate change = 1, fishing = 2
    static int diff = 0;

    public static int difficulty
    {
        get
        {
            return diff;
        }
        set
        {
            diff = value;
        }
    }
}
