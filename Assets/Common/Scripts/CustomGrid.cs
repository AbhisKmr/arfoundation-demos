using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGrid : LayoutGroup
{
    public int rows;
    public int column;
    public Vector2 cellSize;


    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrt);
        column = Mathf.CeilToInt(sqrt);

        float parentHeight = rectTransform.rect.height;
        float parentWidth = rectTransform.rect.width;

        float cellWidth = parentWidth / column;
        float cellHeight = parentHeight / rows;

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / column;
            columnCount = i % column;

            var item = rectChildren[i];

            var xPos = cellSize.x * columnCount;
            var yPos = cellSize.y * rowCount;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);

        }
    }

    public override void SetLayoutVertical()
    {
    }
}
