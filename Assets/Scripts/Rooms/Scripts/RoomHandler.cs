using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    static RoomHandler _instance;
    public static RoomHandler Instance
    {   
        get 
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("RoomHandler");
                go.AddComponent<RoomHandler>();
            }
            return _instance;    
        } 
    }

    public Room currentRoom;
    public Room previousRoom;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        
    }

    private void Start()
    {
        currentRoom.DisableEntry();
    }

    public void UpdateRoom(Room room)
    {
        previousRoom = currentRoom;
        previousRoom.DisableEntry();
        currentRoom = room;


    }

    public void EnablePreviousRoom()
    {
        previousRoom.EnableEntry();
    }


}
