using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpponentMovement : CharacterMovement
{
    public new Opponent CharacterBase => base.CharacterBase as Opponent; 

    private bool isAgentDisabled = false;

    private void Start() 
    {
        agent.speed = moveSpeed;   
        CharacterBase.OnGetPunched += HandleOnGetPunched;
    }

    private void HandleOnGetPunched(float stunDuration)
    {
        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            DisableMovement();
            yield return new WaitForSeconds(stunDuration);
            EnableMovement();
        }
    }

    public event UnityAction<bool> OnMovementStateUpdated;

    public void SetDestination(Vector3 destination)
    {
        if(!isAgentDisabled)
            EnableMovement();
        
        agent.SetDestination(destination);
        lastPickTime = Time.time;
    }

    private void DisableMovement()
    {
        OnMovementStateUpdated?.Invoke(true);
        isAgentDisabled = true;
        agent.enabled = false;
    }

    private void EnableMovement()
    {
        OnMovementStateUpdated?.Invoke(false);
        isAgentDisabled = false;        
        agent.enabled = true;
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
}

public interface IGridCellPicker
{
    GridCell GetGridCell();
    IGridCellPicker Init(List<GridCell> grids);
}
