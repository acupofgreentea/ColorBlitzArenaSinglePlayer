using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardElementUI : MonoBehaviour
{
    [SerializeField] private TMP_Text paintedCountText;
    [SerializeField] private TMP_Text characterNameText;
    
    [SerializeField] private Image colorImage;

    public void SetElementUI(int paintedCount, string characterName, Color color)
    {
        paintedCountText.text = paintedCount.ToString();
        characterNameText.text = characterName;
        colorImage.color = color;
    }  
}
