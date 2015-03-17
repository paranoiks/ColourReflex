using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MyTestController : MonoBehaviour {

    [SerializeField]
    Button ButtonShare;

    // Use this for initialization
    void Start()
    {
        ButtonShare.onClick.AddListener(() => OnButtonSharePressed());
    }

    private void OnButtonSharePressed()
    {
        FB.Init(OnInitComplete, OnHideUnity);
    }

    private void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);

        FB.Login("email,publish_actions", LoginCallback);
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
        CallFBFeed();
    }

    private void CallFBFeed()
    {
        Dictionary<string, string[]> feedProperties = null;
        FB.Feed(
            toId: "",
            link: "",
            linkName: "",
            linkCaption: "",
            linkDescription: "",
            picture: "",
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
    void Update()
    {

    }
}
