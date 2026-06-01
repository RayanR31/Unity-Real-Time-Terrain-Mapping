using UnityEngine;

public class DebugDrawLine
{
    public static void DrawDebugSphere(Vector3 center, float radius, Color color)
    {
        DrawCircle(center, radius, Vector3.right, Vector3.up, color);
        DrawCircle(center, radius, Vector3.right, Vector3.forward, color);
        DrawCircle(center, radius, Vector3.up, Vector3.forward, color);
    }

    public static void DrawCircle(Vector3 center, float radius, Vector3 axisA, Vector3 axisB, Color color)
    {
        int segments = 20;

        Vector3 prev = center + axisA * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;

            Vector3 next =
                center +
                (axisA * Mathf.Cos(angle) +
                 axisB * Mathf.Sin(angle)) * radius;

            Debug.DrawLine(prev, next, color);

            prev = next;
        }
    }
}
