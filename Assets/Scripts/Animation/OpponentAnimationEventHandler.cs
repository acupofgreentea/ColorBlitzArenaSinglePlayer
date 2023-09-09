using UnityEngine;

public class OpponentAnimationEventHandler : MonoBehaviour
{
    private Opponent opponent;
    public OpponentAnimationEventHandler Init(Opponent opponent)
    {
        this.opponent = opponent;
        return this;
    }

    public void HitPunch()
    {
        opponent.OpponentPunchController.HitPunch();
    }
}
