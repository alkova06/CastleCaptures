using UnityEngine;
using System.Collections;

public class GroundPoint : MonoBehaviour
{
    public enum PointType
    {
        LeftUp,
        LeftDown,
        RightUp,
        RightDown
    }

    public PointType pointType;
}
