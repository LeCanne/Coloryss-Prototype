using UnityEngine;

[RequireComponent (typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    public GameObject posA;
    Camera cam;

    private void Awake()
    {
       cam = GetComponent<Camera>();
    }


    
}
