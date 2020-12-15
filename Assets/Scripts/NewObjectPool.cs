using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectPool : ObjectPool
{
    public void PlaceAndActivate(Vector3 pos, Vector3 rot, Vector3 scale)
    {
        GameObject chosen = GetPooledObject();
        if (chosen != null)
        {
            chosen.transform.position = pos;
            chosen.transform.localScale = scale;
            chosen.transform.rotation = Quaternion.Euler(rot);
            chosen.SetActive(true);
        }
    }

    protected override GameObject GetPooledObject()
    {
        foreach (GameObject o in pooledObjects)
        {
            if (!o.activeInHierarchy)
            {
                return o;
            }
        }
        var tmp = Instantiate(prefab[Random.Range(0,prefab.Count)], this.transform);
        tmp.SetActive(false);
        pooledObjects.Add(tmp);
        return tmp;
    }
}
