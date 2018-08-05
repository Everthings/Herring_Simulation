using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCollectionScript: MonoBehaviour
{
    List<GameObject> sections = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        sections.Add(transform.Find("Section1").gameObject);
        sections.Add(transform.Find("Section2").gameObject);
        sections.Add(transform.Find("Section3").gameObject);
        sections.Add(transform.Find("Section4").gameObject);
        sections.Add(transform.Find("Section5").gameObject);
        sections.Add(transform.Find("Section6").gameObject);
        sections.Add(transform.Find("Section7").gameObject);
        sections.Add(transform.Find("Section8").gameObject);
        sections.Add(transform.Find("Section9").gameObject);
        sections.Add(transform.Find("Section10").gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> getSections()
    {
        return sections;
    }
}
