using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {

    int years = 0;
    public int numChanges;
    public int herringAlive;
    int herringAdults;
    int herringChildren;
    int herringImpactedByDifficulty;

    public int herringMultiplier = 0;

    public float climateMultiplier = 0.1f;
    public float fishingMultiplier = 0.2f;

    public int changesPerYear;
    int changesThisYear;

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

        GameObject.Find("New_Herring").GetComponent<Text>().enabled = false;

        herringChildren = 0;

        herringAlive = 30000;
        //disableRestorationOptions();
        disableSkipYear();
        //disableSkipAll();

        GameObject.Find("Time_Text").GetComponent<Text>().text = "Years Elapsed: " + years;
        GameObject.Find("Changes_Text").GetComponent<Text>().text = "Changes Remaining: " + numChanges;
        GameObject.Find("Total").GetComponent<Text>().text = "Total Herring: " + herringAlive;

        changesThisYear = changesPerYear;
        GameObject.Find("Changes_This_Year").GetComponent<Text>().text = "Changes Remaining This Year: " + changesThisYear;
    }

	// Update is called once per frame
	void Update () {
        
    }

    public int getChangesThisYear()
    {
        return changesThisYear;
    }

    public bool checkForFinish()
    {
        if (herringAlive >= 300000)
        {
            SceneManager.LoadScene("End_ScreenW");
            return true;
        }else if (herringAlive <= 1000)
        {
            SceneManager.LoadScene("End_ScreenL");
            return true;
        }

        return false;
    }

    IEnumerator displayInfo(float t)
    {
        GameObject UI = GameObject.Find("GameUI");

        // Herring info
        UI.transform.Find("New_Herring").GetComponent<Text>().enabled = true;
        UI.transform.Find("Herring_Text").GetComponent<Text>().enabled = true;
        UI.transform.Find("New_Herring").GetComponent<Text>().text = herringChildren + " herring were added to the population through spawning.";
        UI.transform.Find("Herring_Text").GetComponent<Text>().text = "Adult Herring: " + herringAdults;
        yield return new WaitForSeconds(t);
        UI.transform.Find("New_Herring").GetComponent<Text>().enabled = false;
        UI.transform.Find("Herring_Text").GetComponent<Text>().enabled = false;

        // Difficulty (Climate &| Fishing) info
        if(DifficultyData.difficulty == 1)
        {
            UI.transform.Find("Difficulty_Text").GetComponent<Text>().enabled = true;
            UI.transform.Find("Difficulty_Text").GetComponent<Text>().text = herringImpactedByDifficulty + " herring died from climate change.";
            yield return new WaitForSeconds(t);
            UI.transform.Find("Difficulty_Text").GetComponent<Text>().enabled = false;
        }else if (DifficultyData.difficulty == 2)
        {
            UI.transform.Find("Difficulty_Text").GetComponent<Text>().enabled = true;
            UI.transform.Find("Difficulty_Text").GetComponent<Text>().text = herringImpactedByDifficulty + " herring died from climate change and fishing.";
            yield return new WaitForSeconds(t);
            UI.transform.Find("Difficulty_Text").GetComponent<Text>().enabled = false;
        }
    }

    public void callSkipAllYears()
    {
        StartCoroutine(skipAllYears());
       
    }

    IEnumerator skipAllYears()
    {
        GameObject.Find("Fade").GetComponent<FadeScript>().fadeOut();
        yield return new WaitForSeconds(0.25f);

        while (!checkForFinish())
        {
            herringAlive = simulateInternally();
            updateHerringCount();
            List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

            for (int i = 0; i < sections.Count; i++)
            {
                sections[i].GetComponent<TreeShrubGeneratorScript>().incrementTreeAges();
            }
            updateYearStatistics();
        }
    }

    public void updateHerringCount()
    {
        GameObject UI = GameObject.Find("GameUI");

        herringChildren = Curved_Random((int)(herringAlive * 0.5), 45);
        herringAdults = herringAlive;

        herringAlive += herringChildren;

        if(DifficultyData.difficulty == 1)
        {
            herringImpactedByDifficulty = (int)(herringAlive * climateMultiplier);
        }
        else if(DifficultyData.difficulty == 2)
        {
            herringImpactedByDifficulty = (int)(herringAlive * fishingMultiplier);
        }

        herringAlive -= herringImpactedByDifficulty;

        UI.transform.Find("Total").GetComponent<Text>().text = "Total Herring: " + herringAlive;
        StartCoroutine(displayInfo(8));
    }

    public int decreaseHerring(int sub)
    {
        int remove = (int)Curved_Random(sub, 16f);
        herringAlive -= remove;
        GameObject.Find("GameUI").transform.Find("Total").GetComponent<Text>().text = "Total Herring: " + herringAlive;
        //GameObject.Find("GameUI").transform.Find("Total").GetComponent<Text>().text = "Total herring: " + herringAlive;

        return remove;
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

    public IEnumerator incrementYear(bool calculateHerringPopulation)
    {

        StopAllCoroutines();

        GameObject.Find("Fade").GetComponent<FadeScript>().fadeOut();
        yield return new WaitForSeconds(0.25f);

        years++;
        GameObject.Find("Time_Text").GetComponent<Text>().text = "Years Elapsed: " + years;

        if (calculateHerringPopulation)
        {
            int survived = simulateInternally();
            GameObject.Find("Canvas").GetComponent<StatisticsScript>().updatePieChart();
            herringAlive = survived;
            updateHerringCount();
        }

        updateYearStatistics();

        GameObject.Find("Total").GetComponent<Text>().text = "Total Herring: " + herringAlive;

        List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

        for (int i = 0; i < sections.Count; i++)
        {
            sections[i].GetComponent<TreeShrubGeneratorScript>().incrementTreeAges();
        }

        enableSpawn();
        enableSkipYear();
        enableSkipAll();
        enableRestorationOptions();

        checkForFinish();

        changesThisYear = changesPerYear;
        GameObject.Find("Changes_This_Year").GetComponent<Text>().text = "Changes Remaining This Year: " + changesThisYear;

        GameObject.Find("Fade").GetComponent<FadeScript>().fadeIn();

    }

    void updateYearStatistics()
    {
        List<int> herringPopulation = new List<int>(StatisticsData.herringPopulation);
        herringPopulation.Add(GameObject.Find("Sections").GetComponent<MainScript>().herringAlive);
        StatisticsData.herringPopulation = herringPopulation.ToArray();
        GameObject.Find("Canvas").GetComponent<StatisticsScript>().updateLineChart();
    }

    public int simulateInternally()
    {
        //returns survived herring

        List<GameObject> s = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();
        List<GameObject> c = GameObject.Find("Culverts").GetComponent<CulvertCollectionScript>().getCulverts();

        int survived = herringAlive;
        StatisticsData.resetKilled();

        for (int i = 0; i < herringAlive / herringMultiplier; i++)
        {

            for (int j = 0; j < 10; j++)
            {
                if (j < c.Count)
                {
                    if (c[j].transform.Find("Button").GetComponent<CulvertRemovalScript>().isRemoved())
                    {
                        if (Random.value > SurvivalData.RestoredSurvivalCulverts)
                        {
                            int killed = Curved_Random(herringMultiplier, 16);
                            StatisticsData.culvertKills += killed;
                            survived -= killed;
                            break;
                        }
                    }
                    else
                    {
                        if (Random.value > SurvivalData.UnrestoredSurvivalCulverts)
                        {
                            int killed = Curved_Random(herringMultiplier, 16);
                            StatisticsData.culvertKills += killed;
                            survived -= killed;
                            break;
                        }
                    }
                }

                if (Random.value > s[j].GetComponent<TreeShrubGeneratorScript>().getTreeSurvivalRate())
                {
                    int killed = Curved_Random(herringMultiplier, 16);
                    StatisticsData.treeKills += killed;
                    survived -= killed;
                    break;
                }

                if (Random.value > s[j].GetComponent<TreeShrubGeneratorScript>().getShrubSurvivalRate())
                {
                    int killed = Curved_Random(herringMultiplier, 16);
                    StatisticsData.shrubKills += killed;
                    survived -= killed;
                    break;
                }

                if (Random.value > s[j].GetComponent<TreeShrubGeneratorScript>().getRiverSurvivalRate())
                {
                    int killed = Curved_Random(herringMultiplier, 16);
                    StatisticsData.riverKills += killed;
                    survived -= killed;
                    break;
                }
            }
        }

        return survived;
    }

    public void decrementNumChanges()
    {
        numChanges--;
        GameObject.Find("Changes_Text").GetComponent<Text>().text = "Changes Remaining: " + numChanges;
    }

    public void decrementChangesThisYear()
    {
        changesThisYear--;
        GameObject.Find("Changes_This_Year").GetComponent<Text>().text = "Changes Remaining This Year: " + changesThisYear;
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

    public void disableSkipYear()
    {
        GameObject.Find("Skip_Year").GetComponent<Button>().enabled = false;
        GameObject.Find("Skip_Year").GetComponent<Image>().enabled = false;
        GameObject.Find("Skip_Year").transform.GetChild(0).GetComponent<Text>().enabled = false;
    }

    public void disableSkipAll()
    {
        GameObject.Find("SkipAll_Button").GetComponent<Button>().enabled = false;
        GameObject.Find("SkipAll_Button").GetComponent<Image>().enabled = false;
        GameObject.Find("SkipAll_Button").transform.GetChild(0).GetComponent<Text>().enabled = false;
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

    public void enableSkipYear()
    {
        GameObject.Find("Skip_Year").GetComponent<Button>().enabled = true;
        GameObject.Find("Skip_Year").GetComponent<Image>().enabled = true;
        GameObject.Find("Skip_Year").transform.GetChild(0).GetComponent<Text>().enabled = true;
    }

    public void enableSkipAll()
    {
        GameObject.Find("SkipAll_Button").GetComponent<Button>().enabled = true;
        GameObject.Find("SkipAll_Button").GetComponent<Image>().enabled = true;
        GameObject.Find("SkipAll_Button").transform.GetChild(0).GetComponent<Text>().enabled = true;
    }
}
