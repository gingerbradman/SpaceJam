using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap_script : MonoBehaviour
{
    bool isWrappingX = false;
    bool isWrappingY = false;
    Renderer[] renderers;
    Camera cam;
    private GameManager_script gm;

    Vector2 viewportPosition;
    float screenWidth;
    float screenHeight;
    Transform[] ghosts = new Transform[2];

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        cam = Camera.main;
        viewportPosition = cam.WorldToViewportPoint(transform.position);
        gm = FindObjectOfType<GameManager_script>();

        var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
 
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.y - screenBottomLeft.y;

        CreateGhostShips();
        PositionGhostShips();
        gm.FillGhosts(ghosts);

    }

    // Update is called once per frame
    void Update()
    {
        ScreenWrap();
    }

    void CreateGhostShips()
    {
        for(int i = 0; i < 2; i++)
        {
            ghosts[i] = Instantiate(transform, Vector3.zero, Quaternion.identity) as Transform;
    
            DestroyImmediate(ghosts[i].GetComponent<ScreenWrap_script>());
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
        SwapShips();
    }

    void SwapShips()
    {
        foreach(var ghost in ghosts)
        {
            if (ghost.position.x < screenWidth && ghost.position.x > -screenWidth &&
                ghost.position.y < screenHeight && ghost.position.y > -screenHeight)
            {
                transform.position = ghost.position;
    
                break;
            }
        }
    
        PositionGhostShips();
    }

    void PositionGhostShips()
    {
        // All ghost positions will be relative to the ships (this) transform,
        // so let's star with that.
        var ghostPosition = transform.position;
    
        // We're positioning the ghosts clockwise behind the edges of the screen.
        // Let's start with the far right.
        ghostPosition.x = transform.position.x + screenWidth;
        ghostPosition.y = transform.position.y;
        ghosts[0].position = ghostPosition;
    
        // Left
        ghostPosition.x = transform.position.x - screenWidth;
        ghostPosition.y = transform.position.y;
        ghosts[1].position = ghostPosition;
    
        // All ghost ships should have the same rotation as the main ship
        for(int i = 0; i < 2; i++)
        {
            ghosts[i].rotation = transform.rotation;
        }
    }
}
