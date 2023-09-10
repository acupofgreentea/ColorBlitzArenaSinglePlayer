using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : CharacterMovement
{
    protected new Player CharacterBase => base.CharacterBase as Player;

    public event UnityAction<Vector2> OnMovementUpdate; 

    public Vector3 MovementInput {get; set;}

    private void Start() 
    {
        CharacterBase.OnGetPunched += HandleGetPunched;  
        CharacterBase.OnStunFinished += EnableMovement;  
    }

    private void HandleStunFinished()
    {
        EnableMovement();
    }

    private void HandleGetPunched(float stunDuration)
    {        
        DisableMovement();
    }

    private void Update() 
    {
        Move();
    }
    
    private void Move()
    {
        if(isAgentDisabled)
            return;
        
        var Direction = CharacterBase.PlayerInputHandler.MoveInput.normalized;

        OnMovementUpdate?.Invoke(Direction.ToVector2());

        MovementInput = Direction;

        agent.Move(MovementInput * moveSpeed * Time.deltaTime);

        if (MovementInput == Vector3.zero) 
            return;
        
        Quaternion lookRotation = Quaternion.LookRotation(MovementInput.normalized, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}
