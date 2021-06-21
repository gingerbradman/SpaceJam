using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D r;
    private ObjectPooler_script objectPooler;
    public void setR(Rigidbody2D rigidBody){ r = rigidBody;}
    private float horizontalMovement;
    private float verticalMovement;
    private GameManager_script gm;
    public GameManager_script getGM(){return gm;}

    GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        setR(this.GetComponent<Rigidbody2D>());
        gm = FindObjectOfType<GameManager_script>();
        objectPooler = ObjectPooler_script.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FireBullet();
    }

    void Movement()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        r.position += new Vector2( horizontalMovement, verticalMovement) * speed * Time.deltaTime;
    }

    void FireBullet()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject clone;
            clone = objectPooler.SpawnFromPool("PlayerBullet", this.transform.position, this.transform.rotation);
            clone.GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("CrossLine"))
        {
            ObstacleLine_script obstacle = other.transform.parent.GetComponent<ObstacleLine_script>();
            
            if(obstacle.getPassed() == false){

                obstacle.GetComponent<ObstacleLine_script>().setPassed();
                gm.IncrementScore();

            }
        }
    }


}
