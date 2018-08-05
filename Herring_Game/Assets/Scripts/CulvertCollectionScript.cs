using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CulvertCollectionScript : MonoBehaviour {

    List<GameObject> culverts = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        culverts.Add(transform.Find("Culvert1").gameObject);
        culverts.Add(transform.Find("Culvert2").gameObject);
        culverts.Add(transform.Find("Culvert3").gameObject);
        culverts.Add(transform.Find("Culvert4").gameObject);
        culverts.Add(transform.Find("Culvert6").gameObject);
        culverts.Add(transform.Find("Culvert7").gameObject);
        culverts.Add(transform.Find("Culvert8").gameObject);
        culverts.Add(transform.Find("Culvert9").gameObject);
        culverts.Add(transform.Find("Culvert10").gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> getCulverts()
    {
        return culverts;
    }
}
