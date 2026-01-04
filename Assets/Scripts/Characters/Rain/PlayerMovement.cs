using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    InputAction action;
    public float speed;
    public bool moving;
    public Vector2 currentVelocity;
    public Vector2 normalizedVelocity;
    Vector2 input;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        action = InputSystem.actions.FindAction("Move");
       

        action.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = action.ReadValue<Vector2>();
     

    }

    private void FixedUpdate()
    {

        body.linearVelocity = action.ReadValue<Vector2>() * speed;
        currentVelocity = body.linearVelocity;
        normalizedVelocity = currentVelocity.normalized;

    }

    public bool isMoving()
    {
        if (currentVelocity.normalized.magnitude > 0.1f)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }


}
