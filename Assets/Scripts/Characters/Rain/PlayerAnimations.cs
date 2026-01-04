using UnityEngine;

[RequireComponent (typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    Animator playerAnimator;
    public float speed;
    public float horizontal;
    public float vertical;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        playerAnimator.SetFloat("Vertical", vertical);
        playerAnimator.SetFloat("Horizontal", horizontal);
        playerAnimator.SetFloat("CurrentSpeed", speed);


    }
}
