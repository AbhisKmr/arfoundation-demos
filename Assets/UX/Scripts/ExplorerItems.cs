using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorerItems : MonoBehaviour
{
    [SerializeField]
    private GameObject bottomSheet;

    [SerializeField]
    private GameObject template;

    [SerializeField]
    private List<Sprite> img = new List<Sprite>();

    private Vector3 initialSheetpos = Vector3.zero;
    private Vector3 hideSheetPos = new Vector3(585.00f, -70.72f, 0.00f);
    private float speed = 3f;
    private PlaceObjectsOnPlane placeObjectsOnPlane;


    void Start()
    {
        bottomSheet.SetActive(false);
        placeObjectsOnPlane = transform.GetComponent<PlaceObjectsOnPlane>();
        List<GameObject> itemToShow = placeObjectsOnPlane.placedPrefab;

        for (int i = 0; i < itemToShow.Count; i++)
        {
            var t = Instantiate(template, bottomSheet.transform.transform);
            t.name = i.ToString();
            t.transform.GetChild(0).GetComponent<Image>().sprite = img[i];

            t.GetComponent<Button>().onClick.AddListener(delegate
            {
                OnItemClicked(int.Parse(t.name));
            });
        }

        //initialSheetpos = bottomSheet.transform.position;
        //bottomSheet.transform.position = hideSheetPos;
    }

    public void explorerItems()
    {
        SheetState(bottomSheet.activeSelf);

        Debug.Log("You have clicked the bottomsheet:// :)");
    }

    void OnItemClicked(int s)
    {
        placeObjectsOnPlane.readyForPlaceItem = true;
        placeObjectsOnPlane.PlacementIntex = s;
        bottomSheet.SetActive(false);
        Debug.Log(s);
    }

    private void SheetState(bool b)
    {
        //bottomSheet.transform.position = b ? hideSheetPos: initialSheetpos;
        placeObjectsOnPlane.BottomSheetActive = b;
        bottomSheet.SetActive(!b);
    }
}