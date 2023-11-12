using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewYOffSet = .6f;
    [SerializeField]
    private GameObject cellIndicator;
    public GameObject previewObject;


    [SerializeField]
    private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    public Transform[] childTrans;


    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);

    }



    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObject = Instantiate(prefab);

        PreparePreReview(previewObject);

        prepareCursor(size);
        cellIndicator.SetActive(true);


    }

    private void prepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);

            cellIndicator.GetComponentInChildren<Renderer>().material.mainTextureScale = size;
        }
    }

    internal void StartShowingRemovePreview()
    {
        cellIndicator.SetActive(true);
        prepareCursor(Vector2Int.one);
        ApplyFeedbackToCursor(false);
    }

    private void PreparePreReview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }

    }
    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        if (previewObject != null)
            Destroy(previewObject);

    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        if (previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity);
        }

        MoveCursor(position);

        ApplyFeedbackToCursor(validity);


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //((previewObject.rotation.eulerAngles.z + 540) % 360) - 180;
            //float rotY = previewObject.transform.eulerAngles.y;
            //previewObject.transform.rotation = Quaternion.AngleAxis(rotY + 90, Vector3.up);

            /*Transform[] */
            childTrans = previewObject.GetComponentsInChildren<Transform>();
            float rotY = childTrans[1].transform.eulerAngles.y;


            Vector3 camEuler = childTrans[1].rotation.eulerAngles;
            camEuler.y += 90;

            childTrans[1].rotation = Quaternion.Euler(camEuler);

            childTrans[2].rotation = Quaternion.Euler(camEuler);


            // finalllyyyyy


        }
    }
    private void ApplyFeedbackToPreview(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }
    private void ApplyFeedbackToCursor(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        cellIndicator.GetComponentInChildren<SpriteRenderer>().color = c;
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = new Vector3(
            position.x, cellIndicator.transform.position.y, position.z); ;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(
            position.x, position.y + previewYOffSet, position.z);
    }
}
