using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLine_script : MonoBehaviour, IPooledObject
{
    private List<Hazard_script.hazardType> hazardList = new List<Hazard_script.hazardType>();
    public Sprite enemyShipSprite;
    public Sprite asteroidSprite;
    public Crossline_script crossline;
    private float obstacleSpeed;
    private float previousSpeed;
    private bool speedChange;
    public void setObstacleSpeed(float x) { speedChange = true; obstacleSpeed = x; }
    public bool getPassed() { return crossline.getPassed(); }
    public void setPassed() { crossline.setPassed(); }
    public void resetPassed() { crossline.resetPassed(); }
    void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.down * obstacleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (speedChange = true)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.down * obstacleSpeed;
            speedChange = false;
        }
    }

    public void OnObjectSpawn()
    {
        hazardList.Clear();
        crossline.resetPassed();
    }

    public void DestroyChild(GameObject child)
    {
        child.tag = "Blank";
        child.layer = 11;
        child.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = null;
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

    private void ClearPreviousHazard(GameObject child)
    {
        if (child.tag == "Blank")
        {
            Destroy(child.GetComponent<Hazard_script>());
        }

        if (child.tag == "Enemy")
        {
            Destroy(child.GetComponent<Enemy_script>());
        }

        if (child.tag == "Asteroid")
        {
            Destroy(child.GetComponent<Asteroid_script>());
        }

        child.tag = "Blank";
        child.layer = 11;
        child.transform.rotation = Quaternion.identity; //Reset Rotation
        GameObject temp = child.transform.GetChild(0).gameObject;
        temp.GetComponent<SpriteRenderer>().sprite = null;
        temp.transform.rotation = Quaternion.identity;
    }

    public void AssignHazard(GameObject childObject, Hazard_script.hazardType hazardType)
    {
        if (hazardType == Hazard_script.hazardType.Blank)
        {
            ClearPreviousHazard(childObject);
            childObject.tag = "Blank";
            childObject.layer = 11; //Blank Layer Integer
            childObject.AddComponent(System.Type.GetType("Hazard_script"));
        }
        else if (hazardType == Hazard_script.hazardType.Enemy)
        {
            ClearPreviousHazard(childObject);
            childObject.tag = "Enemy";
            childObject.layer = 10;
            childObject.AddComponent(System.Type.GetType("Enemy_script"));
            childObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = enemyShipSprite;
        }
        else if (hazardType == Hazard_script.hazardType.Asteroid)
        {
            ClearPreviousHazard(childObject);
            childObject.tag = "Asteroid";
            childObject.layer = 10;
            childObject.AddComponent(System.Type.GetType("Asteroid_script"));
            childObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = asteroidSprite;
        }
    }
}
