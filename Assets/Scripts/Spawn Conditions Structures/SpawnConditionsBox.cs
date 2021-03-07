using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConditionsBox : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    private Transform groundCheck; // A position marking where to check if the player is grounded.
    private Transform playerCheck; // A position marking where to check if the constructor is in range
    private Transform topCheck;     // A position to check if we can delete the box (nothing is above it)

    const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    const float k_PlayerRadius = 1f; // Radius of the overlap circle to determine if player in range

    private bool isGrounded;            // Whether or not the structure is grounded.
    private bool inPlayerInRange;
    private bool isBelowBox;

    public GameObject smoke;
    private GameObject smokeInstance;

    private AudioSource source;
    public AudioClip metalSound;

    private Animator constructorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // The ground check is the first child of the structure object
        if (gameObject.transform.GetChild(0))
            groundCheck = gameObject.transform.GetChild(0);

        // The check to see if the player (constructor) is in range
        if (gameObject.transform.GetChild(1))
            playerCheck = gameObject.transform.GetChild(1);

        // The check to see if the player (constructor) is in range
        if (gameObject.transform.GetChild(2))
            topCheck = gameObject.transform.GetChild(2);

        source = GetComponent<AudioSource>();

        constructorAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, k_GroundedRadius, whatIsGround);
        inPlayerInRange = Physics2D.OverlapCircle(playerCheck.position, k_PlayerRadius, whatIsPlayer);

        if (isGrounded && inPlayerInRange && LevelResources.numOfBox > 0)
        {
            //source.clip = metalSound;
            constructorAnimator.SetTrigger("building");
            StartCoroutine(WaitForSmoke());
            
        }
        else
        {
            // If it wasnt grounded, destroy the gameObject and free the coordenate space
            StructureController.structureCoordenates.Remove("" + gameObject.transform.position + gameObject.tag);
            Destroy(gameObject);
        }
       
    }

    IEnumerator WaitForSmoke()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        smokeInstance = Instantiate(smoke, gameObject.transform.position, Quaternion.identity);
        source.PlayOneShot(metalSound, 0.7f);
        LevelResources.numOfBox--;  // Remove by 1 the number of available structures of boxes
        StructureController.spawnedStructures.Push(gameObject);
        this.enabled = false; // Disable the script so now that its finally instanciated, it doesnt need to keep checking conditions

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true; // If it is grounded, make it now visible
        gameObject.GetComponent<BoxCollider2D>().enabled = true; // If it is grounded, make it now interactible 
        Destroy(smokeInstance);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null)
            return;

        if (playerCheck == null)
            return;

        Gizmos.DrawWireSphere(groundCheck.position, k_GroundedRadius);
        Gizmos.DrawWireSphere(playerCheck.position, k_PlayerRadius);
        Gizmos.DrawWireSphere(topCheck.position, k_GroundedRadius);
    }

    public void DestroyBox()
    {
        isBelowBox = Physics2D.OverlapCircle(topCheck.position, k_GroundedRadius, whatIsGround);

        if (!isBelowBox)
        {
            LevelResources.numOfBox++;  // Add by 1 the number of available structures
            Destroy(gameObject);
        }
    }
}
