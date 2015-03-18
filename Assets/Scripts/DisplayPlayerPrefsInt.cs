using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Attach this to a text field and it will display the Int in the PlayerPrefs
/// corresponding to the given key
/// </summary>
public class DisplayPlayerPrefsInt : MonoBehaviour {

    [SerializeField]
    private string Key = "";

	// Use this for initialization
	void Start () {
        this.GetComponent<Text>().text = PlayerPrefs.GetInt(Key).ToString();
        Debug.Log("PlayerPrefsInt");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
