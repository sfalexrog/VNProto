using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

// Restrict attachment requirements
[RequireComponent(typeof(Image))]
public class SwipableCard : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    // Rotation point
    private Vector2 PivotPoint;
    private Vector3 PivotWorld;
    private Vector2 PivotWorld2D;
    [Header("Appearance")]
    [Tooltip("Distance to pivot point (calculated from this component's anchored position)")]
    public float Radius;

    [Tooltip("Controls how fast the card snaps into place")]
    public float Snappiness;
    
    private Image _card;
    [Header("Behaviour")]
    [Tooltip("First snapping angle (for outcome previews)")]
    public float ShiftAngle;
    [Tooltip("Second snapping angle (for accepting outcomes)")]
    public float AcceptAngle;

    [Header("Event Handlers")]
    // Fired when player shifts the card by ShiftAngle degrees to the left
    public CardShiftLeftEvent onShiftLeft;
    // Fired when player shifts the card by ShiftAngle degrees to the right
    public CardShiftRigthEvent onShiftRight;
    // Fired when player shifts the card by AcceptAngle degrees to the left
    public CardSwipeLeftEvent onSwipeLeft;
    // Fired when player shifts the card by AcceptAngle degrees to the right
    public CardSwipeRightEvent onSwipeRight;
    // Fired when the card is centered
    public CardCenterEvent onCenter;

    private float _radius;
    private float _angle;
    private float _prevAngle;
    private float _angSpeed;

    private float _targetAngle;

    private bool _gravitates;

    // Set this to true to ignore user input
    [HideInInspector]
    public bool IgnoreInput;

    public float Angle { get { return _angle; } }

    // Fired when a player tilts the card, but doesn't 
    [Serializable]
    public class CardShiftLeftEvent : UnityEvent
    {
    }

    [Serializable]
    public class CardShiftRigthEvent : UnityEvent
    {
    }

    [Serializable]
    public class CardSwipeLeftEvent : UnityEvent
    { 
    }

    [Serializable]
    public class CardSwipeRightEvent : UnityEvent
    {
    }

    [Serializable]
    public class CardCenterEvent : UnityEvent
    {
    }

    public void OnPointerDown(PointerEventData ped)
    {
        _gravitates = IgnoreInput;
    }

    public void OnPointerUp(PointerEventData ped)
    {
        _gravitates = true;
    }

    public void OnDrag(PointerEventData ped)
    {
        if (!IgnoreInput)
        {
            var rvec = ped.position - PivotWorld2D;
            var angle = Mathf.Rad2Deg * Mathf.Atan2(rvec.y, rvec.x) - 90.0f;
            _angSpeed = (angle - _angle) / Time.deltaTime;
            SetAngle(angle);
        }
    }

    public void Reset()
    {
        _angle = 0.0f;
        _prevAngle = 0.0f;
        SetAngle(_angle);
    }

    public void SetAngle(float angle)
    {
        var angRad = Mathf.Deg2Rad * (angle + 90.0f);
        var direction = new Vector2(Mathf.Cos(angRad), Mathf.Sin(angRad));
        var newPos = PivotPoint + Radius * direction;
        _card.rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _card.rectTransform.anchoredPosition = newPos;
        _prevAngle = _angle;
        _angle = angle;
        UpdateAngles();
    }

    // Set target angle and fire events if necessary
    private void UpdateAngles()
    {
        // If card is rotated by (-ShiftAngle / 2; ShiftAngle / 2) degrees,
        // make it snap to 0 degrees
        if (Mathf.Abs(_angle) < ShiftAngle / 2)
        {
            _targetAngle = 0.0f;
            // Additionally, if previous angle was outside this range, fire
            // onCenter event handlers
            if (Mathf.Abs(_prevAngle) >= ShiftAngle / 2)
            {
                onCenter.Invoke();
            }
        }
        else
        {
            // Handle positive and negative shifts separately
            // because of case overlapping issues
            if (_angle > 0)
            {
                // If card is rotated by [-ShiftAngle / 2; AcceptAngle)
                // (so it's closer to ShiftAngle than to AcceptAngle), make it snap to
                // ShiftAngle degrees
                if (_angle < AcceptAngle)
                {
                    _targetAngle = ShiftAngle;
                    // Additionally, if previous angle was outside this range,
                    // fire onShiftRight event handlers
                    if (_prevAngle < ShiftAngle / 2)
                    {
                        // Note that we don't check _prevAngle being larger than AcceptAngle,
                        // we assume this means the card is accepted
                        onShiftLeft.Invoke();
                    }
                }
                // If card is rotated by no less than AcceptAngle, it triggers OnSwipeRight
                // event handlers. It will be shifted to AcceptAngle * 2, and it's a good
                // idea to gradually fade it out while it's moving and reset it.
                else if (_angle >= AcceptAngle)
                {
                    _targetAngle = 3 * AcceptAngle;
                    if (_prevAngle < AcceptAngle)
                    {
                        onSwipeLeft.Invoke();
                    }
                }
            }
            else
            {
                // More of the same, but for left swipes
                if (_angle > -AcceptAngle)
                {
                    _targetAngle = -ShiftAngle;
                    if (_prevAngle > -ShiftAngle / 2)
                    {
                        onShiftRight.Invoke();
                    }
                }
                else if (_angle <= -AcceptAngle)
                {
                    _targetAngle = -3 * AcceptAngle;
                    if (_prevAngle > -AcceptAngle)
                    {
                        onSwipeRight.Invoke();
                    }
                }
            }
        }
    }

    void Awake ()
    {
        _card = GetComponent<Image>();
        var position = _card.rectTransform.position;
        PivotPoint = _card.rectTransform.anchoredPosition - new Vector2(0, Radius);
        PivotWorld = _card.rectTransform.TransformPoint(new Vector3(0, -Radius, 0));
        PivotWorld2D = new Vector2(PivotWorld.x, PivotWorld.y);
        Reset();
    }
	
	// Animate shifts (if applicable)
	void Update () {
        if (_gravitates)
        {
            var delta = _angle - _targetAngle;
            SetAngle(_angle - Snappiness * delta * Time.deltaTime);
        }
		
	}
}
