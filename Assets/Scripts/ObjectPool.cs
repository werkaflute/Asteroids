using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public List<GameObject> prefab;
    private GameObject objectPrefab;
    public int amountToPool;

    private GameObject GetPooledObject()
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
        GameObject tmp;
        int rand;
        for (int i = 0; i < amountToPool; i++)
        {
            rand = Random.Range(0, 5);
            objectPrefab = prefab[rand];
            tmp = Instantiate(objectPrefab, this.transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
}
