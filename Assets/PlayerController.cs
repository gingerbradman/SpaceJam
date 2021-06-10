using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D r;
    public void setR(Rigidbody2D rigidBody){ r = rigidBody;}
    private float horizontalMovement;
    private float verticalMovement;
    GameManager_script gm;
    bool isWrappingX = false;
    bool isWrappingY = false;

    Renderer[] renderers;
    Camera cam;
    Vector2 viewportPosition;

    // Start is called before the first frame update
    void Start()
    {
        setR(this.GetComponent<Rigidbody2D>());
        renderers = GetComponentsInChildren<Renderer>();
        cam = Camera.main;
        viewportPosition = cam.WorldToViewportPoint(transform.position);
        gm = FindObjectOfType<GameManager_script>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ScreenWrap();
    }

    void Movement()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        r.position += new Vector2( horizontalMovement, verticalMovement) * speed * Time.deltaTime;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("ObstacleLine"))
        {
            ObstacleLine_script obstacle = other.GetComponent<ObstacleLine_script>();
            
            if(obstacle.getPassed() == false){

                obstacle.GetComponent<ObstacleLine_script>().setPassed();
                gm.IncrementScore();

            }
        }
    }

    bool CheckRenderers()
    {
        foreach(var renderer in renderers)
        {
            // If at least one render is visible, return true
            if(renderer.isVisible)
            {
                return true;
            }
        }
    
        // Otherwise, the object is invisible
        return false;
    }
    
    void ScreenWrap()
    {
        var isVisible = CheckRenderers();
    
        if(isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }
    
        if(isWrappingX && isWrappingY) {
            return;
        }
    
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;
    
        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
    
            isWrappingX = true;
        }
    
        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
    
            isWrappingY = true;
        }
    
        transform.position = newPosition;
    }
}
