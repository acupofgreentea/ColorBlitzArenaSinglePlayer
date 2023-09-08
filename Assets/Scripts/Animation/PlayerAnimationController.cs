using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
        CharacterBase.PlayerPunchController.OnGetPunched += HandleGetPunch;
        CharacterBase.PlayerPunchController.OnPunchUse += HandlePunchUse;
    }

    private void Update()
    {
        HandleOnMovementUpdate(CharacterBase.CharacterMovement.MovementInput.sqrMagnitude);
    }

    private float moveLerpSpeed = 7.5f;
    private float currentMoveParamValue;
    private void HandleOnMovementUpdate(float moveInput)
    {
        float sqrMagnitude = moveInput;

        currentMoveParamValue = Mathf.Lerp(currentMoveParamValue, sqrMagnitude, moveLerpSpeed * Time.deltaTime);

        currentMoveParamValue = Mathf.Clamp01(currentMoveParamValue);
        
        SetFloat(AnimationKeys.Move, currentMoveParamValue);
    }

    private void HandleGetPunch(float stunDuration)
    {
        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            SetBool(AnimationKeys.IsStun, true);

            yield return new WaitForSeconds(stunDuration);

            SetBool(AnimationKeys.IsStun, false);
        }
    }
    
    private void HandlePunchUse()
    {
        SetFloat(AnimationKeys.PunchRandomMultp, Random.Range(1f, 1.5f));
        SetTrigger(AnimationKeys.Punch);
    }
    
}