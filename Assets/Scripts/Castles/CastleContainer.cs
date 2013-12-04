using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class CastleContainer : MonoBehaviour
{
    List<Castle> castles;

    static CastleContainer instance;
    public static CastleContainer Instance { get { return instance; } }

    public static string BattlefieldDataFileName = "BattlefieldData";

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        LoadXML();
    }

    void Start()
    {
    }

    void LoadXML()
    {
        castles = new List<Castle>();
        XmlDocument doc = new XmlDocument();
        TextAsset text = new TextAsset();
        text = (TextAsset)Resources.Load(BattlefieldDataFileName, typeof(TextAsset));
        doc.LoadXml(text.text);

        foreach (XmlNode node in doc.ChildNodes[1].ChildNodes[0].ChildNodes)
        {
            Castle tmp = new Castle
                (
                    (Castle.Type)Enum.Parse(typeof(Castle.Type), node.Attributes["Type"].Value),
                    EngineHelper.StringToColor(node.Attributes["Color"].Value),
                    bool.Parse(node.Attributes["CastleEnable"].Value),
                    bool.Parse(node.Attributes["CanLadderEnable"].Value)
                );
            foreach (XmlNode unitTypeNode in node.ChildNodes[0].ChildNodes)
            {
                Castle.UnitType unitType = (Castle.UnitType)Enum.Parse(typeof(Castle.UnitType), unitTypeNode.Attributes["Type"].Value);
                tmp.maxUnitTogether.Add(unitType, int.Parse(unitTypeNode.Attributes["MaxCountTogether"].Value));
                tmp.units.Add(unitType, new List<Unit>());

                foreach (XmlNode unitNode in unitTypeNode.ChildNodes[0].ChildNodes)
                {
                    int count = int.Parse(unitNode.Attributes["Count"].Value);
                    Unit.UnitType type = (Unit.UnitType)Enum.Parse(typeof(Unit.UnitType), unitNode.Attributes[0].Value);
                    
                    for (int i = 0; i < count; i++)
                        tmp.units[unitType].Add(UnitContainer.Instance.GetUnit(type));
                }
            }
            castles.Add(tmp);
        }
    }

    public Castle GetCastle(Castle.Type type)
    {
        for (int i = 0; i < castles.Count; i++)
            if (castles[i].type == type)
                return castles[i];
        return null;
    }
}
