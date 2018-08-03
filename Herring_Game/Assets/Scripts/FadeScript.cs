using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour {

    public Animator anim;
    string nextScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void fadeOutScene(string nextScene)
    {
        anim.SetTrigger("FadeOut");
        this.nextScene = nextScene;
    }

    public void fadeOut()
    {
        anim.SetTrigger("FadeOutTemp");
    }

    public void fadeIn()
    {
        anim.SetTrigger("FadeInTemp");
    }

    public void changeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
