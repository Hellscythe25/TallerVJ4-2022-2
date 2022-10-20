using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyTime;
    void Start()
    {
        Invoke("DestroyThisObject", destroyTime);
    }


    private void DestroyThisObject() 
    {
        Destroy(gameObject);
    }
}
