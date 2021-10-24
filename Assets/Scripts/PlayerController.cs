using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D r;
    private ObjectPooler_script objectPooler;
    public void setR(Rigidbody2D rigidBody){ r = rigidBody;}
    public Joystick joystick;
    private bool isMoving = false;
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
    }

    void Movement()
    {
        if(!Touchscreen.current.primaryTouch.press.isPressed)
        {
            isMoving = false;
            return;
        }

        isMoving = true;

        r.position += new Vector2(joystick.Horizontal,joystick.Vertical) * speed * Time.deltaTime;
    }

    //FireBullet Function is called by the Shooting Panel event triggers. 
    // NOTE: It is very important that the Shooting Panel is above the DirectionPanel in the hierarchy. It will not work any other way.
    public void FireBullet()
    {
        GameObject clone;
        clone = objectPooler.SpawnFromPool("PlayerBullet", this.transform.position, this.transform.rotation);
        clone.GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
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
