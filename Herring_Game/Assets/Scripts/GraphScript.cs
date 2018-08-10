using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphScript : MonoBehaviour {

    Rect windowRect;
    public string gName;

    public int minX;
    public int maxX;
    public float minY;
    public float maxY;

    public int numIncrementsX;
    public int numIncrementsY;

    float graphStartX;
    float graphStartY;

    float graphWidth;
    float graphHeight;

    List<GraphPoint> dataPoints;

    public Button b;

    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, DoMyWindow, gName);
    }

    // Make the contents of the window
    void DoMyWindow(int windowID)
    {
        float x_increment = (float)(maxX -  minX) / (numIncrementsX - 1);
        float y_increment = (float)(maxY - minY) / (numIncrementsY - 1);

        float x_pixel_increment = (graphWidth) / (numIncrementsX - 1);
        float y_pixel_increment = (graphHeight) / (numIncrementsY - 1);

        int text_length = 40;

        if (minX != minY)
        {
            for (int i = 0; i < numIncrementsX; i++)
            {
                GUI.TextArea(new Rect(windowRect.x + graphStartX + x_pixel_increment * i - text_length / 2, windowRect.y + graphStartY, text_length, 20), (minX + x_increment * i) + "");
            }

            for (int i = 0; i < numIncrementsY; i++)
            {
                GUI.TextArea(new Rect(windowRect.x + graphStartX - text_length, windowRect.y + graphStartY - y_pixel_increment * i - 10, text_length, 20), (minY + y_increment * i) + "");
            }
        }
        else
        {
            for(int i = 1; i < numIncrementsX; i++)
            {
                GUI.TextArea(new Rect(windowRect.x + graphStartX + x_pixel_increment * i - text_length / 2, windowRect.y + graphStartY, text_length, 20), (minX + x_increment * i) + "");
            }

            for (int i = 1; i < numIncrementsY; i++)
            {
                GUI.TextArea(new Rect(windowRect.x + graphStartX - text_length, windowRect.y + graphStartY - y_pixel_increment * i - 10, text_length, 20), (minY + y_increment * i) + "");
            }

            GUI.TextArea(new Rect(windowRect.x + graphStartX - text_length, windowRect.y + graphStartY, text_length, 20), minX + "");
        }
    }
    
    // Use this for initialization
    void Start () {

        windowRect = new Rect(0, 0, Screen.width, Screen.height);

        graphStartX = windowRect.x + (windowRect.width / 9);
        graphStartY = windowRect.y + (windowRect.height * 9 / 11);

        graphWidth = (float)windowRect.width * 7 / 9;
        graphHeight = (float)(windowRect.height * 9 / 11) - (windowRect.height / 10);

        Vector2[] graphOutline = new Vector2[3];
        graphOutline[0] = new Vector2(windowRect.x + (windowRect.width / 9), windowRect.y + (windowRect.height / 10));
        graphOutline[1] = new Vector2(windowRect.x + (windowRect.width / 9), windowRect.y + (windowRect.height * 9 / 11));
        graphOutline[2] = new Vector2(windowRect.x + (windowRect.width * 8 / 9), windowRect.y + (windowRect.height * 9 / 11));

        GetComponent<LineCreator>().drawNewLines(graphOutline, "graph outline", Color.white, Color.white);

        initGraphData();

        GetComponent<LineCreator>().drawNewLines(graphDataToPixels(getGraphPositionData()), "data", Color.red, Color.green);
    }

    public void redrawGraph()
    {
        GetComponent<LineCreator>().removeLine("data");
        GetComponent<LineCreator>().drawNewLines(graphDataToPixels(getGraphPositionData()), "data", Color.red, Color.green);

        setData(dataPoints.ToArray());
    }

    void initGraphData()
    {
        dataPoints = new List<GraphPoint>();

        Vector2[] data = dataToPixels(getData());

        for(int i = 0; i < data.Length; i++)
        {
            Button newB = Instantiate(b, new Vector3(data[i].x, Screen.height - data[i].y, 0), Quaternion.identity);
            newB.transform.SetParent(transform);
            dataPoints.Add(new GraphPoint(newB));
        }
    }

    public Vector2 pixelsToDataPoint(Vector2 pos)
    {
        // returns the value of a graph point base on it's position
        pos.y = Screen.height - pos.y;
        float newX = (pos.x - graphStartX) / graphWidth * (float)(maxX - minX) + minX;
        float newY = (graphStartY - pos.y) / graphHeight * (float)(maxY - minY) + minY;

        Debug.Log(newY);

        return new Vector2(cutDecimals(newX, 5), cutDecimals(newY, 5));
    }

    public float cutDecimals(float value, int numDecimals)
    {
        return ((int)(value * Mathf.Pow(10, numDecimals))) / Mathf.Pow(10, numDecimals);
    }

    Vector2[] getGraphPositionData()
    {
        Vector2[] data = new Vector2[dataPoints.Count];

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = dataPoints[i].getButton().GetComponent<RectTransform>().position;
        }

        return data;
    }

    Vector2[] graphDataToPixels(Vector2[] data)
    {
        // graph button points to pixels (buttons are cenetred weird)

        Vector2[] fixedData = data;

        for (int i = 0; i < fixedData.Length; i++)
        {
            fixedData[i].y = Screen.height - fixedData[i].y;
        }

        return fixedData;
    }

    Vector2[] dataToPixels(Vector2[] data)
    {
        // lines data to pixels (lines are centered weird as well)

        Vector2[] fixedData = data;

        for(int i = 0; i < fixedData.Length; i++)
        {
            fixedData[i].x = graphStartX + graphWidth * ((fixedData[i].x - minX) / (float)(maxX - minX));
            fixedData[i].y = graphStartY - graphHeight * ((fixedData[i].y - minY) / (float)(maxY - minY));
        }

        return fixedData;
    }

    Vector2[] getData()
    {
        Vector2[] test = new Vector2[10];
        float[] rates = SurvivalData.RestoredSurvivalTrees;

        test[0] = new Vector2(1f, rates[0]);
        test[1] = new Vector2(2f, rates[1]);
        test[2] = new Vector2(3f, rates[2]);
        test[3] = new Vector2(4f, rates[3]);
        test[4] = new Vector2(5f, rates[4]);
        test[5] = new Vector2(6f, rates[5]);
        test[6] = new Vector2(7f, rates[6]);
        test[7] = new Vector2(8f, rates[7]);
        test[8] = new Vector2(9f, rates[8]);
        test[9] = new Vector2(10f, rates[9]);

        return test;
    }

    void setData(GraphPoint[] data)
    {
        float[] rates = new float[10];

        for(int i = 0; i < rates.Length; i++)
        {
            rates[i] = pixelsToDataPoint(data[i].getButton().GetComponent<RectTransform>().position).y;
        }

        SurvivalData.RestoredSurvivalTrees = rates;
    }

    // Update is called once per frame
    void Update () {
		
	}
}

class GraphPoint
{
    Button b;

    public GraphPoint(Button b)
    {
        this.b = b;
    }
    
    public Button getButton()
    {
        return b;
    }
}