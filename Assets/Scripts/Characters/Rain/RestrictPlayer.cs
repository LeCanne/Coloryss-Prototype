using UnityEngine;

public class RestrictPlayer : MonoBehaviour
{
    public bool restrictToCamera = false;
    public Collider2D hitBoxBound;

    private void Awake()
    {
        hitBoxBound = GetComponentInChildren<Collider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(restrictToCamera == true)
        {
            RestrictToCameraBounds();
        }
    }


    void RestrictToCameraBounds()
    {
        float screenAspect = 8f / 7f;
        float cameraHeight = Camera.main.orthographicSize * 2;
        Bounds bounds = new Bounds(
            Camera.main.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));


        float camX = Camera.main.transform.position.x;
        float camY = Camera.main.transform.position.y;  
        float boundRight = bounds.extents.x + camX - (hitBoxBound.bounds.size.x+0.4f);
        float boundLeft = -bounds.extents.x + camX + (hitBoxBound.bounds.size.x+0.4f); 
        float boundUp = -bounds.extents.y + camY + (hitBoxBound.bounds.size.y+0.4f);
        float boundDown = bounds.extents.y + camY - (hitBoxBound.bounds.size.y+0.4f);

        float posX = transform.position.x;
        float posY = transform.position.y;
        float ClampedX = Mathf.Clamp(posX, boundLeft, boundRight);
        float ClampedY = Mathf.Clamp(posY, boundUp, boundDown);

        transform.position = new Vector2(ClampedX, ClampedY);
    }
}
