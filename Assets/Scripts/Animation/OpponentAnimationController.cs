using System.Collections;
using UnityEngine;

public class OpponentAnimationController : CharacterAnimationControllerBase
{
    private float currentMoveParamValue = 0f;
    public new Opponent CharacterBase => base.CharacterBase as Opponent;

    protected override void CreateDictionary()
    {
        animationDic = new()
        {
            {AnimationKeys.Move, AnimationHashKeys.MoveHashKey},
            {AnimationKeys.IsSpeedBoost, AnimationHashKeys.IsSpeedBoostHashKey},
            {AnimationKeys.IsStun, AnimationHashKeys.IsStunHashKey},
            {AnimationKeys.Punch, AnimationHashKeys.PunchHashKey},
            {AnimationKeys.PunchRandomMultp, AnimationHashKeys.PunchRandomMultpHashKey},
        };
    }

    private void Start() 
    {
        CharacterBase.CharacterMovement.OnMovementStateUpdated += HandleOnMovementUpdate;
        CharacterBase.OnGetPunched += HandleGetPunched;
        CharacterBase.OnStunFinished += HandleStunFinished;
        CharacterBase.OnPunchUse += HandleUsePunch;
    }

    private void HandleStunFinished()
    {
        SetBool(AnimationKeys.IsStun, false);

        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            yield return new WaitForSeconds(0.15f);
            CharacterBase.IsStunned = false;
        }
    }

    private void HandleUsePunch()
    {
        SetFloat(AnimationKeys.PunchRandomMultp, Random.Range(1f, 1.5f));
        SetTrigger(AnimationKeys.Punch);
    }

    private void HandleGetPunched(float stunDuration)
    {
        SetBool(AnimationKeys.IsStun, true);
    }

    private Coroutine movementUpdateCoroutine;

    private void HandleOnMovementUpdate(bool isStopping)
    {
        if(movementUpdateCoroutine != null)
            StopCoroutine(movementUpdateCoroutine);
        
        StartCoroutine(Sequence());
        
        IEnumerator Sequence()
        {
            float timer = 0.5f;
            float elapsedTime = 0f;

            float startValue = currentMoveParamValue;
            float targetValue = isStopping ? 0f : 1f;

            while (elapsedTime < timer)
            {
                elapsedTime += Time.deltaTime;
                currentMoveParamValue = Mathf.Lerp(startValue, targetValue, elapsedTime / timer);

                
                SetFloat(AnimationKeys.Move, currentMoveParamValue);
                yield return null;
            }
        }
    }

    private void OnDestroy() 
    {
        CharacterBase.CharacterMovement.OnMovementStateUpdated -= HandleOnMovementUpdate;
        CharacterBase.OnGetPunched -= HandleGetPunched;
        CharacterBase.OnStunFinished -= HandleStunFinished;
        CharacterBase.OnPunchUse -= HandleUsePunch;    
    }
}
