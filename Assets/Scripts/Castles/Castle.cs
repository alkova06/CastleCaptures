using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Castle
{
    public enum Type
    {
        England,
        Forest,
        Crimea,
        Caravan
    }

    public enum UnitType
    {
        Melle,
        Range,
        UpperRange,
        Helper
    }

    public Castle.Type type;
    public Color Color;
    public bool EnableCastle;
    public bool CanEnableLadder;

    public Dictionary<UnitType,List<Unit>> units;
    public Dictionary<UnitType, int> maxUnitTogether;


    public Castle(Castle.Type type, Color Color, bool EnableCastle, bool CanEnableLadder)
    {
        units = new Dictionary<UnitType, List<Unit>>();
        maxUnitTogether = new Dictionary<UnitType, int>();

        this.type = type;
        this.Color = Color;
        this.EnableCastle = EnableCastle;
        this.CanEnableLadder = CanEnableLadder;
    }
}
