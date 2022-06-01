using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchProductHelper : MonoBehaviour
{
    private PlaceObjectsOnPlane placeObjectsOnPlane;

    private void Start()
    {
        placeObjectsOnPlane = FindObjectOfType<PlaceObjectsOnPlane>();
    }

    void Update()
    {
        if (Input.touchCount > 0 &&
            (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Moved))
        {
            placeObjectsOnPlane.TappedOnObject = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == transform.name)
                {
                    if (Input.touchCount == 1)
                    {
                        Touch screenTouch = Input.GetTouch(0);
                        if (screenTouch.phase == TouchPhase.Moved)
                        {
                            transform.Rotate(0f, -1 * screenTouch.deltaPosition.x, 0f);
                            //foreach(Transform c in transform)
                            //{
                            //    c.gameObject.transform.Rotate(0f, -1 * screenTouch.deltaPosition.x, 0f);
                            //}
                        }
                    }
                }
            }
        }
        else
            placeObjectsOnPlane.TappedOnObject = false;
    }
}
