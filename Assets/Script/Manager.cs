using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public int crtObject;

    public enum Mode { allObject, specificObject }

    public bool splashScreen = true;

    public Mode mode;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            splashScreen = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
