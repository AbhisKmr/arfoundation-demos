using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSheetHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject bottomSheet;

    [SerializeField]
    private GameObject sheetList;

    private bool isSheetStateShow = true;
    private Vector3 showPos = new Vector3();

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
        
        isSheetStateShow = !isSheetStateShow;
    }
}