using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectDatabaseSO database;
    GridData structure;
    ObjectPlacer objectPlacer;

    public PlacementState(int ýD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          ObjectDatabaseSO database,
                          GridData structure,
                          ObjectPlacer objectPlacer)
    {

        ID = ýD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.structure = structure;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = database.objectData.FindIndex(data => data.ID == ID);

        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(database.objectData[selectedObjectIndex].prefab, database.objectData[selectedObjectIndex].Size);
            
        }
        else
            throw new Exception($"no object");

    }
    
    public void EndState()
    {
        previewSystem.StopShowingPreview();
        Array.Clear(previewSystem.childTrans, 0, previewSystem.childTrans.Length);
    }
    public void OnAction(Vector3Int gridPosition)
    {

        bool placementValidity = CheckValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;

       Transform[] objectTrans = database.objectData[selectedObjectIndex].prefab.GetComponentsInChildren<Transform>();
        objectTrans[1].rotation = previewSystem.childTrans[1].rotation;
        objectTrans[2].rotation = previewSystem.childTrans[2].rotation;
        int index = objectPlacer.PlaceObject(database.objectData[selectedObjectIndex].prefab, grid.CellToWorld(gridPosition));


        structure.AddObjectAt(gridPosition, database.objectData[selectedObjectIndex].Size,
            database.objectData[selectedObjectIndex].ID, index);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
       
    }
    private bool CheckValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = structure;

        return selectedData.CanPlacedObjects(gridPosition, database.objectData[selectedObjectIndex].Size);

    }
    public void UpdateState(Vector3Int gridPosition)
    {

        bool placementValidity = CheckValidity(gridPosition, selectedObjectIndex);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);


    }
}
