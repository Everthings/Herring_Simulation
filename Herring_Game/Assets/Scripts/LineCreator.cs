using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// © 2018 TheFlyingKeyboard and released under MIT License 
// theflyingkeyboard.net 
public class LineCreator : MonoBehaviour
{
    [SerializeField] private GameObject line;
    List<LineObject> lines;

    private void Start()
    {
        lines = new List<LineObject>();
    }

    public void drawNewLines(Vector2[] points, string name, Color startColor, Color endColor)
    {
        GameObject temp = Instantiate(line, new Vector3(), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        temp.transform.SetParent(transform);
        temp.GetComponent<LineDrawer>().drawLines(points);
        temp.GetComponent<LineRenderer>().startColor = startColor;
        temp.GetComponent<LineRenderer>().endColor = endColor;
        lines.Add(new LineObject(temp, name));
    }

    public void removeLine(string name)
    {
        for(int i = lines.Count - 1; i >= 0; i--)
        {
            if (lines[i].getName().Equals(name))
            {
                Destroy(lines[i].getLine());
                lines.Remove(lines[i]);
                break;
            }
        }
    }
}

class LineObject
{
    GameObject line;
    string name;

    public LineObject(GameObject obj, string name)
    {
        this.line = obj;
        this.name = name;
    }

    public GameObject getLine()
    {
        return line;
    }

    public string getName()
    {
        return name;
    }
}