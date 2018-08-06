using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// © 2018 TheFlyingKeyboard and released under MIT License 
// theflyingkeyboard.net 
public class LineCreator : MonoBehaviour
{
    [SerializeField] private GameObject line;

    public void drawNewLines(Vector2[] points)
    {
        GameObject temp = Instantiate(line, new Vector3(), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        temp.transform.SetParent(transform);
        temp.GetComponent<LineDrawer>().drawLines(points);
    }
}