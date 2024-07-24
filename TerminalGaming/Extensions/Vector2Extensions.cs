using UnityEngine;

namespace TerminalGaming.Extensions;

public static class Vector2Extensions
{
    public static Vector2 Rotated(this Vector2 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        return new Vector2(
            (cos * vector.x) - (sin * vector.y),
            (sin * vector.x) + (cos * vector.y)
        );
    }
}
