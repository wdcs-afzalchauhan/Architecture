using UnityEngine;
using DG.Tweening;

public class Animation : MonoBehaviour
{
    public RectTransform current;

    private void Awake()
    {
        current.DOPivotY(0.7f, 0.5f);
    }

    public void ShowAndHideObjectList()
    {
        Debug.LogError("current.pivot " + current.pivot.y);

        if (!UIManager.Instance.isShowList)
        {
            UIManager.Instance.isShowList = true;
            current.DOPivotY(0.1f, 0.5f);
        }
        else
        {
            UIManager.Instance.isShowList = false;
            current.DOPivotY(0.7f, 0.5f);
        }
    }
}
