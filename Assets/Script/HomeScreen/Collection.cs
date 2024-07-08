using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
    public Image image;

    public TextMeshProUGUI collectionText;

    public int crtObject;

    public void SetData(Sprite sprite, int crt)
    {
        crtObject = crt;

        image.sprite = sprite;
        image.SetNativeSize();
        gameObject.name = "collections_" + crt;
        collectionText.text = "Collection_" + crt;
        gameObject.SetActive(true);
    }

    public void LoadArScene()
    {
        Manager.Instance.crtObject = crtObject;
        Manager.Instance.mode = Manager.Mode.specificObject;
        SceneManager.LoadScene("ARScene");
    }

}
