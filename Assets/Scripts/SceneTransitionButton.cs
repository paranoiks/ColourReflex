using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneTransitionButton : MonoBehaviour {

    [SerializeField]
    private string SceneName = "";

	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(() => OnButtonClick());
	}

    private void OnButtonClick()
    {
        Application.LoadLevel(SceneName);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
