using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayPlayerPrefsString : MonoBehaviour {

    [SerializeField]
    private string Key;

	// Use this for initialization
	void Start () {
        this.GetComponent<Text>().text = PlayerPrefs.GetString(Key);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
