using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLine_script : MonoBehaviour, IPooledObject
{
    private List<Hazard_script.hazardType> hazardList = new List<Hazard_script.hazardType>();
    public Sprite enemyShip;

    public Crossline_script crossline;
    public bool getPassed() { return crossline.getPassed(); }
    public void setPassed(){ crossline.setPassed(); }
    public void resetPassed(){ crossline.resetPassed(); }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnObjectSpawn()
    {
        for (int i = 0; i < hazardList.Count; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.tag = "Blank";
            child.layer = 11;
            child.GetComponent<SpriteRenderer>().sprite = null;
        }
        hazardList.Clear();
        crossline.resetPassed();
    }

    public void DestroyChild(GameObject child)
    {
        child.tag = "Blank";
        child.layer = 11;
        child.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void Hazards(Hazard_script.hazardType h0,
                         Hazard_script.hazardType h1,
                         Hazard_script.hazardType h2,
                         Hazard_script.hazardType h3,
                         Hazard_script.hazardType h4,
                         Hazard_script.hazardType h5,
                         Hazard_script.hazardType h6,
                         Hazard_script.hazardType h7
                         )

    {
        hazardList.Add(h0);
        hazardList.Add(h1);
        hazardList.Add(h2);
        hazardList.Add(h3);
        hazardList.Add(h4);
        hazardList.Add(h5);
        hazardList.Add(h6);
        hazardList.Add(h7);

        FillLine();
    }

    public void FillLine()
    {
        AssignHazard(transform.GetChild(0).gameObject, hazardList[0]);
        AssignHazard(transform.GetChild(1).gameObject, hazardList[1]);
        AssignHazard(transform.GetChild(2).gameObject, hazardList[2]);
        AssignHazard(transform.GetChild(3).gameObject, hazardList[3]);
        AssignHazard(transform.GetChild(4).gameObject, hazardList[4]);
        AssignHazard(transform.GetChild(5).gameObject, hazardList[5]);
        AssignHazard(transform.GetChild(6).gameObject, hazardList[6]);
        AssignHazard(transform.GetChild(7).gameObject, hazardList[7]);
    }

    public void AssignHazard(GameObject childObject, Hazard_script.hazardType hazardType)
    {
        if(hazardType == Hazard_script.hazardType.Blank)
        {
            childObject.tag = "Blank";
            childObject.layer = 11; //Blank Layer Integer
            childObject.AddComponent(System.Type.GetType("Hazard_script"));
        }
        else if(hazardType == Hazard_script.hazardType.Enemy)
        {
            childObject.tag = "Enemy";
            childObject.layer = 10;
            childObject.AddComponent(System.Type.GetType("Enemy_script"));
            childObject.GetComponent<SpriteRenderer>().sprite = enemyShip;
        }
        /*
        else if(hazardType == Hazard_script.hazardType.Asteroid)
        {
            
        }
        */
    }
}
