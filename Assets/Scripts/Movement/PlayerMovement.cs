using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : CharacterMovement
{
    protected new Player CharacterBase => base.CharacterBase as Player;

    public event UnityAction<Vector2> OnMovementUpdate; 

    public Vector3 MovementInput {get; set;}

    private PlayerInput inputActions;

    private void Awake() 
    {
        inputActions = new PlayerInput();
    }

    private void Start() 
    {
        EnableMovement();    
    }

    public void EnableMovement()
    {
        inputActions.Enable();
    }

    public void DisableMovement()
    {
        inputActions.Disable();
    }

    
    private void Update() 
    {
        Move();
    }
    private void Move()
    {
        var Direction = inputActions.PlayerActions.Movement.ReadValue<Vector3>().normalized;

        OnMovementUpdate?.Invoke(Direction.ToVector2());

        MovementInput = Direction;

        agent.Move(MovementInput * moveSpeed * Time.deltaTime);

        if (MovementInput == Vector3.zero) 
            return;
        
        Quaternion lookRotation = Quaternion.LookRotation(MovementInput.normalized, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}
