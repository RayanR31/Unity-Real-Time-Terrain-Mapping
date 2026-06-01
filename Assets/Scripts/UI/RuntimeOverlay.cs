using TMPro;
using UnityEngine;

public class RuntimeOverlay : MonoBehaviour
{
    [SerializeField] private TMP_Text overlayText;
    [SerializeField] private RuntimeMapSystem runtimeMapSystem;
    [SerializeField] private ProbeScanner probeScanner;

    private float timer;
    private float fps;

    private void Update()
    {
        timer += Time.deltaTime;

        fps = 1f / Time.deltaTime;

        if (timer >= 0.25f)
        {
            RefreshUI();
            timer = 0;
        }
    }

    private void RefreshUI()
    {
        int exploredCells = 0;
        float maxSlope = 0f;

        for (int i = 0; i < runtimeMapSystem.RuntimeMap.GetLength(0); i++)
        {
            for (int j = 0; j < runtimeMapSystem.RuntimeMap.GetLength(1); j++)
            {
                Cell cell = runtimeMapSystem.RuntimeMap[i, j];

                if (!cell.isExplored)
                    continue;

                exploredCells++;

                float angle = Vector3.Angle(cell.normal, Vector3.up);

                if (angle > maxSlope)
                    maxSlope = angle;
            }
        }

        int totalCells =
            runtimeMapSystem.RuntimeMap.GetLength(0) *
            runtimeMapSystem.RuntimeMap.GetLength(1);

        float coverage =
            exploredCells / (float)totalCells * 100f;

        overlayText.text =
            $"REAL-TIME MAPPING SYSTEM\n\n" +
            $"Coverage     : {coverage:0.0}%\n" +
            $"Mapped Cells : {exploredCells}\n" +
            $"Max Slope    : {maxSlope:0.0}°\n" +
            $"Scan Hits    : {probeScanner.GetScanData.Cells.Count}\n" +
            $"FPS          : {fps:0}";
    }
}