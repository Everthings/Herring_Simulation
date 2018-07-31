using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    int years = 0;
    public int numChanges;
    int herringAlive;

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
        disableRestorationOptions();

        GameObject.Find("Time_Text").GetComponent<Text>().text = "Years Elapsed: " + years;
        GameObject.Find("Changes_Text").GetComponent<Text>().text = "Changes Remaining: " + numChanges;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void incrementYear()
    {

        years++;
        GameObject.Find("Time_Text").GetComponent<Text>().text = "Years Elapsed: " + years;

        if(years % 3 == 0)
        {
            enableSpawn();
        }
        else
        {
            disableSpawn();
        }

        enableRestorationOptions();
    }

    public void decrementNumChanges()
    {
        numChanges--;
        GameObject.Find("Changes_Text").GetComponent<Text>().text = "Chnages Remaining: " + numChanges;
    }

    public int getChangesLeft()
    {
        return numChanges;
    }

    private void disableRestorationOptions()
    {
        GameObject.Find("Restoration_Options").GetComponent<Dropdown>().enabled = false;
        GameObject.Find("Restoration_Options").GetComponent<Image>().enabled = false;
        GameObject.Find("Restoration_Label").GetComponent<Text>().enabled = false;
    }

    private void disableSpawn()
    {
        GameObject.Find("Spawn").GetComponent<Button>().enabled = false;
        GameObject.Find("Spawn").GetComponent<Image>().enabled = false;
        GameObject.Find("Spawn_Text").GetComponent<Text>().enabled = false;
    }

    private void enableRestorationOptions()
    {
        GameObject.Find("Restoration_Options").GetComponent<Dropdown>().enabled = true;
        GameObject.Find("Restoration_Options").GetComponent<Image>().enabled = true;
        GameObject.Find("Restoration_Label").GetComponent<Text>().enabled = true;
    }

    private void enableSpawn()
    {
        GameObject.Find("Spawn").GetComponent<Button>().enabled = true;
        GameObject.Find("Spawn").GetComponent<Image>().enabled = true;
        GameObject.Find("Spawn_Text").GetComponent<Text>().enabled = true;
    }
}
