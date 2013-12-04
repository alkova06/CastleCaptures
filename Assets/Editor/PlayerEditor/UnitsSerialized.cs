using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("Units")]
public class UnitsSerialized
{
    [XmlArray("UnitSet"), XmlArrayItem("Unit", typeof(UnitSetSerialized))]
    public List<UnitSetSerialized> Sets;

    public UnitsSerialized()
    {
        Sets = new List<UnitSetSerialized>();
    }

    [XmlIgnore]
    public string[] SetsNames;

    public void GenerateGUIContents()
    {
        List<string> tmp = new List<string>();
        foreach(UnitSetSerialized unit in Sets)
            tmp.Add(unit.Type);
        SetsNames = tmp.ToArray();
    }
}

public class UnitSetSerialized
{
    [XmlAttribute("Type")]
    public string Type;
    [XmlAttribute("Life")]
    public int Life;
    [XmlAttribute("Damage")]
    public int Damage;
    [XmlAttribute("AttackSpeed")]
    public float AttackSpeed;
    [XmlAttribute("Evasion")]
    public int Evasion;
    [XmlAttribute("RandomDamage")]
    public int RandomDamage;
    [XmlAttribute("Accuracy")]
    public float Accuracy;
}

