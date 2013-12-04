using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

public class UnitsEditor : EditorWindow
{
    [MenuItem("Engine Editor/Unit Editor")]
    static void Init()
    {
        window = (UnitsEditor)EditorWindow.GetWindow(typeof(UnitsEditor));
        window.title = "Player editor";
        window.autoRepaintOnSceneChange = true;
        LoadFromXML();
    }
    static UnitsEditor window;

    static XmlDocument doc ;
    static UnitsSerialized unitsSet;

    static void LoadFromXML()
    {
        doc = new XmlDocument();
        TextAsset text = new TextAsset();
        text = (TextAsset)Resources.Load("UnitsData", typeof(TextAsset));
        doc.LoadXml(text.text);

        XmlSerializer serializer = new XmlSerializer(typeof(UnitsSerialized));
        XmlReader reader = new XmlNodeReader(doc.ChildNodes[1]);
        unitsSet = (UnitsSerialized)serializer.Deserialize(reader);

        unitsSet.GenerateGUIContents();
    }

    void UnitGUI()
    {
        GUILayout.BeginVertical(GUI.skin.box);

        unitsSet.Sets[selectedSet].Life = EditorGUILayout.IntField("Life :", unitsSet.Sets[selectedSet].Life);
        unitsSet.Sets[selectedSet].Damage = EditorGUILayout.IntField("Damage :", unitsSet.Sets[selectedSet].Damage);
        unitsSet.Sets[selectedSet].AttackSpeed = EditorGUILayout.FloatField("Attack Speed :", unitsSet.Sets[selectedSet].AttackSpeed);
        unitsSet.Sets[selectedSet].Evasion = EditorGUILayout.IntField("Evasion :", unitsSet.Sets[selectedSet].Evasion);
        unitsSet.Sets[selectedSet].RandomDamage = EditorGUILayout.IntField("Random Damage :", unitsSet.Sets[selectedSet].RandomDamage);
        unitsSet.Sets[selectedSet].Accuracy = EditorGUILayout.FloatField("Accuracy :", unitsSet.Sets[selectedSet].Accuracy);

        GUILayout.EndVertical();
    }

    int selectedSet;
    void OnGUI()
    {
        if (unitsSet == null)
            LoadFromXML();

        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Space(1);

        if (GUILayout.Button("Reload XML"))
            LoadFromXML();

        selectedSet = EditorGUILayout.Popup("Units :", selectedSet, unitsSet.SetsNames);
        UnitGUI();

        if (GUILayout.Button("Save"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UnitsSerialized));
            using (FileStream fs = new FileStream(Application.dataPath + @"/Resources/UnitsData.xml", FileMode.Create))
                serializer.Serialize(fs, unitsSet);
        }

        GUILayout.EndVertical();
    }
}
