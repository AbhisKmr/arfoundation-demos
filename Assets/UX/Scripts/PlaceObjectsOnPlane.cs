using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    List<GameObject> m_PlacedPrefab;
    [SerializeField]
    private GameObject bottomBar;
    [SerializeField]
    private GameObject placementLocator;

    int placementIndex = 0;
    public int PlacementIntex
    {
        get { return placementIndex; }
        set { placementIndex = value; }
    }

    public List<GameObject> placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    public GameObject spawnedObject { get; private set; }

    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    [SerializeField]
    bool m_CanReposition = true;

    public bool canReposition
    {
        get => m_CanReposition;
        set => m_CanReposition = value;
    }

    private bool isTappedOnObject = false;

    public bool TappedOnObject
    {
        get => isTappedOnObject;
        set => isTappedOnObject = value;
    }

    bool isBottomSheetActive = false;

    public bool BottomSheetActive
    {
        get => isBottomSheetActive;
        set => isBottomSheetActive = value;
    }

    bool isReadyForPlaceItem = false;
    Vector3 touchPosWorld;
    TouchPhase touchPhase = TouchPhase.Ended;

    public bool readyForPlaceItem
    {
        get => isReadyForPlaceItem;
        set => isReadyForPlaceItem = value;
    }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        placementLocator.SetActive(false);
    }

    void Update()
    {
        bool isPlanDetected = m_RaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), s_Hits, TrackableType.PlaneWithinPolygon);

        if (isPlanDetected)
        {
            bottomBar.SetActive(true);

            if (isReadyForPlaceItem)
            {
                Pose hitPose = s_Hits[0].pose;
                placementLocator.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                placementLocator.SetActive(true);

                if (!isTappedOnObject)
                {
                    if (!isBottomSheetActive)
                        TouchOnLocator(hitPose);
                }
            }
        }
    }

    private void TouchOnLocator(Pose hitPose)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                    {
                        spawnedObject = Instantiate(m_PlacedPrefab[placementIndex], hitPose.position, hitPose.rotation);
                        spawnedObject.name = m_PlacedPrefab[placementIndex].name;
                        m_NumberOfPlacedObjects++;
                    }

                    isReadyForPlaceItem = false;
                    placementLocator.SetActive(false);
                    //else
                    //{
                    //    //if (m_CanReposition)
                    //    //{
                    //    //    spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    //    //}
                    //}

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }
    }




















    /*
     *  
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            placementIndex %= 3;

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                    {
                        spawnedObject = Instantiate(m_PlacedPrefab[placementIndex], hitPose.position, hitPose.rotation);

                        m_NumberOfPlacedObjects++;
                        placementIndex++;
                    }
                    else
                    {
                        if (m_CanReposition)
                        {
                            spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        }
                    }

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }
     */
}
