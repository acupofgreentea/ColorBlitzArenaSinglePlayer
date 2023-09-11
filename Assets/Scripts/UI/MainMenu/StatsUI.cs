using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text totalPaintedCountText; 
    [SerializeField] private TMP_Text bluePaintedCountText; 
    [SerializeField] private TMP_Text yellowPaintedCountText; 
    [SerializeField] private TMP_Text redPaintedCountText; 
    [SerializeField] private TMP_Text greenPaintedCountText; 

    private void Start() 
    {
        SetTexts();
    
    }
    private void SetTexts()
    {
        var data = PlayerDataHelper.Instance.Load();
        totalPaintedCountText.text = data.GridPaintedCount.ToString();

        bluePaintedCountText.text =   data.BluePaintCount.ToString();
        yellowPaintedCountText.text = data.YellowPaintCount.ToString();
        redPaintedCountText.text =    data.RedPaintCount.ToString();
        greenPaintedCountText.text =  data.GreenPaintCount.ToString();
    }
}
