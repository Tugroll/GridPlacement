using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public GameObject deneme;
    private Vector3 lastPosition;
    [SerializeField]
    Camera mainCamera;
    public LayerMask groundMask;

    public event Action OnClick, OnExit;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();

        
        


    }

  
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();


    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            lastPosition = hit.point;
           
        }
        return lastPosition;
    }
  
}
