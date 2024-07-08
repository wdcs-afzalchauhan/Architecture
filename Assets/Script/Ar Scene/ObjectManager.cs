using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> objects;

    Transform parant;

    public List<GameObject> allObjects;

    public List<Sprite> sprite;

    public float rotationSpeed = 100f;

    public Quaternion rot;

    public VariableJoystick variableJoystick;

    private void OnEnable()
    {
        if (parant == null)
        {
            parant = this.transform;
        }

        if (allObjects.Count == 0)
        {
            if (Manager.Instance.mode == Manager.Mode.allObject)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    allObjects.Add(Instantiate(objects[i], objects[i].transform.localPosition, objects[i].transform.localRotation, parant));

                    if (i == 0)
                        allObjects[0].SetActive(true);
                    else
                        allObjects[i].SetActive(false);
                }
            }
            else
            {
                int crt = Manager.Instance.crtObject;
                allObjects.Add(Instantiate(objects[crt], objects[crt].transform.localPosition, objects[crt].transform.localRotation, parant));
                allObjects[0].SetActive(true);
            }
        }
        UIManager.Instance.objectManager = this;
    }

    public void ResetObject()
    {
        transform.localRotation = rot;
    }

}
