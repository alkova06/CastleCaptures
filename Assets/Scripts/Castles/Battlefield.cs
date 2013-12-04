using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Battlefield : MonoBehaviour
{
    public Castle.Type battleType;
    Castle battleParams;

    Dictionary<Castle.UnitType, List<Unit>> enemieUnitStore;
    Dictionary<Unit.UnitType, GameObject[]> enemieUnitGameObject;

    void Start()
    {
        enemieUnitStore = new Dictionary<Castle.UnitType, List<Unit>>();
        enemieUnitGameObject = new Dictionary<Unit.UnitType, GameObject[]>();
        battleParams = CastleContainer.Instance.GetCastle(battleType);

        int count = Enum.GetNames(typeof(Castle.UnitType)).Length;
        for (int i = 0; i < count; i++)
        {
            Castle.UnitType unitType = (Castle.UnitType)i;

            if (battleParams.units.ContainsKey(unitType))
            {
                enemieUnitStore.Add(unitType, new List<Unit>());

                //Debug.Log(battleParams.units[unitType][0].Type);
                for (int k = 0; k < battleParams.units[unitType].Count; k++)
                {
                    if (!enemieUnitGameObject.ContainsKey(battleParams.units[unitType][k].Type))
                    {
                        enemieUnitGameObject.Add(battleParams.units[unitType][k].Type, new GameObject[battleParams.maxUnitTogether[unitType]]);
                        for (int j = 0; j < battleParams.maxUnitTogether[unitType]; j++)
                            enemieUnitGameObject[battleParams.units[unitType][k].Type][j] = (GameObject)Instantiate(battleParams.units[unitType][k].GameObject, new Vector3(-10,0,0), Quaternion.identity);
                    }
                    enemieUnitStore[unitType].Add(battleParams.units[unitType][k]);
                }
            }
        }

    }

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 0.01f);

        //    if (hit.collider != null)
        //        if (hit.collider.gameObject == enemyMelleGO[0])
        //            enemyMelle[0].Select();
        //}
    }
}