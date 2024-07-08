using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform btnParant;

    public ObjectManager objectManager;

    public GameObject videoBg;

    public Transform arCam;

    Coroutine coroutine;

    public List<Sprite> soundSprites;

    public GameObject howOver;

    public Image sound;

    public List<Sprite> ObjectSprites;

    public List<objectButton> objectButtons;

    public objectButton objectButton;

    public GameObject objectList;

    public bool isShowList;


    public void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (PlayerPrefs.GetInt("arSceneSound") == 1)
            sound.sprite = soundSprites[1];
        else
            sound.sprite = soundSprites[0];

        howOver.SetActive(false);

        if (PlayerPrefs.GetInt("HowOver") == 0)
        {
            PlayerPrefs.SetInt("HowOver", 1);
            howOver.SetActive(true);

            Invoke(nameof(DeActiveHoveOver), 2);

        }

        if (objectButtons.Count > 0)
            return;


        if (Manager.Instance.mode == Manager.Mode.allObject)
        {
            objectList.SetActive(true);
            for (int i = 0; i < ObjectSprites.Count; i++)
            {
                objectButtons.Add(Instantiate(objectButton, btnParant));
                objectButtons[i].SetData(ObjectSprites[i], i);
            }
        }
        else
        {
            objectList.SetActive(false);
        }
    }

    public void DeActiveHoveOver()
    {
        howOver.SetActive(false);
    }


    private void Start()
    {
        coroutine = StartCoroutine(waitForVideoBg());
    }

    public IEnumerator waitForVideoBg()
    {
        yield return new WaitUntil(() => arCam.childCount > 1);

        videoBg = arCam.GetChild(1).gameObject;
    }

    public void BgOnOff()
    {
        if (videoBg == null)
            return;

        if (videoBg.activeSelf)
            videoBg.SetActive(false);
        else
            videoBg.SetActive(true);
    }


    public void ButtonEnable(List<Sprite> sprite)
    {

    }


    public void BtnClick(int crt)
    {
        if (objectManager == null)
            return;

        for (int i = 0; i < objectManager.allObjects.Count; i++)
            objectManager.allObjects[i].SetActive(false);

        objectManager.allObjects[crt].SetActive(true);

        objectManager.ResetObject();
    }

    public void SoundONOFF()
    {
        if (sound.sprite == soundSprites[0])
        {
            sound.sprite = soundSprites[1];
            PlayerPrefs.SetInt("arSceneSound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("arSceneSound", 0);
            sound.sprite = soundSprites[0];
        }
    }

    public void ResetBtn()
    {
        if (objectManager)
            objectManager.ResetObject();
    }

    public void Back()
    {
        SceneManager.LoadScene("Splash");
    }
}
