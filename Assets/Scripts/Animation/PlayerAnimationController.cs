using System.Collections;
using UnityEngine;

public class PlayerAnimationController : CharacterAnimationControllerBase
{
    public new Player CharacterBase => base.CharacterBase as Player;
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
        CharacterBase.OnGetPunched += HandleGetPunch;
        CharacterBase.OnPunchUse += HandlePunchUse;
        CharacterBase.CharacterMovement.OnMovementUpdate += HandleOnMovementUpdate;
    }
    private void HandleGetPunch(float stunDuration)
    {
        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            SetBool(AnimationKeys.IsStun, true);

            yield return new WaitForSeconds(stunDuration);

            SetBool(AnimationKeys.IsStun, false);

            yield return new WaitForSeconds(0.15f); //treshold so character will not be stunned forevers
            CharacterBase.IsStunned = false;
        }
    }
    
    private void HandlePunchUse()
    {
        SetFloat(AnimationKeys.PunchRandomMultp, Random.Range(1f, 1.5f));
        SetTrigger(AnimationKeys.Punch);
    }

    private float moveLerpSpeed = 7.5f;
    private float currentMoveParamValue;
    private void HandleOnMovementUpdate(Vector2 moveInput)
    {
        float sqrMagnitude = moveInput.sqrMagnitude;

        currentMoveParamValue = Mathf.Lerp(currentMoveParamValue, sqrMagnitude, moveLerpSpeed * Time.deltaTime);

        currentMoveParamValue = Mathf.Clamp01(currentMoveParamValue);
        
        SetFloat(AnimationKeys.Move, currentMoveParamValue);
    }
}