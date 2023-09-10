using System.Collections.Generic;
using UnityEngine;

public class RandomGridCellPicker : MonoBehaviour, IGridCellPicker
{
    private List<GridCell> grids;

    public GridCell GetGridCell()
    {
        return grids.GetRandom();
    }

    public IGridCellPicker Init(List<GridCell> grids)
    {
        this.grids = grids;
        return this;
    }
}

public interface IGridCellPicker
{
    GridCell GetGridCell();
    IGridCellPicker Init(List<GridCell> grids);
}
