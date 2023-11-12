using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectData;
}

[Serializable]
public class ObjectData
{
    [field: SerializeField]

    public string Name;
    [field: SerializeField]

    public int ID;
    [field: SerializeField]

    public Vector2Int Size;
    [field: SerializeField]

    public GameObject prefab;
  
}