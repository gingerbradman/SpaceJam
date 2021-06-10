using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_script : MonoBehaviour
{

    public GameObject obstacleLine;

    private IEnumerator coroutine;
    public TextMeshProUGUI scoreText;
    public int score;
    private Hazard_script.hazardType ht_blank = Hazard_script.hazardType.Blank;
    private Hazard_script.hazardType ht_enemy = Hazard_script.hazardType.Enemy;
    private Hazard_script.hazardType ht_asteroid = Hazard_script.hazardType.Asteroid;

    void Start()
    {
        coroutine = CreateObstacle(2.0f);
        StartCoroutine(coroutine);
        score = 0;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private IEnumerator CreateObstacle(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject clone;
            clone = Instantiate(obstacleLine, this.transform.position, this.transform.rotation);
            clone.GetComponent<Rigidbody2D>().velocity = Vector2.down * 3;

            ObstacleLine_script obstacle = clone.GetComponent<ObstacleLine_script>();

            obstacle.Hazards(RandomHazardGenerator(),
                            RandomHazardGenerator(),
                            RandomHazardGenerator(),
                            RandomHazardGenerator(),
                            RandomHazardGenerator(),
                            RandomHazardGenerator(),
                            RandomHazardGenerator(),
                            RandomHazardGenerator()
            );
        }
    }

    private Hazard_script.hazardType RandomHazardGenerator()
    {
        int i = Random.Range(1,3);
        switch (i)
        {
            case 1:
                return ht_blank;
            
            case 2:
                return ht_enemy;

            case 3:
                return ht_asteroid;

            default:
                return ht_blank;
        }
    }
}
