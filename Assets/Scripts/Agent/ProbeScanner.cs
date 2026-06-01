using UnityEngine;

public class ProbeScanner : MonoBehaviour
{
    private readonly ScanData scanData = new ScanData();
    public ScanData GetScanData => scanData;
    [SerializeField] private int ringCount = 6;
    [SerializeField] private int segments = 64;
    [SerializeField] private float spacing = 1f;
    [SerializeField] private float rayHeight = 3f;
    [SerializeField] private float rayDistance = 6f;
    [SerializeField] private bool drawDebug;

    private Vector3 groundNormal;
    private Vector3 pointGround;
    void Update()
    {
        AnalyseGround(-transform.up);
    }

    private void AnalyseGround(Vector3 pos)
    {
        CalculInformationGround(pos);
        DrawAdaptiveRings(pointGround, groundMask: 1 << 0);
    }

    private void CalculInformationGround(Vector3 pos)
    {
        if (Physics.Raycast(transform.position, pos, out RaycastHit hit, rayDistance))
        {
            groundNormal = hit.normal;
            pointGround = hit.point;
        }
        if(drawDebug)
            Debug.DrawRay(transform.position, pos * rayDistance, Color.red);
    }
    private void DrawAdaptiveRings(Vector3 center, LayerMask groundMask = default)
    {
        Vector3 axisA = Vector3.ProjectOnPlane(Vector3.right, groundNormal).normalized;

        if (axisA.sqrMagnitude < 0.001f)
            axisA = Vector3.ProjectOnPlane(Vector3.forward, groundNormal).normalized;

        Vector3 axisB = Vector3.Cross(groundNormal, axisA).normalized;

        for (int r = 1; r <= ringCount; r++)
        {
            float radius = r * spacing;

            Vector3? previousPoint = null;

            for (int s = 0; s <= segments; s++)
            {
                float angle = s * Mathf.PI * 2f / segments;

                Vector3 dir =
                    axisA * Mathf.Cos(angle) +
                    axisB * Mathf.Sin(angle);

                Vector3 theoreticalPoint = center + dir * radius;

                Vector3 rayOrigin = theoreticalPoint + groundNormal * rayHeight;
                Vector3 rayDir = -groundNormal;

                if (Physics.Raycast(rayOrigin, rayDir, out RaycastHit hit, rayDistance, groundMask))
                {
                    Vector3 projectedPoint = hit.point;
                    Vector2Int cellID = new Vector2Int((int)projectedPoint.x, (int)projectedPoint.z);
                    
                    if (scanData.ScannedCellIDs.Add(cellID) == true)
                    {
                        Cell cell = new Cell(cellID, projectedPoint , true , hit.normal);
                        scanData.Cells.Add(cell);
                    }

                    if (previousPoint.HasValue)
                    {
                       /* float t = hit.distance / 6f;
                        Color c = Color.Lerp(Color.green, Color.red, t);*/
                       if(drawDebug)
                        Debug.DrawLine(previousPoint.Value, projectedPoint, Color.red);

                    }

                    previousPoint = projectedPoint;
                }
                else
                {
                    previousPoint = null;
                }
            }
        }
    }
}