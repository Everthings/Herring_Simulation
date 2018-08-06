using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        GetComponent<LineCreator>().drawNewLines(graphOutline);

        GetComponent<LineCreator>().drawNewLines(dataToPixels(testData()));
    }

    Vector2[] dataToPixels(Vector2[] data)
    {
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
        Vector2[] test = new Vector2[5];

        test[0] = new Vector2(0.1f, 0.1f);
        test[1] = new Vector2(0.2f, 0.2f);
        test[2] = new Vector2(0.3f, 0.3f);
        test[3] = new Vector2(0.4f, 0.4f);
        test[4] = new Vector2(0.5f, 0.5f);

        return test;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
