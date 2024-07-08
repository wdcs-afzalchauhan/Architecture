using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public static HomeManager instance;

    public List<Image> imaegs = new List<Image>();

    public Canvas splashScreen;

    public UiAnimation homeScreen;

    public bool isAnim;


    private void Awake()
    {
        instance = this;

        if (Manager.Instance.splashScreen)
        {
            homeScreen.CloseScreen();
            splashScreen.enabled = true;
            for (int i = 0; i < imaegs.Count; i++)
                imaegs[i].DOFade(0, 0);
            StartCoroutine(ImageAnimStart());
            Manager.Instance.splashScreen = false;
        }
        else
        {
            splashScreen.enabled = false;
            homeScreen.OpenScreen();
        }
    }

    public IEnumerator ImageAnimStart()
    {
        for (int i = 0; i < imaegs.Count; i++)
        {
            imaegs[i].DOFade(1, 1);

            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);

        splashScreen.enabled = false;

        homeScreen.OpenScreen();

    }

    public void ArScene()
    {
        Manager.Instance.mode = Manager.Mode.allObject;

        PlayerPrefs.SetInt("SetCrtObject", 0);
        SceneManager.LoadScene("ARScene");
    }

}
