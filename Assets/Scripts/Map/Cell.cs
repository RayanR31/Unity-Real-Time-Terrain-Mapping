using UnityEngine;

public class Cell
{
    public Vector2Int cellID ;
    public Vector3 worldPos ;
    public bool isExplored ;
    public Vector3 normal;


    public Cell(Vector2Int cellID, Vector3 worldPos, bool isExplored, Vector3 normal)
    {
        this.cellID = cellID;
        this.worldPos = worldPos;
        this.isExplored = isExplored;
        this.normal = normal;
    }
}
