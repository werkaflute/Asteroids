using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInformation : MonoBehaviour
{
    // Start is called before the first frame update

    void DisableControlsChangeInfo()
    {
        gameObject.SetActive(false);
    }
    void Start()
    {
        Invoke("DisableControlsChangeInfo", 5);
    }
}
