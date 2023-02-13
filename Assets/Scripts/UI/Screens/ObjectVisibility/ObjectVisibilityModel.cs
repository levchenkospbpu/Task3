using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisibilityModel : BaseModel
{
    private List<GameObject> _checkedGameObjects;
    private Dictionary<int, float> _transparencyLevelValues;

    public ObjectVisibilityModel()
    {
        _checkedGameObjects = new List<GameObject>();
        _transparencyLevelValues = new Dictionary<int, float>()
        {
            {1, 0.2f },
            {2, 0.4f },
            {3, 0.6f },
            {4, 0.8f },
            {5, 1f }
        };
    }

    public void ChangeCheckedObjects(DataProvider dataProvider)
    {
        bool isOn = dataProvider.GetData<bool>();
        GameObject obj = dataProvider.GetData<GameObject>();
        if (isOn)
        {
            _checkedGameObjects.Add(obj);
        }
        else
        {
            _checkedGameObjects.Remove(obj);
        }
    }

    public void ChangeVisibility(DataProvider dataProvider)
    {
        bool isOn = dataProvider.GetData<bool>();
        foreach (GameObject obj in _checkedGameObjects)
        {
            if (isOn)
            {
                obj.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                obj.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    public void ChangeTranparency(DataProvider dataProvider)
    {
        int transparencyLevel = dataProvider.GetData<int>();
        foreach (GameObject obj in _checkedGameObjects)
        {
            Material newMaterial = obj.GetComponent<Renderer>().material;
            MaterialExtensions.ToFadeMode(newMaterial);
            newMaterial.color = new Color(newMaterial.color.r, newMaterial.color.g, newMaterial.color.b, _transparencyLevelValues[transparencyLevel]);
            obj.GetComponent<Renderer>().material = newMaterial;
        }
    }
}
