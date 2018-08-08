using ChartAndGraph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameStatistics : MonoBehaviour {

	// Use this for initialization
	void Start () {
        updateLineChart();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateLineChart()
    {
        GraphChart graph = GameObject.Find("GraphChart").GetComponent<GraphChart>();
        int[] herringPopulation = StatisticsData.herringPopulation;

        graph.DataSource.StartBatch();
        graph.DataSource.ClearAndMakeBezierCurve("Line");
        graph.DataSource.SetCurveInitialPoint("Line", 0, herringPopulation[0]);

        for (int i = 1; i < herringPopulation.Length; i++)
        {
            graph.DataSource.AddLinearCurveToCategory("Line", new DoubleVector2(i, herringPopulation[i]));
        }

        graph.DataSource.MakeCurveCategorySmooth("Line");
        graph.DataSource.EndBatch();
    }
}
