using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SheetDataObject
{
    public GameObject products;
    public RawImage representativeImage;
    public string name;
}

public class ExplorerItems : MonoBehaviour
{
    [SerializeField]
    private GameObject bottomSheet;

    [SerializeField]
    private GameObject template;

    [SerializeField]
    private List<Sprite> img = new List<Sprite>();

    private PlaceObjectsOnPlane placeObjectsOnPlane;
    private List<SheetDataObject> sheetObjList = new List<SheetDataObject>();
    private BottomSheetHelper SheetHelper;

    private string[] nameList = new string[] { "Fridge", "Recliner", "Wodrobe", "Sofa" };

    public List<SheetDataObject>  GetSheetList
    {
        get { return sheetObjList; }
        set { sheetObjList = value; }
    }


    void Start()
    {
        placeObjectsOnPlane = transform.GetComponent<PlaceObjectsOnPlane>();
        SheetHelper = FindObjectOfType<BottomSheetHelper>();

        List<GameObject> itemToShow = placeObjectsOnPlane.placedPrefab;

        for (int i = 0; i < itemToShow.Count; i++)
        {
            var t = Instantiate(template, bottomSheet.transform.transform);
            t.name = i.ToString();
            t.transform.GetChild(0).GetComponent<RawImage>().texture = img[i].texture;
            t.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = nameList[i];

            t.GetComponent<Button>().onClick.AddListener(delegate
            {
                OnItemClicked(int.Parse(t.name));
            });
        }
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
        SheetHelper.BottonSheetPosition();
        Debug.Log(s);
    }

    private void SheetState(bool b)
    {
        //bottomSheet.transform.position = b ? hideSheetPos: initialSheetpos;
        placeObjectsOnPlane.BottomSheetActive = b;
        //bottomSheet.SetActive(!b);
    }
}