using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> placedObject = new();

    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        placedObject.Add(newObject);

        return placedObject.Count - 1;
    }

    internal void RemoveObject(int gameObjectIndex)
    {
        if (placedObject.Count <= gameObjectIndex || placedObject[gameObjectIndex] == null)
            return;
        Destroy(placedObject[gameObjectIndex]);
        placedObject[gameObjectIndex] = null;
    }
}
