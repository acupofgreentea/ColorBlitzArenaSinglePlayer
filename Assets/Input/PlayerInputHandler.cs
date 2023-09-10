using UnityEngine;
using UnityEngine.Events;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput inputActions;

    public Vector3 MoveInput => inputActions.PlayerActions.Movement.ReadValue<Vector3>();

    public event UnityAction OnPunch;
    private Player player;
    
    public PlayerInputHandler Init(Player player)
    {
        inputActions = new PlayerInput();
        this.player = player;
        return this;
    }

    private void Start() 
    {
        inputActions.PlayerActions.Punch.performed += HandlePunchPerform;   
        player.OnGetPunched += HandleGetPunched;
        player.OnStunFinished += HandleStunFinished;
        DisableInput();
        SessionManager.OnSessionStart += EnableInput; 
        SessionManager.OnSessionFinish += DisableInput; 
    }

    private void HandleStunFinished()
    {
        EnableInput(); 
    }

    private void HandleGetPunched(float stunDuration)
    {
        DisableInput();
    }

    private void HandlePunchPerform(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(player.IsStunned)
            return;
             
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
