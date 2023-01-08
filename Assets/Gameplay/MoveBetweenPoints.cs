using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class MoveBetweenPoints : MonoBehaviour
{
    private PlayerInput _playerInput;
    
    [SerializeField] 
    private MovementPoint[] points;
    private int _currentPoint = 0;

    [SerializeField]
    private float movementTime = 2f;
    private float _currentTime = 0f;
    
    private Vector3 _startPos;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    
    private void Start()
    {
        Transform t;
        (t = transform).position = points[0].transform.position;
        _startPos = t.position;
        _currentPoint = 0;
    }
    
    private void OnEnable()
    {
        _playerInput.actions.FindAction("Left").performed += MoveLeft;
        _playerInput.actions.FindAction("Right").performed += MoveRight;
    }
    
    private void OnDisable()
    {
        _playerInput.actions.FindAction("Left").performed -= MoveLeft;
        _playerInput.actions.FindAction("Right").performed -= MoveRight;
    }

    private  void MoveLeft(InputAction.CallbackContext _)
    {
        if (_currentPoint == 0) return;
        _currentPoint--;
        _startPos = transform.position;
        _currentTime = 0;
        //Can put stuff for anim here if time permits
    }

    private void MoveRight(InputAction.CallbackContext _)
    {
        if (_currentPoint + 1 == points.Length) return;
        _currentPoint++;
        _startPos = transform.position;
        _currentTime = 0;
        //Can put stuff for anim here if time permits
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        Vector3 dir =  points[_currentPoint].transform.position - _startPos;
        if (!(Vector3.Distance(transform.position, points[_currentPoint].transform.position) <= .5f))
            transform.position += dir.normalized * Time.deltaTime * movementTime;
        else
        {
            transform.position = points[_currentPoint].transform.position;
        }
    }
}
