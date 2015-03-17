using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainControllerWackyMode : MonoBehaviour {

    private int Score = 0;
    private bool Started = false;
    private float ReactionTime = 3;
    private float ReactionTimeChange = 0.1f;
    private float MinReactionTime = 1;

    private int ClicksUntilNexRotationChange = 6;
    private int ClicksUntilNextDarkening = 7;

    private int CurrentTargetColour = 0; //always start from orange for now
    
    [SerializeField]
    private GameObject SquaresRoot;

    [SerializeField]
    private GameObject[] ColourSquares; //Orange, Purple, Cyan, Green

    [SerializeField]
    private Material[] Materials; //Orange, Purple, Cyan, Green

    [SerializeField]
    private GameObject ColourPicker;

    [SerializeField]
    private GameObject SparkParticlePrefab;

    private float ParticlesLayerZ = -6f;

    private float ColourPickerMinScale = 0.6f;
    private float ColourPickerMaxScale = 1.8f;
    private float ColourPickerCurrentScale = 0.6f;

	// Use this for initialization
	void Start () {
	
	}

    /// <summary>
    /// return the click/tap position
    /// so the game can be played with touch or mouse
    /// </summary>
    /// <returns></returns>
    private Vector3 GetTapPosition()
    {
        Vector3 tapPosition = Vector3.zero;
        if(Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tapPosition = Input.touches[0].position;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            tapPosition = Input.mousePosition;
        }

        return tapPosition;
    }

    /// <summary>
    /// get the click/tap position and check if the player tapped on the correct square
    /// </summary>
    private void HandleInput()
    {
        Vector3 tapPosition = GetTapPosition();

        if(tapPosition == Vector3.zero)
        {
            return;
        }

        Vector3 tapPositonWorld = Camera.main.ScreenToWorldPoint(tapPosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(tapPositonWorld, Vector3.forward, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if(hitObject == ColourSquares[CurrentTargetColour])
            {
                CorrectColourHit(tapPositonWorld);
            }
            else if(hitObject != ColourPicker)
            {
                Dead();
            }
        }
    }

    /// <summary>
    /// Change the current colour with a new on
    /// make sure it is not the same two times in a row
    /// </summary>
    private void ChooseNewColour()
    {
        bool flag = true;
        while(flag)
        {
            int newColour = Random.Range(0, 4);
            if(newColour != CurrentTargetColour)
            {
                CurrentTargetColour = newColour;
                flag = false;
            }
        }        
    }

    private void SpawnSparkParticle(Vector3 tapPosition)
    {
        tapPosition.z = ParticlesLayerZ;
        Instantiate(SparkParticlePrefab, tapPosition, Quaternion.identity);
    }

    private void SetSquaresRotation()
    {
        var SquaresScript = SquaresRoot.GetComponent<Squares>();
        if(!SquaresScript.Rotating)
        {
            SquaresScript.Rotating = true;
        }
        SquaresScript.RotationDirection *= -1;
    }

    private void SetDarkening(bool value)
    {
        var SquaresScript = SquaresRoot.GetComponent<Squares>();
        if(!SquaresScript.Darkening)
        {
            SquaresScript.Darkening = value;
        }
    }

    /// <summary>
    /// Run this when the correct colour is hit
    /// </summary>
    private void CorrectColourHit(Vector3 tapPosition)
    {
        ManageEffects();
        DelegateClicks();

        Score++;

        Started = true;

        SpawnSparkParticle(tapPosition);

        ColourPickerCurrentScale = ColourPickerMinScale;
        Vector3 colourPickerScale = ColourPicker.transform.localScale;
        colourPickerScale.x = colourPickerScale.z = ColourPickerMinScale;
        ColourPicker.transform.localScale = colourPickerScale;

        if (ReactionTime > MinReactionTime)
        {
            ReactionTime -= ReactionTimeChange;
        }

        ChooseNewColour();
        ColourPicker.GetComponent<Renderer>().material = Materials[CurrentTargetColour];
    }

    private void ManageEffects()
    {
        ClicksUntilNexRotationChange--;        
        if(ClicksUntilNexRotationChange == 0)
        {
            SetSquaresRotation();
            ClicksUntilNexRotationChange = Random.Range(5, 15);
        }

        ClicksUntilNextDarkening--;
        if(ClicksUntilNextDarkening == 0)
        {
            SetDarkening(true);
            ClicksUntilNextDarkening = Random.Range(5, 8);
        }
    }

    private void DelegateClicks()
    {
        var SquaresScript = SquaresRoot.GetComponent<Squares>();
        SquaresScript.Click();
    }

    /// <summary>
    /// Make the colour picker grow until it reaches the limit
    /// </summary>
    private void UpdateColourPicker()
    {
        if (Started)
        {
            ColourPickerCurrentScale += (ColourPickerMaxScale - ColourPickerMinScale) / ReactionTime * Time.deltaTime;

            if (ColourPickerCurrentScale > ColourPickerMaxScale)
            {
                Dead();
                return;
            }

            Vector3 colourPickerScale = ColourPicker.transform.localScale;
            colourPickerScale.x = colourPickerScale.z = ColourPickerCurrentScale;
            ColourPicker.transform.localScale = colourPickerScale;
        }
    }

    /// <summary>
    /// run this whent he player clicks the wrong colour or runs out of time
    /// </summary>
    private void Dead()
    {
        SetHighScore();
        PlayerPrefs.SetInt(GlobalStrings.CURRENT_SCORE_STRING, Score);
        PlayerPrefs.SetString(GlobalStrings.CURRENT_MODE, GlobalStrings.WACKY_MODE);
        Application.LoadLevel(GlobalStrings.SCENE_GAMEOVER);
    }

    /// <summary>
    /// Check if the new score is bigger than the currently saved high score and change it
    /// </summary>
    private void SetHighScore()
    {
        if(PlayerPrefs.HasKey(GlobalStrings.HIGH_SCORE_WACKY_STRING))
        {
            int currentHighScore = PlayerPrefs.GetInt(GlobalStrings.HIGH_SCORE_WACKY_STRING);
            if(Score > currentHighScore)
            {
                PlayerPrefs.SetInt(GlobalStrings.HIGH_SCORE_WACKY_STRING, Score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(GlobalStrings.HIGH_SCORE_WACKY_STRING, Score);
        }

        PlayerPrefs.Save();
    }

	// Update is called once per frame
	void Update () {
        HandleInput();
        UpdateColourPicker();
	}
}
