using UnityEngine;
using UnityEngine.Events;

public class OpponentMovement : CharacterMovement
{
    public new Opponent CharacterBase => base.CharacterBase as Opponent; 
    public event UnityAction<bool> OnMovementStateUpdated;

    private void Start() 
    {
        agent.speed = moveSpeed;   
        DisableMovement();

        CharacterBase.OnGetPunched += HandleOnGetPunched;
        CharacterBase.OnStunFinished += HandleStunFinished;
        SessionManager.OnSessionStart += EnableMovement;
        SessionManager.OnSessionFinish += DisableMovement;
    }

    private void HandleStunFinished()
    {
        if(!Managers.Instance.SessionManager.IsSessionActive)
            return;
        
        EnableMovement();
    }

    private void HandleOnGetPunched(float stunDuration)
    {
        DisableMovement();
    }
    public void SetDestination(Vector3 destination)
    {
        if(!isAgentDisabled)
            EnableMovement();
        
        agent.SetDestination(destination);
        lastPickTime = Time.time;
    }

    public override void DisableMovement()
    {
        OnMovementStateUpdated?.Invoke(true);
        base.DisableMovement();
    }

    public override void EnableMovement()
    {
        OnMovementStateUpdated?.Invoke(false);
        base.EnableMovement();
    }

    private float gridPickInterval = 3f;
    private float lastPickTime;

    private void Update() 
    {
        if(isAgentDisabled)
            return;
        
        if(Time.time < gridPickInterval + lastPickTime)
        {
            if(agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath)
            {
                //must set a new destination
                SetDestination(GetRandomGridCellDestination());
            }  
            return;
        }

        SetDestination(GetRandomGridCellDestination());
    }

    private Vector3 GetRandomGridCellDestination()
    {
        GridCell targetGrid = Managers.Instance.GridManager.GetGridCell();
        return targetGrid.transform.position;
    }

    private void OnDestroy() 
    {
        CharacterBase.OnGetPunched -= HandleOnGetPunched;
        CharacterBase.OnStunFinished -= HandleStunFinished;
        SessionManager.OnSessionStart -= EnableMovement;
        SessionManager.OnSessionFinish -= DisableMovement;    
    }
}
