using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_script : MonoBehaviour
{
    private int placeInArray;
    public enum hazardType {Blank, Enemy, Asteroid}; 

    public hazardType m_hazardType = hazardType.Blank;

    public void SetPlaceInArray(int i)
    {
        placeInArray = i;
    }
}
