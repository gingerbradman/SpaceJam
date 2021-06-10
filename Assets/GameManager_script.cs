using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_script : MonoBehaviour
{

    public GameObject obstacleLine;

    private IEnumerator coroutine;

    void Start()
    {
        coroutine = CreateObstacle(2.0f);
        StartCoroutine(coroutine);
    }

    void Update()
    {
        
    }

    private IEnumerator CreateObstacle(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject clone;
            clone = Instantiate(obstacleLine, this.transform.position, this.transform.rotation);
            clone.GetComponent<Rigidbody2D>().velocity = Vector2.down * 3;
        }
    }
}
