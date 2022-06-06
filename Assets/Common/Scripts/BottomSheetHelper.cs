using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomSheetHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject bottomSheet;

    [SerializeField]
    private GameObject sheetList;

    [SerializeField]
    private RawImage rawImage;

    [SerializeField]
    private Text text;

    [SerializeField]
    private List<Sprite> ImageSprite;


    private bool isSheetStateShow = true;
    private Vector3 showPos = new Vector3();
    private string[] textlst = new string[] { "Tap to hide bottom sheet", "Tap to expand bottom sheet" };

    private void Start()
    {
        showPos = bottomSheet.transform.position;
        BottonSheetPosition();
    }

    public void BottonSheetPosition()
    {
        var hidePos = new Vector3(showPos.x, 245f, showPos.z);

        bottomSheet.transform.position = isSheetStateShow ? showPos: hidePos;
        sheetList.SetActive(isSheetStateShow);

        if (isSheetStateShow)
        {
            rawImage.texture = ImageSprite[1].texture;
            text.text = textlst[0];
        }
        else
        {
            rawImage.texture = ImageSprite[0].texture;
            text.text = textlst[1];
        }

        isSheetStateShow = !isSheetStateShow;
    }
}