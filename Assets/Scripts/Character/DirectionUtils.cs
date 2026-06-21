using UnityEngine;

public static class DirectionUtils
{
    private const float HysteresisBuffer = 0.1f;

    public static Vector2 EvaluateDirection(Vector2 currentDirection, Vector2 previousDirection)
    {
        if (currentDirection == Vector2.zero)
        {
            return Vector2.zero;
        }

        float absX = Mathf.Abs(currentDirection.x);
        float absY = Mathf.Abs(currentDirection.y);

        if (previousDirection == Vector2.right || previousDirection == Vector2.left)
        {
            absY -= HysteresisBuffer;
        }
        else if (previousDirection == Vector2.up || previousDirection == Vector2.down)
        {
            absX -= HysteresisBuffer;
        }

        if (absX >= absY)
        {
            return currentDirection.x > 0 ? Vector2.right : Vector2.left;
        }

        return currentDirection.y > 0 ? Vector2.up : Vector2.down;
    }
}