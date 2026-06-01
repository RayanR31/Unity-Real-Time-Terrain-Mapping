using UnityEngine;

public class RuntimeMapSystem : MonoBehaviour
{
    public int width = 102;
    public int height = 102;
    [SerializeField] private ProbeScanner[] Scanner;
    public Cell[,] RuntimeMap ;
    
    private float timer = 0;

    private void Start()
    {
        RuntimeMap = new Cell[width, height];
        
        for (int i = 0; i < RuntimeMap.GetLength(0); i++)
        {
            for (int j = 0; j < RuntimeMap.GetLength(1); j++)
            {
                RuntimeMap[i, j] = new Cell(new Vector2Int(i,j) ,Vector3.zero, false , Vector3.zero ); 
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= 0.1f)
        {
            AnalyseMap();
            timer = 0;
        }
    }

    private void AnalyseMap()
    {
        for (int i = 0; i < Scanner.Length; i++)
        {
            for (int j = 0; j < Scanner[i].GetScanData.Cells.Count; j++)
            {
                bool isLimit = Scanner[i].GetScanData.Cells[j].cellID.x >= 0
                               && Scanner[i].GetScanData.Cells[j].cellID.x < RuntimeMap.GetLength(0)
                               && Scanner[i].GetScanData.Cells[j].cellID.y >= 0
                               && Scanner[i].GetScanData.Cells[j].cellID.y < RuntimeMap.GetLength(1);

                if (isLimit)
                {
                    RuntimeMap[Scanner[i].GetScanData.Cells[j].cellID.x, Scanner[i].GetScanData.Cells[j].cellID.y] = Scanner[i].GetScanData.Cells[j];
                }

            }
            
            Scanner[i].GetScanData.Cells.Clear();
            Scanner[i].GetScanData.ScannedCellIDs.Clear();
        }
    }
}
