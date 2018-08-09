// Script from PubNub https://www.pubnub.com/blog/realtime-highscores-leaderboards-in-unity/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using UnityEngine.UI;
using SimpleJSON;

public class leaderBoard : MonoBehaviour {

	public static PubNub pubnub;
	public Text Line1;
	public Text Line2;
	public Text Line3;
	public Text Line4;
	public Text Line5;
	public Text Score1;
	public Text Score2;
	public Text Score3;
	public Text Score4;
	public Text Score5;

	//public Object[] tiles = {}
	// Use this for initialization
	void Start () {
        updateLeaderboard(0);

    }

    public void updateLeaderboard(int i)
    {
        // Use this for initialization
        PNConfiguration pnConfiguration = new PNConfiguration();

        if (i == 0)
        {
            //easy
            pnConfiguration.PublishKey = "pub-c-3f4c82b1-e976-4c64-8d17-6561a89a738b";
            pnConfiguration.SubscribeKey = "sub-c-b8190a96-9b85-11e8-979a-3665db0876c0";
        }
        else if (i == 1)
        {
            //medium
            pnConfiguration.PublishKey = "pub-c-569d862a-e815-4fd8-a8d9-b82b198a0636";
            pnConfiguration.SubscribeKey = "sub-c-81de4cf8-9c15-11e8-944c-22e677923cb5";
        }
        else if (i == 2)
        {
            //hard
            pnConfiguration.PublishKey = "pub-c-4ba8063a-d8a2-45e1-b86c-2fd85bb87e0c";
            pnConfiguration.SubscribeKey = "sub-c-dcdc159a-9c15-11e8-9a7c-62794ce13da1";
        }

        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        pnConfiguration.UUID = Random.Range(0f, 999999f).ToString();

        pubnub = new PubNub(pnConfiguration);
        Debug.Log(pnConfiguration.UUID);


        MyClass myFireObject = new MyClass();
        myFireObject.test = "new user";
        string fireobject = JsonUtility.ToJson(myFireObject);
        pubnub.Fire()
            .Channel("my_channel")
            .Message(fireobject)
            .Async((result, status) => {
                if (status.Error)
                {
                    Debug.Log(status.Error);
                    Debug.Log(status.ErrorData.Info);
                }
                else
                {
                    Debug.Log(string.Format("Fire Timetoken: {0}", result.Timetoken));
                }
            });

        pubnub.SusbcribeCallback += (sender, e) => {
            SusbcribeEventEventArgs mea = e as SusbcribeEventEventArgs;
            if (mea.Status != null)
            {
            }
            if (mea.MessageResult != null)
            {
                Dictionary<string, object> msg = mea.MessageResult.Payload as Dictionary<string, object>;

                string[] strArr = msg["username"] as string[];
                string[] strScores = msg["score"] as string[];

                int usernamevar = 1;
                foreach (string username in strArr)
                {
                    string usernameobject = "Line" + usernamevar;
                    GameObject.Find(usernameobject).GetComponent<Text>().text = usernamevar.ToString() + ". " + username.ToString();
                    usernamevar++;
                    Debug.Log(username);
                }

                int scorevar = 1;
                foreach (string score in strScores)
                {
                    string scoreobject = "Score" + scorevar;
                    GameObject.Find(scoreobject).GetComponent<Text>().text = "Score: " + score.ToString();
                    scorevar++;
                    Debug.Log(score);
                }
            }
            if (mea.PresenceEventResult != null)
            {
                Debug.Log("In Example, SusbcribeCallback in presence" + mea.PresenceEventResult.Channel + mea.PresenceEventResult.Occupancy + mea.PresenceEventResult.Event);
            }
        };
        pubnub.Subscribe()
            .Channels(new List<string>() {
                "my_channel2"
            })
            .WithPresence()
            .Execute();
    }
}
