using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class UnitContainer : MonoBehaviour
{
    public GameObject[] prefubs;

    public string UnitDataFileName = "UnitsData";
    public List<Unit> Units;

    public static UnitContainer Instance { get { return instance; } }
    static UnitContainer instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        LoadUnitData();
    }

    void LoadUnitData()
    {
        Units = new List<Unit>();
        XmlDocument doc = new XmlDocument();
        TextAsset text = new TextAsset();
        text = (TextAsset)Resources.Load(UnitDataFileName, typeof(TextAsset));
        doc.LoadXml(text.text);

        foreach (XmlNode node in doc.ChildNodes[1].ChildNodes[0].ChildNodes)
        {
            Unit.UnitType type = (Unit.UnitType)Enum.Parse(typeof(Unit.UnitType), node.Attributes["Type"].Value);
            Unit unit = new Unit
                (
                type,
                int.Parse(node.Attributes["Life"].Value),
                int.Parse(node.Attributes["Damage"].Value),
                float.Parse(node.Attributes["AttackSpeed"].Value),
                int.Parse(node.Attributes["Evasion"].Value),
                int.Parse(node.Attributes["RandomDamage"].Value),
                int.Parse(node.Attributes["Accuracy"].Value),
                prefubs[(int)type]
                );
            Units.Add(unit);
        }
    }

    public Unit GetUnit(Unit.UnitType type)
    {
        foreach (Unit unit in Units)
            if (unit.Type == type)
                return unit;
        return null;
    }
}
