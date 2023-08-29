using System;
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
    
    private int GetPercentageOfColor(ColorType type)
    {
        float countOfType = gridCells.FindAll(x => x.ColorType == type).Count;    
        float percentageOfType = countOfType / gridCells.Count * 100;
        percentageOfType = Mathf.RoundToInt(percentageOfType);

        return (int)percentageOfType;
    }

    private void OnGUI() 
    {
        int percentageOfBlue = GetPercentageOfColor(ColorType.Blue); 
        int percentageOfRed = GetPercentageOfColor(ColorType.Red);
        int percentageOfYellow = GetPercentageOfColor(ColorType.Yellow);
        int percentageOfGreen = GetPercentageOfColor(ColorType.Green); 
        int percentageOfDefault = GetPercentageOfColor(ColorType.Default);

        GUI.Label(new Rect(0, 0, 200, 250), 
        $"Blue %{percentageOfBlue}\nRed %{percentageOfRed}\nYellow %{percentageOfYellow}\nGreen %{percentageOfGreen}\nDefault %{percentageOfDefault}");    
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
}