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
}