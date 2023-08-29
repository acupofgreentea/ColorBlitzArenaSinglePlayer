using System.Collections;
using UnityEngine;

public class PlayerPunchController : MonoBehaviour, IPunchable, IPunchUser
{
    [SerializeField] private float stunDuration = 1.25f;

    private Player player;

    public PlayerPunchController Init(Player player)
    {
        this.player = player;
        return this;
    }

    public void HandleGetPunch()
    {
        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            player.CharacterAnimationControllerBase.SetBool(AnimationKeys.IsStun, true);

            yield return new WaitForSeconds(stunDuration);

            player.CharacterAnimationControllerBase.SetBool(AnimationKeys.IsStun, false);
        }
    }

    public void HitPunch()
    {
        currentPunchable.HandleGetPunch();
    }

    public void Punch()
    {
        player.CharacterAnimationControllerBase.SetFloat(AnimationKeys.PunchRandomMultp, Random.Range(1f, 1.5f));
        player.CharacterAnimationControllerBase.SetTrigger(AnimationKeys.Punch);
    }

    private IPunchable currentPunchable;

    private void OnTriggerEnter(Collider other) 
    {

        if(!other.TryGetComponent(out IPunchable punchable) || other == this)
            return;

        /*if(other.TryGetComponent(out NetworkCharacterControllerPrototype networkCharacterController))
        {
            if(!networkCharacterController.IsStunned)
                Punch();
*/
            currentPunchable = punchable;
        //}
    }
}

public interface IPunchable
{
    void HandleGetPunch();
}

public interface IPunchUser
{
    void Punch();
}
