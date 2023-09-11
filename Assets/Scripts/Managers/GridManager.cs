using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour 
{
    [SerializeField] GameObject gridCellPrefab;
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5; 
    [SerializeField] private float cellSize = 1.0f; 

    [SerializeField] private List<GridCell> gridCells = new();

    [SerializeField] private ColorConfig defaultGridConfig;

    public GridCell GetRandomGridCell => gridCells.GetRandom();
    private IGridCellPicker gridCellPicker;

    private void Awake() 
    {
        gridCellPicker = GetComponent<IGridCellPicker>().Init(gridCells);    
    }
    private void Start() 
    {
        SetAllGridsColorDataToDefault();
        SessionScoreboardUI.GetScoreboardPercentages += GetPaintedCounts;
    }

    public GridCell GetGridCell() => gridCellPicker.GetGridCell();

    private void SetAllGridsColorDataToDefault()
    {
        foreach (GridCell grid in gridCells)
        {
            grid.SetColorData(defaultGridConfig.GetColorData);
        }
    }
    
    private int GetPercentageOfColor(ColorType type)
    {
        float countOfType = gridCells.FindAll(x => x.ColorType == type).Count;    
        float percentageOfType = countOfType / gridCells.Count * 100;
        percentageOfType = Mathf.RoundToInt(percentageOfType);

        return (int)percentageOfType;
    }

    private int GetCountOfColor(ColorType type)
    {
        int countOfType = gridCells.FindAll(x => x.ColorType == type).Count;
        return countOfType;    
    }

    private List<(ColorType, int)> GetPaintedCounts()
    {
        int percentageOfBlue = GetCountOfColor(ColorType.Blue); 
        int percentageOfRed = GetCountOfColor(ColorType.Red);
        int percentageOfYellow = GetCountOfColor(ColorType.Yellow);
        int percentageOfGreen = GetCountOfColor(ColorType.Green); 
        
        var blue = (ColorType.Blue, percentageOfBlue); 
        var red = (ColorType.Red, percentageOfRed); 
        var yellow = (ColorType.Yellow, percentageOfYellow); 
        var green = (ColorType.Green, percentageOfGreen); 

        List<(ColorType, int)> values = new()
        {
            blue, red, yellow, green
        };

        values = values.OrderByDescending(x=> x.Item2).ToList();

        return values;
    }

    private void OnGUI() 
    {
        int percentageOfBlue = GetPercentageOfColor(ColorType.Blue); 
        int percentageOfRed = GetPercentageOfColor(ColorType.Red);
        int percentageOfYellow = GetPercentageOfColor(ColorType.Yellow);
        int percentageOfGreen = GetPercentageOfColor(ColorType.Green); 
        int percentageOfOncolored = GetPercentageOfColor(ColorType.Uncolored);

        GUI.Label(new Rect(0, 0, 200, 250), 
        $"Blue %{percentageOfBlue}\nRed %{percentageOfRed}\nYellow %{percentageOfYellow}\nGreen %{percentageOfGreen}\nUncolored %{percentageOfOncolored}");    
    }


    #if UNITY_EDITOR
    [ContextMenu("Create")]
    private void GenerateGrid()
    {
        Vector3 startPosition = transform.position - new Vector3((width - 1) * cellSize * 0.5f, 0f, (height - 1) * cellSize * 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 spawnPosition = startPosition + new Vector3(x * cellSize, 0f, z * cellSize);
                var cell = PrefabUtility.InstantiatePrefab(gridCellPrefab) as GameObject;

                cell.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                cell.transform.parent = this.transform;
            }
        }
    }

    [ContextMenu("Destroy")]
    private void Destroy()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    [ContextMenu("Fill List")]
    private void AddToList()
    {
        gridCells.Clear();
        foreach (Transform item in transform)
        {
            gridCells.Add(item.GetComponent<GridCell>());
        }
    }
#endif

    public List<GridCell> GetCellsInRange(Transform target, float radius)
    {
        List<GridCell> gridsInRange = new();

        foreach (var grid in gridCells)
        {
            if(Vector3.Distance(grid.transform.position, target.position) > radius)
                continue;

            gridsInRange.Add(grid);
        }
        
        gridsInRange = gridsInRange.OrderBy(x => Vector3.Distance(x.transform.position, target.position)).ToList();
        return gridsInRange;
    }

    private void OnDestroy()
    {
        SessionScoreboardUI.GetScoreboardPercentages -= GetPaintedCounts;
    }
}