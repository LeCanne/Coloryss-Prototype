using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector]public Vector3 roomPosition;
    [HideInInspector]public Collider2D myEntry;
    bool triggered;

    private void Awake()
    {
        roomPosition = transform.position;  
        myEntry = GetComponent<Collider2D>();
    }

    public void EnableEntry()
    {
        myEntry.enabled = true;
        
    }

    public void DisableEntry()
    {
        myEntry.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        RainController myRain = collision.gameObject.GetComponentInParent<RainController>();
        if (myRain != null)
        {
            
            StartCoroutine(OnRoomChange(myRain));
        }
              
    }

    IEnumerator OnRoomChange(RainController player)
    {
        

        //RestrictPlayer
        player.ToggleRestrictPlayer();
        Time.timeScale = 0;

        //Update current room
        RoomHandler.Instance.UpdateRoom(this);
        DisableEntry();
  
        //MoveCamera and DisableCollisions
        CameraHandler.Instance.MoveCamera(Camera.main, roomPosition);

        //Wait until Camera has finished moving
        yield return new WaitForSecondsRealtime(CameraHandler.Instance.transitionDuration + 0.2f);

        
        //Default gamestate
        Time.timeScale = 1;
        RoomHandler.Instance.EnablePreviousRoom();

        player.ToggleRestrictPlayer();
        yield return null;


        
    }
}

