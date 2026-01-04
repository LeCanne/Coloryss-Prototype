using UnityEngine;

public class RainController : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private SpriteRenderer playerRenderer;
    private RestrictPlayer restrictPlayer;

    private void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerAnimations = GetComponentInChildren<PlayerAnimations>();
        playerRenderer = GetComponentInChildren<SpriteRenderer>();
        restrictPlayer = GetComponentInChildren<RestrictPlayer>();

        DialogueHandler.Instance.DialogueStarted += StopMovement;
        DialogueHandler.Instance.DialogueEnded += AllowMovement;


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }


    private void Update()
    {
        ManageAnimations();
    }

    void ManageAnimations()
    {
        playerAnimations.speed = playerMovement.normalizedVelocity.magnitude;
        if (playerMovement.isMoving())
        {
            playerAnimations.horizontal = playerMovement.normalizedVelocity.x;
            playerAnimations.vertical = playerMovement.normalizedVelocity.y;
        }     
    }

    public void ToggleRestrictPlayer()
    {
        
        restrictPlayer.restrictToCamera = !restrictPlayer.restrictToCamera;
    }

    void StopMovement()
    {
       
        playerMovement.enabled = false;
    }

    void AllowMovement()
    {
        playerMovement.enabled = true;
    }

    
}
