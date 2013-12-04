using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

public class BattlefieldEditor : UnitsEditor
{
    [MenuItem("Engine Editor/Battlefield Editor")]
    static void Init()
    {
        window = (BattlefieldEditor)EditorWindow.GetWindow(typeof(BattlefieldEditor));
        window.title = "Battlefield Editor";
        window.autoRepaintOnSceneChange = true;
        LoadFromXML();
    }

    static BattlefieldEditor window;

    static XmlDocument doc;
    static BattlefieldSerializer battlefieldSet;

    static string fileName = "BattlefieldData";

    static string[] unitTypeNames = { "Melle", "Range", "UpperRange", "Helper" };
    static string[][] unitNames = { new string[] { "Knight", "Bandit" }, new string[] { "Archer" }, new string[] { "Archer" }, new string[] { "Helper" } };
    static string[] boolNames = { "false", "true" };
    static void LoadFromXML()
    {
        doc = new XmlDocument();
        TextAsset text = new TextAsset();
        text = (TextAsset)Resources.Load(fileName, typeof(BattlefieldSerializer));
        doc.LoadXml(text.text);
        Debug.Log(text.text);
        XmlSerializer serializer = new XmlSerializer(typeof(BattlefieldSerializer));
        XmlReader reader = new XmlNodeReader(doc.ChildNodes[1]);
        battlefieldSet = (BattlefieldSerializer)serializer.Deserialize(reader);

        battlefieldSet.GenerateGUIContent();
    }

    int selectedSet;
    int lastSelectedSet;
    int selectedUnit;
    int selctedUnitType;

    int selectedCastleEnable;
    int selectedCanLadderEnable;

    void OnGUI()
    {
        if (battlefieldSet == null)
            LoadFromXML();

        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Space(1);

        if (GUILayout.Button("Reload XML"))
            LoadFromXML();

        GUILayout.BeginHorizontal(GUI.skin.box);
        lastSelectedSet = selectedSet;
        selectedSet = EditorGUILayout.Popup("Castle :", selectedSet, battlefieldSet.castleNames);
        battlefieldSet.castleSet[selectedSet].Color = EngineHelper.ColorToString(EditorGUILayout.ColorField("Color :", EngineHelper.StringToColor(battlefieldSet.castleSet[selectedSet].Color)));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal(GUI.skin.box);
        selectedCastleEnable = EditorGUILayout.Popup("Castle Enable :", selectedCastleEnable, boolNames);
        battlefieldSet.castleSet[selectedSet].CastleEnable = boolNames[selectedCastleEnable];
        selectedCanLadderEnable = EditorGUILayout.Popup("Can enable ladder :", selectedCanLadderEnable, boolNames);
        battlefieldSet.castleSet[selectedSet].CanLadderEnable = boolNames[selectedCanLadderEnable];
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.BeginHorizontal(GUI.skin.box);
        for (int i = 0; i < battlefieldSet.castleSet[selectedSet].unitsTypeSet.Count; i++)
        {
            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.LabelField("Type :", battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].Type);
            battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].MaxCountTogether = EditorGUILayout.IntField("Max count together :", battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].MaxCountTogether);
            GUILayout.EndHorizontal();
            for (int j = 0; j < battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].unitSet.Count; j++)
            {
                GUILayout.BeginHorizontal(GUI.skin.box);
                EditorGUILayout.LabelField(battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].unitSet[j].Type);
                battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].unitSet[j].Count = EditorGUILayout.IntField(battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].unitSet[j].Count);
                if (GUILayout.Button("Delete"))
                {
                    battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].unitSet.RemoveAt(j);
                    j--;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            if (i == 1)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(GUI.skin.box);
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Space(2);

        lastTmpSelectedType = tmpSelectedType;
        tmpSelectedType = EditorGUILayout.Popup("Type :", tmpSelectedType, unitTypeNames);
        tmpUnit.Type = unitTypeNames[tmpSelectedType];
        if (lastTmpSelectedType != tmpSelectedType)
            tmpSelectedUnitType = 0;
        tmpSelectedUnitType = EditorGUILayout.Popup("Unit type :", tmpSelectedUnitType, unitNames[tmpSelectedType]);
        tmpUnit.UnitType = unitNames[tmpSelectedType][tmpSelectedUnitType];
        tmpUnit.Count = EditorGUILayout.IntField("Count :", tmpUnit.Count);
        if (GUILayout.Button("Add"))
        {
            bool added = false;
            for (int i = 0; i < battlefieldSet.castleSet[selectedSet].unitsTypeSet.Count; i++)
                if (string.Equals(battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].Type, tmpUnit.Type))
                {
                    UnitsSetSerialized tmpSetSerialized = new UnitsSetSerialized();
                    tmpSetSerialized.Type = tmpUnit.UnitType;
                    tmpSetSerialized.Count = tmpUnit.Count;
                    battlefieldSet.castleSet[selectedSet].unitsTypeSet[i].unitSet.Add(tmpSetSerialized);
                    added = true;
                }
            if (!added)
            {
                UnitTypeSetSerialized tmpTypeSetSerialized = new UnitTypeSetSerialized();
                tmpTypeSetSerialized.MaxCountTogether = 1;
                tmpTypeSetSerialized.Type = tmpUnit.Type;

                UnitsSetSerialized tmpSetSerialized = new UnitsSetSerialized();
                tmpSetSerialized.Type = tmpUnit.UnitType;
                tmpSetSerialized.Count = tmpUnit.Count;

                tmpTypeSetSerialized.unitSet.Add(tmpSetSerialized);

                battlefieldSet.castleSet[selectedSet].unitsTypeSet.Add(tmpTypeSetSerialized);
            }
        }
        GUILayout.EndVertical();

        if (GUILayout.Button("Save"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BattlefieldSerializer));
            using (FileStream fs = new FileStream(Application.dataPath + @"/Resources/" + fileName + ".xml", FileMode.Create))
                serializer.Serialize(fs, battlefieldSet);
        }
    }

    TmpUnit tmpUnit = new TmpUnit();
    int tmpSelectedType;
    int lastTmpSelectedType;
    int tmpSelectedUnitType;
}
class TmpUnit
{
    public string Type;
    public string UnitType;
    public int Count;
}
