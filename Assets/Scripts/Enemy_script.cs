using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_script : Hazard_script
{

    // Start is called before the first frame update
    void Start()
    {
        m_hazardType = hazardType.Enemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().getGM().DestroyPlayer();
        }
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            other.gameObject.SetActive(false);
            GetComponentInParent<ObstacleLine_script>().DestroyChild(this.gameObject);
        }
        
    }
}
