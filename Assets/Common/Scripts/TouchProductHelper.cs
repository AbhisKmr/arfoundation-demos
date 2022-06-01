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

            Debug.Log("Touch on start:: " + Input.touches[0].phase);
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("name// "+hit.collider.name +"::"+ transform.name);
                if (hit.collider.name == transform.name)
                {
                    if (Input.touchCount == 1)
                    {
                        Touch screenTouch = Input.GetTouch(0);
                        Debug.Log("phase:: " + screenTouch.phase);
                        if (screenTouch.phase == TouchPhase.Moved)
                        {
                            Debug.Log("TouchPhase.Moved");

                            transform.Rotate(0f, -1*screenTouch.deltaPosition.x, 0f);
                        }
                    }
                }
            }
        }
        else
            placeObjectsOnPlane.TappedOnObject = false;
    }
}
