using System.Collections;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, IPowerUp
{
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float duration = 3.5f;

    private Renderer _renderer;

    private bool isInteractable = true;

    private void Awake() 
    {
        _renderer = GetComponentInChildren<Renderer>();    

        startPosY = transform.position.y;
    }

    private float startPosY;
    private void Update()
    {
        transform.position = 
        new Vector3(transform.position.x, 
        startPosY + Mathf.Sin(Time.time), 
        transform.position.z);
    }
    
    public void Activate()
    {
        _renderer.enabled = false;
        isInteractable = false;

        var playerAnimationController = interactedPlayer.CharacterAnimationControllerBase;
        var playerMovement = interactedPlayer.CharacterMovement;

        playerMovement.SetSpeedDefault();

        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            playerAnimationController.SetBool(AnimationKeys.IsSpeedBoost, true);
            playerMovement.SetSpeedByMultp(speedMultiplier);
            yield return new WaitForSeconds(duration);
            playerAnimationController.SetBool(AnimationKeys.IsSpeedBoost, false);
            playerMovement.SetSpeedDefault();
        }
    }

    private Player interactedPlayer;
    private void OnTriggerEnter(Collider other) 
    {
        if(!other.TryGetComponent(out Player player))
            return;

        if(!isInteractable)
            return;

        interactedPlayer = player;
        Activate();
    }
}
