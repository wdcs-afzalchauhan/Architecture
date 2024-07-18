using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class UiAnimation : MonoBehaviour
{
    public Canvas canvas;

    public Image bg;

    public List<Transform> scale = new List<Transform>();
    public List<Transform> rot = new List<Transform>();
    public List<Transform> pos = new List<Transform>();

    public float waitTime = 1;

    private void Awake()
    {

    }
    public void OpenScreen()
    {
        canvas.enabled = true;

        StartCoroutine(AnimStart());

    }

    public void CloseScreen()
    {


        bg.DOFade(0, 0);

        if (scale.Count > 0)
            for (int i = 0; i < scale.Count; i++)
                scale[i].DOScale(0, 0);

        canvas.enabled = false;

    }


    public IEnumerator AnimStart()
    {
        HomeManager.instance.isAnim = true;

        bg.DOFade(1, 1);

        if (scale.Count > 0)
        {
            for (int i = 0; i < scale.Count; i++)
            {
                scale[i].DOScale(1, waitTime);
                yield return new WaitForSeconds(waitTime);
            }
        }

        HomeManager.instance.isAnim = false;
    }

}
