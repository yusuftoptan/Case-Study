using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    [Tooltip("Normal speed while player running")] 
    private float _normalSpeed;

    [SerializeField]
    [Tooltip("Slowed speed to use while player spending money on an option")] 
    private float _slowedSpeed;

    [SerializeField]
    [Tooltip("Sensitivity of horizontal movement")] 
    private float _sensitivity;

    private float _currentSpeed;
    private bool _run = false;

    private PlayerAnimationController _anim;

    //Input related attributes
    private Vector3 _touchPos;
    private Vector3 _deltaTouch;
    private Vector3 _movementVector;
    #endregion

    #region Callback Functions
    private void Start()
    {
        _anim = GetComponent<PlayerAnimationController>();
        _currentSpeed = _normalSpeed;
    }

    private void Update()
    {
        //Get input
        if (Input.GetMouseButtonDown(0))
        {
            _run = true;
            _touchPos = Input.mousePosition;
            _anim.run = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _run = false;
            _anim.run = false;
        }
    }

    private void FixedUpdate()
    {
        if (_run)
        {
            //Set deltaTouch for horizontal movement
            _deltaTouch = Input.mousePosition - _touchPos;
            _touchPos = Input.mousePosition;

            //Set movement vector
            _movementVector = Vector3.forward * Time.fixedDeltaTime * _currentSpeed; //Forward movement
            _movementVector += Vector3.right * Time.fixedDeltaTime * _sensitivity * _deltaTouch.x; //Horizontal movement

            //Move
            transform.Translate(_movementVector);

            //Clamp movement
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -4.5f, 4.5f);
            transform.position = position;
        }
    }
    #endregion

    #region Custom Functions

    //Set whether player runs normally or slower
    public bool slowerRun
    {
        set
        {
            if (value)
                _currentSpeed = _slowedSpeed;
            else
                _currentSpeed = _normalSpeed;

            _anim.slowedMovement = value;
        }
    }
    #endregion
}
