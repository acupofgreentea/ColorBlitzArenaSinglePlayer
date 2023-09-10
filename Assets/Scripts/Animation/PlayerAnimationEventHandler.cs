using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    private Player player;
    public PlayerAnimationEventHandler Init(Player player)
    {
        this.player = player;
        return this;
    }

    public void HitPunch()
    {
        player.CharacterPunchController.HitPunch();
    }
}
