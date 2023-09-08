using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBombGridPicker : MonoBehaviour
{
    private GridManager gridManager;

    private GridCell currentColorBombGrid;

    [SerializeField] private float colorBombSelectInterval = 5f;
    [SerializeField] private float radius = 10f;

    private float lastPickTime;

    private List<GridCell> gridCellsInRange = new();
    
    private void Awake() 
    {
        gridManager = GetComponent<GridManager>();    
    }

    private void Start() 
    {
        lastPickTime = Time.time;    
    }
    private void SetGridsInRange()
    {
        gridCellsInRange = gridManager.GetCellsInRange(currentColorBombGrid.transform, radius);
    }

    private void Update() 
    {
        if(currentColorBombGrid != null)
            return;

        if(Time.time < lastPickTime + colorBombSelectInterval)
            return;

        SelectRandomColorBombGrid();
    }

    private void SelectRandomColorBombGrid()
    {
        GridCell randomCell = gridManager.GetRandomGridCell;
        currentColorBombGrid = randomCell;
        SetGridsInRange();
        
        randomCell.ColorBombGridCell.Activate();

        currentColorBombGrid.OnTriggered += HandleTriggered;
    }

    private void HandleTriggered(CharacterBase player)
    {
        currentColorBombGrid.OnTriggered -= HandleTriggered;

        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            foreach (var item in gridCellsInRange)
            {
                item.ChangeColor(player.CharacterColor.ColorData, item.transform);
                yield return new WaitForSeconds(0.01f);
            }
        }

        lastPickTime = Time.time;
        currentColorBombGrid = null;
    }
}
