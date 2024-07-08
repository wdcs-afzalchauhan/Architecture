using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public Collection collection;

    public Transform parent;

    public List<Sprite> sprites = new List<Sprite>();

    public List<Collection> collections = new List<Collection>();

    public UiAnimation uiAnimation;



    private void Awake()
    {
        if (collections.Count == 0)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                collections.Add(Instantiate(collection, parent));

                collections[i].SetData(sprites[i], i);

                collections[i].transform.DOScale(0, 0);

                uiAnimation.scale.Add(collections[i].transform);
            }
        }
    }

}
