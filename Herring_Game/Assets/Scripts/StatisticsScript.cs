using ChartAndGraph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StatisticsData.resetAll();
        updatePieChart();
        updateLineChart();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void updatePieChart()
    {
        GameObject canvas = GameObject.Find("PieCanvas");
        canvas.GetComponent<PieChart>().DataSource.StartBatch();
        canvas.GetComponent<PieChart>().DataSource.SetValue("Trees and Shrubs", StatisticsData.treeShrubKills);
        canvas.GetComponent<PieChart>().DataSource.SetValue("Culverts", StatisticsData.culvertKills);
        canvas.GetComponent<PieChart>().DataSource.SetValue("River", StatisticsData.riverKills);
        canvas.GetComponent<PieChart>().DataSource.EndBatch();
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
