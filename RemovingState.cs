using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int selectedObjectIndex = -1;
    
    Grid grid;
    PreviewSystem previewSystem;
    ObjectDatabaseSO database;
    GridData structure;
    ObjectPlacer objectPlacer;

    public RemovingState(
                   
                         Grid grid,
                         PreviewSystem previewSystem,
                         ObjectDatabaseSO database,
                         GridData structure,
                         ObjectPlacer objectPlacer)
    {
       
      
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.structure = structure;
        this.objectPlacer = objectPlacer;

        
        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
      
        if (structure == null)
        {
            Debug.Log("You did it");
        }
        else
        {
            selectedObjectIndex = structure.GetRepresentationIndex(gridPosition);
            if (selectedObjectIndex == -1)
                return;

            structure.RemoveObject(gridPosition);
            objectPlacer.RemoveObject(selectedObjectIndex);
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckValidity(gridPosition));
    }
    private bool CheckValidity(Vector3Int gridPosition)
    {
      

        return structure.CanPlacedObjects(gridPosition, Vector2Int.one);

    }
    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckValidity(gridPosition);
        previewSystem.UpdatePosition(gridPosition,validity);
    }

   
}
