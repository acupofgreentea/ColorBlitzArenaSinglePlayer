using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text remainingTimeText;

    [SerializeField] private List<TMP_Text> colorStatsText;

    [SerializeField] private GameObject textsPanel;

    private void Start()
    {
        SessionManager.OnGameTimeUpdate += HandleGameTimeUpdate;
        SessionManager.OnSessionStart += HandleSessionStart;
        GridManager.OnUpdatePaintedCounts += HandlePaintedCounts;
        remainingTimeText.gameObject.SetActive(false);
        textsPanel.gameObject.SetActive(false);
        gameTimeStr = new();
    }
    StringBuilder colorStatStr = new();

    private void HandlePaintedCounts(List<(ColorType, int)> paintedCounts)
    {
        for (int i = 0; i < paintedCounts.Count; i++)
        {
            var colorType = paintedCounts[i].Item1;
            
            colorStatStr.Clear();
            colorStatStr.Append(paintedCounts[i].Item2);
            
            switch (colorType)
            {
                case ColorType.Red:
                    colorStatsText[0].SetText(colorStatStr);
                    break;
                case ColorType.Blue:
                    colorStatsText[1].SetText(colorStatStr);
                    break;
                case ColorType.Green:
                    colorStatsText[2].SetText(colorStatStr);
                    break;
                case ColorType.Yellow:
                    colorStatsText[3].SetText(colorStatStr);
                    break;
            }
        }
    }

    private void HandleSessionStart()
    {
        remainingTimeText.gameObject.SetActive(true);
        textsPanel.gameObject.SetActive(true);
    }

    StringBuilder gameTimeStr;
    private void HandleGameTimeUpdate(int gameTime)
    {
        gameTimeStr.Clear();
        gameTimeStr.Append(gameTime);
        remainingTimeText.SetText(gameTimeStr);
    }

    private void OnDestroy()
    {
        SessionManager.OnGameTimeUpdate -= HandleGameTimeUpdate;
        GridManager.OnUpdatePaintedCounts -= HandlePaintedCounts;        
        SessionManager.OnCountdownStart -= HandleSessionStart;
    }

}
