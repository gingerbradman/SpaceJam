using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_script : MonoBehaviour
{

    public GameObject obstacleLine;
    private PlayerController player;
    private Transform[] ghostPlayers = new Transform[2];
    Renderer[] renderers;
    private ObjectPooler_script objectPooler;
    [SerializeField] private float obstacleSpeed = 3;
    [SerializeField] private float waitBetweenObstacleLines = 2.0f;
    [SerializeField] private int randomUpperLimit = 3;
    private IEnumerator coroutine;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public int score;
    [SerializeField] private bool scoreChanged = false;
    private Hazard_script.hazardType ht_blank = Hazard_script.hazardType.Blank;
    private Hazard_script.hazardType ht_enemy = Hazard_script.hazardType.Enemy;
    private Hazard_script.hazardType ht_asteroid = Hazard_script.hazardType.Asteroid;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        objectPooler = ObjectPooler_script.Instance;

        coroutine = CreateObstacle();
        StartCoroutine(coroutine);

        score = 0;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        ScoreManager();
    }

    private void ScoreManager()
    {
        if(scoreChanged == false)
        {
            return;
        }

        if(score >= 10)
        {
            randomUpperLimit = 4;
        }

        if(score >= 10 && obstacleSpeed <= 10f)
        {
            obstacleSpeed += .2f;

            if(waitBetweenObstacleLines >= 0.6f){
                waitBetweenObstacleLines -= .1f;
            }
        }

        scoreChanged = false;
    }

    public void FillGhosts(Transform[] ghosts)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghostPlayers[i] = ghosts[i];
        }
    }

    public void DestroyPlayer()
    {
        Destroy(player.gameObject);
        for (int i = 0; i < ghostPlayers.Length; i++)
        {
            Destroy(ghostPlayers[i].gameObject);
        }
        DecrementScore();
        gameOverPanel.SetActive(true);
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
        scoreChanged = true;
    }

    public void DecrementScore()
    {
        if(score != 0)
        {
            score--;
        }
        scoreText.text = score.ToString();
    }

    private IEnumerator CreateObstacle()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(waitBetweenObstacleLines);
            GameObject clone;
            clone = objectPooler.SpawnFromPool("ObstacleLine", this.transform.position, this.transform.rotation);
            clone.GetComponent<ObstacleLine_script>().setObstacleSpeed(obstacleSpeed);

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
        int i = Random.Range(1,randomUpperLimit); //Please note, random range goes from the first number to the int before the second.
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
