using ChartAndGraph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        updateChart();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void updateChart()
    {
        GetComponent<PieChart>().DataSource.SetValue("Trees", StatisticsData.treeKills);
        GetComponent<PieChart>().DataSource.SetValue("Shrubs", StatisticsData.shrubKills);
        GetComponent<PieChart>().DataSource.SetValue("Culverts", StatisticsData.culvertKills);
        GetComponent<PieChart>().DataSource.SetValue("River", StatisticsData.riverKills);
    }
}
