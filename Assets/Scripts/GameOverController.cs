using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOverController : MonoBehaviour {

    [SerializeField]
    Button ButtonShare;

    [SerializeField]
    Button ButtonRestart;

    const string PostPictureString = "https://www.petfinder.com/wp-content/uploads/2012/11/99233806-bringing-home-new-cat-632x475.jpg";

	// Use this for initialization
	void Start () {
        ButtonShare.onClick.AddListener(() => OnButtonSharePressed());
        ButtonRestart.onClick.AddListener(() => OnButtonRestartPressed());
	}

    private void OnButtonSharePressed()
    {
        if (FB.IsInitialized)
        {
            TryLogIn();
        }
        else
        {
            FB.Init(OnInitComplete, OnHideUnity);
        }
    }

    private void OnButtonRestartPressed()
    {
        string currentMode = PlayerPrefs.GetString(GlobalStrings.CURRENT_MODE);
        string levelName = "";
        if(currentMode == GlobalStrings.CLASSIC_MODE)
        {
            levelName = "ScenePlayClassicMode";
        }
        else if(currentMode == GlobalStrings.SWAP_MODE)
        {
            levelName = "ScenePlaySwapMode";
        }
        else if(currentMode == GlobalStrings.WACKY_MODE)
        {
            levelName = "ScenePlayWackyMode";
        }

        Application.LoadLevel(levelName);
    }

    private void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
        TryLogIn();
    }

    private void TryLogIn()
    {
        if (FB.IsLoggedIn)
        {
            PostScoreToFB();
        }
        else
        {
            FB.Login("email,publish_actions", LoginCallback);
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Is game showing? " + isGameShown);
    }

    void LoginCallback(FBResult result)
    {
        if (FB.IsLoggedIn)
        {
            OnLoggedIn();
        }
    }

    void OnLoggedIn()
    {
        PostScoreToFB();
    }

    private void PostScoreToFB()
    {
        Dictionary<string, string[]> feedProperties = new Dictionary<string, string[]>();
        feedProperties.Add("key2", new[] { "valueString2", "http://www.facebook.com" });

        int score = PlayerPrefs.GetInt(GlobalStrings.CURRENT_SCORE_STRING);

        FB.Feed(
            toId: "",
            link: "http://www.google.com/",
            linkName: "Colour Reflex",
            linkCaption: "",
            linkDescription: "I just scored " + score.ToString() + " on Colour Reflex",
            picture: PostPictureString,
            mediaSource: "",
            actionName: "",
            actionLink: "",
            reference: "",
            properties: feedProperties,
            callback: Callback
        );
    }

    protected void Callback(FBResult result)
    {
    }

	// Update is called once per frame
	void Update () {
	
	}
}
