using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drageable : MonoBehaviour
{
    private float distx;
    private float distz;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        Vector3 v3;

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        try
        {
            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10f, layerMask))
                {
                    if (hit.collider.tag == "cube")
                    {
                        toDrag = hit.transform;
                        distx = hit.transform.position.x - Camera.main.transform.position.x;
                        
                        distz = hit.transform.position.z - Camera.main.transform.position.z;
                        v3 = new Vector3(distz, pos.y, distz);
                        v3 = Camera.main.ScreenToWorldPoint(v3);
                        offset = toDrag.position - v3;
                        dragging = true;
                    }
                }
            }

            if (dragging && touch.phase == TouchPhase.Moved)
            {
                v3 = new Vector3(distx, Input.mousePosition.y, distz);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                toDrag.position = v3 + offset;
            }

            if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                dragging = false;
            }
        }
        catch(System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
