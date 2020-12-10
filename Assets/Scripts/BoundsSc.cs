using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsSc : MonoBehaviour
{
    public int xSize, ySize;
    public GameObject[] boundaries;
    public GameObject[] asteroidSpawns;

    void OnValidate()
    {
        boundaries[0].transform.position = new Vector3(0f, 0f, ySize / 2);
        boundaries[1].transform.position = new Vector3(xSize/2, 0f, 0f);
        boundaries[2].transform.position = new Vector3(0f, 0f, -ySize / 2);
        boundaries[3].transform.position = new Vector3(-xSize/2, 0f, 0f);

        boundaries[0].transform.localScale = new Vector3(xSize, 1f, 1f);
        boundaries[1].transform.localScale = new Vector3(1f, 1f, ySize);
        boundaries[2].transform.localScale = new Vector3(xSize, 1f, 1f);
        boundaries[3].transform.localScale = new Vector3(1f, 1f, ySize);
    }
}
