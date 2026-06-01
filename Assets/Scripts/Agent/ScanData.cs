using System.Collections.Generic;
using UnityEngine;
public class ScanData
{
    public List<Cell> Cells = new List<Cell>();
    public HashSet<Vector2Int> ScannedCellIDs = new HashSet<Vector2Int>();
}
