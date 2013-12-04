using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground : MonoBehaviour
{
    static Ground instance;
    public static Ground Instance { get { return instance; } }

    public RectangleBounding GroundRect;

    void Awake()
    {
        instance = this;
        GetGround();
    }

    void GetGround()
    {
        GroundPoint[] points = GetComponentsInChildren<GroundPoint>();
        Vector3 leftUp = Vector3.zero, leftDown = Vector3.zero, rightUp = Vector3.zero, rightDown = Vector3.zero;

        for (int i = 0; i < points.Length; i++)
            switch (points[i].pointType)
            {
                case GroundPoint.PointType.LeftDown:
                    leftDown = points[i].transform.position;
                    break;
                case GroundPoint.PointType.LeftUp:
                    leftUp = points[i].transform.position;
                    break;
                case GroundPoint.PointType.RightDown:
                    rightDown = points[i].transform.position;
                    break;
                case GroundPoint.PointType.RightUp:
                    rightUp = points[i].transform.position;
                    break;
            }

        GroundRect = new RectangleBounding()
        {
            leftDown = leftDown,
            leftUp = leftUp,
            rightDown = rightDown,
            rightUp = rightUp
        };
    }
}
