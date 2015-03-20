using UnityEngine;
using System.Collections;

public class ObjectAnimation : MonoBehaviour {

    [SerializeField]
    private bool ComingIn;

    [SerializeField]
    private Vector3 PositionOffset;

    [SerializeField]
    private Vector3 ScaleOffset;

    [SerializeField]
    private Vector3 RotationOffset;

    [SerializeField]
    private AnimationCurve Curve;

    [SerializeField]
    private float Duration;

    [SerializeField]
    private float Delay;

    private Vector3 StartingPosition;
    private Vector3 TargetPosition;
    private Vector3 StartingScale;
    private Vector3 TargetScale;
    private Vector3 StartingRotation;
    private Vector3 TargetRotation;
    private float Timer;

	// Use this for initialization
	void Start () {
        Vector3 currentPosition = transform.position;
        StartingPosition = ComingIn ? currentPosition + PositionOffset : currentPosition;
        TargetPosition = ComingIn ? currentPosition : currentPosition + PositionOffset;
        transform.position = StartingPosition;

        Vector3 currentScale = transform.localScale;
        StartingScale = ComingIn ? currentScale + ScaleOffset : currentScale;
        TargetScale = ComingIn ? currentScale : currentScale + ScaleOffset;
        transform.localScale = StartingScale;

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        StartingRotation = ComingIn ? currentRotation + RotationOffset : currentRotation;
        TargetRotation = ComingIn ? currentRotation : currentRotation + RotationOffset;

        Timer = Duration;
	}
	
	// Update is called once per frame
	void Update () {
        if(Delay > 0)
        {
            Delay -= Time.deltaTime;
            return;

        }
        if(Timer <= 0)
        {
            return;
        }

        Timer -= Time.deltaTime;
        if(Timer <= 0)
        {
            Timer = 0;
        }
                
        float currentCurveValue = Curve.Evaluate(1 - (Timer / Duration));
        Debug.Log(currentCurveValue);
        Vector3 currentPosition = StartingPosition + (TargetPosition - StartingPosition) * currentCurveValue;
        transform.position = currentPosition;

        Vector3 currentScale = StartingScale + (TargetScale - StartingScale) * currentCurveValue;
        transform.localScale = currentScale;

        Vector3 currentRotation = StartingRotation + (TargetRotation - StartingRotation) * currentCurveValue;
        transform.localRotation = Quaternion.Euler(currentRotation);        
	}
}
