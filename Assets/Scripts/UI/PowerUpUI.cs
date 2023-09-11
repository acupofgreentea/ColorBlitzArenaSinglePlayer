using DG.Tweening;
using TMPro;
using UnityEngine;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private TMP_Text powerUpText;

    private void Start() 
    {
        powerUpText.gameObject.SetActive(false); 
    }

    private void HandlePowerUpActivate()
    {
        powerUpText.gameObject.SetActive(true);
        powerUpText.text = "Speed up";
        powerUpText.DOFade(0f, 1f);
    }
}
