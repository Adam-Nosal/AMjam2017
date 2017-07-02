using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public    class ScreenResolutionManager : Singleton<ScreenResolutionManager>
    {
    public int xRes = 1280 ;
    public int yRes = 720;


    private void Awake()
    {
        Screen.SetResolution(xRes, yRes, true);
    }
}

