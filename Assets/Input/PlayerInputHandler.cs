using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput inputActions;

    public Vector3 MoveInput => inputActions.PlayerActions.Movement.ReadValue<Vector3>();

    public event UnityAction OnPunch;

    private void Awake() 
    {
        inputActions = new PlayerInput();
    }

    private void Start() 
    {
        inputActions.PlayerActions.Punch.performed += HandlePunchPerform;   
        EnableInput(); 
    }

    private void HandlePunchPerform(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnPunch?.Invoke();
    }

    public void EnableInput()
    {
        inputActions.Enable();
    }

    public void DisableInput()
    {
        inputActions.Disable();
    }

}
