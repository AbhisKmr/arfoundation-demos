using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchProductHelper : MonoBehaviour
{
    private PlaceObjectsOnPlane placeObjectsOnPlane;
    Vector3 initialScale = Vector3.zero;
    float initialDistance = 0f;
    private AnimatorHandler animatorHandler;
    bool lMove = false;


    private void Start()
    {
        placeObjectsOnPlane = FindObjectOfType<PlaceObjectsOnPlane>();

        foreach (Transform t in transform)
        {
            animatorHandler = t.gameObject.GetComponent<AnimatorHandler>();
            if (animatorHandler != null) break;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            placeObjectsOnPlane.TappedOnObject = true;
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    Debug.Log("Touch Move");
                    onDragFinger();
                    lMove = true;
                    break;

                case TouchPhase.Began:
                    Debug.Log("Touch Began");
                    break;

                case TouchPhase.Ended:
                    Debug.Log("Touch ended");
                    if (!lMove)
                    {
                        onTouchObject();
                    }
                    break;
            }
            lMove = false;
        }
        else
            placeObjectsOnPlane.TappedOnObject = false;
    }

    private void onTouchObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == transform.name)
            {
                if (Input.touchCount == 1) 
                {
                    if (animatorHandler != null)
                    {
                        animatorHandler.clickedOnObject = true;
                    }
                    
                }
            }
        }
    }

    private void onDragFinger()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == transform.name)
            {
                if (Input.touchCount == 1) // to rotate
                {
                    Touch screenTouch = Input.GetTouch(0);
                    if (screenTouch.phase == TouchPhase.Moved)
                    {
                        transform.Rotate(0f, -1 * screenTouch.deltaPosition.x, 0f);
                    }
                }

                else if (false) // to scale (Input.touchCount == 2)
                {
                    Touch zeroTouch = Input.GetTouch(0);
                    Touch oneTouch = Input.GetTouch(1);

                    if (zeroTouch.phase == TouchPhase.Ended || zeroTouch.phase == TouchPhase.Canceled ||
                        oneTouch.phase == TouchPhase.Ended || oneTouch.phase == TouchPhase.Canceled)
                    {
                        Debug.Log("return");
                        return;
                    }

                    var statmentOne = zeroTouch.phase == TouchPhase.Began || zeroTouch.phase == TouchPhase.Stationary;
                    var statmentTwo = oneTouch.phase == TouchPhase.Began || oneTouch.phase == TouchPhase.Stationary;

                    if (statmentOne || statmentTwo)
                    {
                        initialDistance = Vector2.Distance(zeroTouch.position, oneTouch.position);
                        initialScale = transform.localScale;

                        Debug.Log("dis: " + initialDistance + " :: scale\\" + initialScale);
                    }
                    else
                    {
                        Debug.Log("tap else");

                        float currentDistance = Vector2.Distance(zeroTouch.position, oneTouch.position);

                        if (Mathf.Approximately(initialDistance, 0)) return;

                        var factor = currentDistance / initialDistance;
                        Debug.Log("next:: " + currentDistance + "::" + factor);

                        transform.localScale = initialScale * factor;
                    }
                } 
            }
        }
    }
}
