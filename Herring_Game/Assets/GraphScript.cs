using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphScript : MonoBehaviour {

    Rect windowRect;
    public string gName;

    public int minX;
    public int maxX;
    public int minY;
    public int maxY;

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
    }

    void initGraphData()
    {
        dataPoints = new List<GraphPoint>();

        Vector2[] data = dataToPixels(testData());

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
        float newX = (pos.x - graphStartX) / graphWidth * (float)(maxX - minX);
        float newY = (pos.y - graphStartY) / graphHeight * (float)(maxY - minY);

        return new Vector2(cutDecimals(newX, 2), -cutDecimals(newY, 2));
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
            fixedData[i].x = graphStartX + graphWidth * (fixedData[i].x / (float)(maxX - minX));
            fixedData[i].y = graphStartY - graphHeight * (fixedData[i].y / (float)(maxY - minY));
        }

        return fixedData;
    }

    Vector2[] testData()
    {
        Vector2[] test = new Vector2[10];

        test[0] = new Vector2(0.1f, 0.1f);
        test[1] = new Vector2(0.2f, 0.2f);
        test[2] = new Vector2(0.3f, 0.3f);
        test[3] = new Vector2(0.4f, 0.4f);
        test[4] = new Vector2(0.5f, 0.5f);
        test[5] = new Vector2(0.6f, 0.6f);
        test[6] = new Vector2(0.7f, 0.7f);
        test[7] = new Vector2(0.8f, 0.8f);
        test[8] = new Vector2(0.9f, 0.9f);
        test[9] = new Vector2(1f, 1f);

        return test;
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