using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : CharacterMovement
{
    protected new Player CharacterBase => base.CharacterBase as Player;

    public event UnityAction<Vector2> OnMovementUpdate; 

    public Vector3 MovementInput {get; set;}

    [SerializeField] private Joystick _joystick;
    private void Update() 
    {
        Move();
    }

    private void Move()
    {
        var Direction = _joystick.Direction.normalized;

        OnMovementUpdate?.Invoke(Direction);

        MovementInput = Direction.ToVector3();

        agent.Move(MovementInput * moveSpeed * Time.deltaTime);

        if (MovementInput == Vector3.zero) 
            return;
        
        Quaternion lookRotation = Quaternion.LookRotation(MovementInput.normalized, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}
