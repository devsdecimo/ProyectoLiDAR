using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    public static MeshController instance;

    public Renderer currentRenderer;
    public Material[] materials;
    public int index;
    public GameObject ToolsPanel;
    public Transform upperHandle;
    public Transform lowerHandel;

    public GameObject MaterialPanel;

    private void Awake()
    {
        instance = this;
    }

    public void ShowTools()
    {
        ToolsPanel.SetActive(true);
        MaterialPanel.SetActive(true);
    }

    public void HideTools()
    {
        this.upperHandle.localScale = new Vector3(0f, 0f, 0f);
        this.lowerHandel.localScale = new Vector3(0f, 0f, 0f);

        this.upperHandle = null;
        this.lowerHandel = null;

        currentRenderer = null;

        ToolsPanel.SetActive(false);
        MaterialPanel.SetActive(false);
    }

    public void SetRenderer(Renderer value, Transform upperHandle, Transform lowerHandel)
    {
        this.upperHandle = upperHandle;
        this.lowerHandel = lowerHandel;

        this.upperHandle.localScale = new Vector3(2, 2f,2f);
        this.lowerHandel.localScale = new Vector3(2f, 2f, 2f);

        currentRenderer = value;
    }

    public void NextMaterial()
    {
        if(materials.Length > 0)
        {
            index++;
            if (index >= materials.Length) index = 0;
            currentRenderer.material = materials[index];
        }
    }

    public void PrevMaterial()
    {
        if (materials.Length > 0)
        {
            index++;
            if (index <= -1) index = (materials.Length - 1);
            currentRenderer.material = materials[index];
        }
    }

    public void DeteleObject()
    {
        if(gameObject != null)
        {
            Destroy(currentRenderer.gameObject.transform.parent.gameObject);
            currentRenderer = null;
        }
        HideTools();
    }
}
