using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour
{
    public GameObject[] Prefabs;

    void Awake()
    {
        for (int i = 0; i < Prefabs.Length; i++)
            Instantiate(Prefabs[i]);
    }
}
