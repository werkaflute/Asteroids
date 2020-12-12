using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectPrefab;
    public int amountToPool;

    protected virtual GameObject GetPooledObject()
    {
        foreach(GameObject o in pooledObjects)
        {
            if(!o.activeInHierarchy)
            {
                return o;
            }
        }
        return null;
    }

    public void PlaceAndActivate(Vector3 pos)
    {
        GameObject chosen = GetPooledObject();
        if(chosen != null)
        {
            chosen.transform.position = pos;
            chosen.SetActive(true);
        }
    }

    public void PlaceAndActivate(Vector3 pos, Vector3 rot)
    {
        GameObject chosen = GetPooledObject();
        if (chosen != null)
        {
            chosen.transform.position = pos;
            chosen.transform.rotation = Quaternion.Euler(rot);
            chosen.SetActive(true);
        }
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectPrefab, this.transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
}
