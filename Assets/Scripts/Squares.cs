using UnityEngine;
using System.Collections;

public class Squares : MonoBehaviour {

    public bool Rotating { get; set; }
    private int _RotationDirection;
    public int RotationDirection
    {
        get
        {
            return _RotationDirection;
        }
        set
        {
            RotationSpeed += RotationSpeedIncrease;
            _RotationDirection = value;
        }
    }

    private Vector3 RotationVector = new Vector3(0, 0, 1);
    private float RotationSpeed = 10;
    private float RotationSpeedIncrease = 2;

    [SerializeField]
    private GameObject DarkScreen;

    private bool _Darkening;
    public bool Darkening
    {
        get
        {
            return _Darkening;
        }
        set
        {
            if (value)
            {
                DarkTTL = Random.Range(3, 6);
                DarkTimer = 0;
                _Darkening = true;
                StopDarkening = false;
            }
            else
            {
                StopDarkening = true;
            }            
        }
    }
    private bool StopDarkening = false;
    private float DarkeningSpeed = 1;
    private float DarkTimer = 0;
    private int DarkTTL = 3;

	// Use this for initialization
	void Start () {
        Darkening = false;
        Rotating = false;
        RotationDirection = 1;
	}

    /// <summary>
    /// rotate the object if neccesary
    /// </summary>
    private void Rotate()
    {
        if(Rotating)
        {
            gameObject.transform.Rotate(RotationVector, RotationSpeed * RotationDirection * Time.deltaTime);
        }
    }

    /// <summary>
    /// Darken the screen if neccessary
    /// </summary>
    private void Darken()
    {
        if(Darkening)
        {
            DarkTimer += DarkeningSpeed * Time.deltaTime;
            Color darkScreenColor = DarkScreen.GetComponent<Renderer>().material.color;
            darkScreenColor.a = Mathf.Sin(DarkTimer) / 2 + 0.3f;
            if (darkScreenColor.a <= 0 && StopDarkening)
            {
                Darkening = false;
            }
            DarkScreen.GetComponent<Renderer>().material.color = darkScreenColor;
        }
    }

    public void Click()
    {
        if (Darkening)
        {
            DarkTTL--;
            if (DarkTTL <= 0)
            {
                StopDarkening = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Rotate();
        Darken();
	}
}