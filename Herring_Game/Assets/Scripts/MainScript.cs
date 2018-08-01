﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {

    int years = 0;
    public int numChanges;
    public int herringAlive;
    int NewHerring;

    public int herringMultiplier;

    // GAME DESIGN
    /*
     * Year 0: Herring spawn and run down river
     * Year 1: First opportunity to make changes
     * Year 2: Second oppotunity
     * Year 3: Herring run + changes
     * Etc...
     */
	// Use this for initialization
	void Start () {

        GameObject.Find("Canvas").transform.Find("New_Herring").GetComponent<Text>().enabled = false;

        NewHerring = 0;

        herringAlive = 30000;
        disableRestorationOptions();
        disableNextYear();

        GameObject.Find("Time_Text").GetComponent<Text>().text = "Years Elapsed: " + years;
        GameObject.Find("Changes_Text").GetComponent<Text>().text = "Changes Remaining: " + numChanges;
        GameObject.Find("Herring_Text").GetComponent<Text>().text = "Herring Alive: " + herringAlive;
    }

	// Update is called once per frame
	void Update () {
        if (herringAlive >= 300000){
            SceneManager.LoadScene("End_ScreenW");
        }
        if (herringAlive <= 0)
        {
            SceneManager.LoadScene("End_ScreenL");
        }
	}

    IEnumerator displayNH(float t)
    {
        GameObject.Find("Canvas").transform.Find("New_Herring").GetComponent<Text>().enabled = true;
        GameObject.Find("Canvas").transform.Find("New_Herring").GetComponent<Text>().text = NewHerring + " herring were added to the population through spawning.";
        yield return new WaitForSeconds(t);
        GameObject.Find("Canvas").transform.Find("New_Herring").GetComponent<Text>().enabled = false;
    }

    public void setHerringCount(int num)
    {
        herringAlive = num;
    }

    public void updateHerringCount()
    {
        NewHerring = Curved_Random((int)(herringAlive * 0.3), 45);

        StartCoroutine(displayNH(8));
       // yield return new WaitForSecondsRealtime(3);

        herringAlive += NewHerring;
        GameObject.Find("Herring_Text").GetComponent<Text>().text = "Herring Alive: " + herringAlive;
    }

    public int Curved_Random(int mean, int scale){ //integer

        int num = Random.Range(0, 100);
        int rand = 0;

        if (num <= 68){
            rand = mean + (int)(Random.Range(-1 * scale, scale));
        }
        else if (num > 68 && num <= 95){
            int x = (int)(Random.Range(-1 * scale, scale));
            if (x < 0){
                rand = -1 * scale + mean + x;
            }
            else if (x >= 0){
                rand = mean + scale + x;
            }
        }
        else if (num > 95 && num <= 100){
            int x = (int)(Random.Range(-1 * scale, scale));
            if (x < 0)
            {
                rand = -2 * scale + mean + x;
            }
            else if (x >= 0)
            {
                rand = 2*scale + mean + x;
            }
        }
        return rand;

    }

    public float Curved_Random(float mean, float scale) //float
    {

        int num = Random.Range(0, 100);
        float rand = 0f;

        if (num <= 68)
        {
            rand = mean + Random.Range(-1 * scale, scale);
        }
        else if (num > 68 && num <= 95)
        {
            float x = Random.Range(-1 * scale, scale);
            if (x < 0)
            {
                rand = -1 * scale + mean + x;
            }
            else if (x >= 0)
            {
                rand = mean + scale + x;
            }
        }
        else if (num > 95 && num <= 100)
        {
            float x = Random.Range(-1 * scale, scale);
            if (x < 0)
            {
                rand = -2 * scale + mean + x;
            }
            else if (x >= 0)
            {
                rand = 2 * scale + mean + x;
            }
        }
        return rand;

    }


    public void incrementYear()
    {

        years++;
        GameObject.Find("Time_Text").GetComponent<Text>().text = "Years Elapsed: " + years;

        List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

        for (int i = 0; i < sections.Count; i++)
        {
            sections[i].GetComponent<TreeShrubGeneratorScript>().incrementTreeAges();
        }

        if (years % 3 == 0)
        {
            enableSpawn();
            //disableNextYear();
        }
        else
        {
            disableSpawn();
            enableNextYear();
        }

        enableRestorationOptions();
    }

    public void decrementNumChanges()
    {
        numChanges--;
        GameObject.Find("Changes_Text").GetComponent<Text>().text = "Changes Remaining: " + numChanges;
    }

    public int getChangesLeft()
    {
        return numChanges;
    }

    public void disableRestorationOptions()
    {
        GameObject.Find("Restoration_Options").GetComponent<Dropdown>().enabled = false;
        GameObject.Find("Restoration_Options").GetComponent<Image>().enabled = false;
        GameObject.Find("Restoration_Label").GetComponent<Text>().enabled = false;
    }

    public void disableSpawn()
    {
        GameObject.Find("Spawn").GetComponent<Button>().enabled = false;
        GameObject.Find("Spawn").GetComponent<Image>().enabled = false;
        GameObject.Find("Spawn_Text").GetComponent<Text>().enabled = false;
    }

    public void disableNextYear()
    {
        GameObject.Find("Next_Year").GetComponent<Button>().enabled = false;
        GameObject.Find("Next_Year").GetComponent<Image>().enabled = false;
        GameObject.Find("Next_Text").GetComponent<Text>().enabled = false;
    }

    public void enableRestorationOptions()
    {
        GameObject.Find("Restoration_Options").GetComponent<Dropdown>().enabled = true;
        GameObject.Find("Restoration_Options").GetComponent<Image>().enabled = true;
        GameObject.Find("Restoration_Label").GetComponent<Text>().enabled = true;
    }

    public void enableSpawn()
    {
        GameObject.Find("Spawn").GetComponent<Button>().enabled = true;
        GameObject.Find("Spawn").GetComponent<Image>().enabled = true;
        GameObject.Find("Spawn_Text").GetComponent<Text>().enabled = true;
    }

    public void enableNextYear()
    {
        GameObject.Find("Next_Year").GetComponent<Button>().enabled = true;
        GameObject.Find("Next_Year").GetComponent<Image>().enabled = true;
        GameObject.Find("Next_Text").GetComponent<Text>().enabled = true;
    }
}
