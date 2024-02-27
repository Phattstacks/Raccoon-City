using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Axis HorizontalMovement;

    private PlayerInput _playerInput;
    private InputAction _horizontalMovement;
    

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _horizontalMovement = _playerInput.actions["HorizontalMovement"];
    }

    private void Update()
    {
        HorizontalMovement = _horizontalMovement.ReadValue<Axis>();
    }
}
