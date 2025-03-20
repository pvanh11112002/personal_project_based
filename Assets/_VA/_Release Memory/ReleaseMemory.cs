using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VA.Addressable;
using VA.DesignPattern;

public class ReleaseMemory : MonoBehaviour
{
    private void OnDestroy()
    {
        CustomAddressables.ReleaseMemory();
        //PoolingObject.ReleaseMemory();
    }
}
