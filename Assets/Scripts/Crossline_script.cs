using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossline_script : MonoBehaviour
{
    private bool passed = false;
    public bool getPassed(){ return passed;}
    public void setPassed(){ passed = true;}
    public void resetPassed(){ passed = false;}

}
