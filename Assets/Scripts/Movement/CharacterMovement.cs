using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float rotateSpeed = 6f;

    protected NavMeshAgent agent;
    private float defaultSpeed;

    protected CharacterBase CharacterBase {get; private set;}

    public float MoveSpeed => moveSpeed;
    protected bool isAgentDisabled = false;

    public CharacterMovement Init(CharacterBase characterBase)
    {
        CharacterBase = characterBase;

        defaultSpeed = moveSpeed;

        agent = GetComponent<NavMeshAgent>();
    
        return this;
    }

    public void SetSpeedDefault() => moveSpeed = defaultSpeed;
    public void SetSpeedByMultp(float multp) => moveSpeed *= multp;

    public virtual void DisableMovement()
    {
        isAgentDisabled = true;
        agent.enabled = false;
    }

    public virtual void EnableMovement()
    {
        isAgentDisabled = false;        
        agent.enabled = true;
    }
}
