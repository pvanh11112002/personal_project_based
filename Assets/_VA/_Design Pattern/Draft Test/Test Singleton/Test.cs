using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VA.DesignPattern;

public class Test : Singleton<Test>
{
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Test Initialized");
    }
    public void SomeMethod()
    {
        Debug.Log("Some Method");
    }    
}
