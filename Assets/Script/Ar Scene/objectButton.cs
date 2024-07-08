using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectButton : MonoBehaviour
{
    public Image image;
    public int crtObject;

    public void SetData(Sprite sprite , int crt)
    {
        gameObject.name = "Collection_" + crt;
        image.sprite = sprite;
        image.SetNativeSize();
        crtObject = crt;
        gameObject.SetActive(true);
    }


    public void OnImageClick()
    {
        UIManager.Instance.BtnClick(crtObject);
    }



}
