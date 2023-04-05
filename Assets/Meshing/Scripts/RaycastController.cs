using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public Renderer currentRenderer;
    public Transform UpperHandle;
    public Transform LowerHandle;

    private void Start()
    {
        currentRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        //layerMask = ~layerMask;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 20f, layerMask))
        {
            try
            {
                if (hit.collider.gameObject == this.gameObject && Input.GetMouseButtonDown(0))
                {
                    print(hit.collider.name);
                    MeshController.instance.SetRenderer(currentRenderer, UpperHandle, LowerHandle);
                    MeshController.instance.ShowTools();
                }
            }
            catch(System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        CalculateScale();
    }

    private void CalculateScale()
    {
        Vector3 P1 = UpperHandle.localPosition;
        Vector3 P2 = LowerHandle.localPosition;

        transform.localPosition = (P1 + P2) / 2;
        transform.localScale =
            new Vector3(
                ((Mathf.Abs(UpperHandle.localPosition.x) + Mathf.Abs(LowerHandle.localPosition.x)) / 2f) / 5f,
                -1f,
                ((Mathf.Abs(UpperHandle.localPosition.z) + Mathf.Abs(LowerHandle.localPosition.z)) / 2f) / 5f
            );
    }
}
