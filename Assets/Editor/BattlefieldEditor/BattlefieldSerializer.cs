using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("Battlefield")]
public class BattlefieldSerializer
{
    [XmlArray("Castles"), XmlArrayItem("Castle", typeof(CastlesSetSerialized))]
    public List<CastlesSetSerialized> castleSet;
    
    [XmlIgnore]
    public string[] castleNames;
    public void GenerateGUIContent()
    {
        List<string> tmp = new List<string>();
        foreach (CastlesSetSerialized castle in castleSet)
            tmp.Add(castle.Type);
        castleNames = tmp.ToArray();
    }
}

public class CastlesSetSerialized
{
    [XmlAttribute("Type")]
    public string Type;
    [XmlAttribute("CastleEnable")]
    public string CastleEnable;
    [XmlAttribute("CanLadderEnable")]
    public string CanLadderEnable;
    [XmlAttribute("Color")]
    public string Color;

    [XmlArray("UnitsType"), XmlArrayItem("UnitType", typeof(UnitTypeSetSerialized))]
    public List<UnitTypeSetSerialized> unitsTypeSet;
    
    public CastlesSetSerialized()
    {
        unitsTypeSet = new List<UnitTypeSetSerialized>();
    }
}

public class UnitTypeSetSerialized
{
    [XmlAttribute("Type")]
    public string Type;
    [XmlAttribute("MaxCountTogether")]
    public int MaxCountTogether;

    [XmlArray("Units"), XmlArrayItem("Unit", typeof(UnitsSetSerialized))]
    public List<UnitsSetSerialized> unitSet;

    public UnitTypeSetSerialized()
    {
        unitSet = new List<UnitsSetSerialized>();
    }
}

public class UnitsSetSerialized
{
    [XmlAttribute("Type")]
    public string Type;
    [XmlAttribute("Count")]
    public int Count;
}