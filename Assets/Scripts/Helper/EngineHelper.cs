using UnityEngine;
using System.Collections;

public class RectangleBounding
{
    public Vector2 leftUp;
    public Vector2 rightUp;
    public Vector2 rightDown;
    public Vector2 leftDown;
}

public class EngineHelper
{
    public static Color StringToColor(string color)
    {
        string[] chanels = color.Split(',');
        Color outColor = new Color(float.Parse(chanels[0]), float.Parse(chanels[1]), float.Parse(chanels[2]));
        return outColor;
    }

    public static string ColorToString(Color color)
    {
        return color.r.ToString() + "," + color.g.ToString() + "," + color.b.ToString();
    }

    public Vector2 Bezier2D(Vector2 startPosition, Vector2 middlePosition, Vector2 endPosition, float t)
    {
        Vector2 one = Vector2.Lerp(startPosition, middlePosition, t);
        Vector2 two = Vector2.Lerp(middlePosition, endPosition, t);
        return Vector2.Lerp(one, two, t);
    }
    public static Vector3 PointOnRectangle(RectangleBounding rect)
    {
        float amt = Random.RandomRange(0.0f, 1.0f);

        Vector2 point1;
        point1.x = Mathf.Lerp(rect.leftUp.x, rect.rightUp.x, amt);
        point1.y = Mathf.Lerp(rect.leftUp.y, rect.rightUp.y, amt);

        amt = Random.Range(0.0f, 1.0f);
        Vector2 point2;
        point2.x = Mathf.Lerp(rect.leftDown.x, rect.rightDown.x, amt);
        point2.y = Mathf.Lerp(rect.leftDown.y, rect.rightDown.y, amt);

        amt = Random.Range(0.0f, 1.0f);

        return new Vector3(Mathf.Lerp(point1.x, point2.x, amt), Mathf.Lerp(point1.y, point2.y, amt), 0);
    }
}
