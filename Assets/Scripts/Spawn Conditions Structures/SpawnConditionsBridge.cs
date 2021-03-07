using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConditionsBridge : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    private Transform wallCheckLeft; // A position marking where to check if the player is grounded.
    private Transform wallCheckRight; // A position marking where to check if the player is grounded.
    private Transform playerCheck; // A position marking where to check if the constructor is in range

    const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    const float k_PlayerRadius = 2f; // Radius of the overlap circle to determine if player in range

    private bool isGrounded;            // Whether or not the structure is grounded.
    private bool inPlayerInRange;

    public GameObject smoke;
    private GameObject smokeInstance;

    private AudioSource source;
    public AudioClip metalSound;

    // Start is called before the first frame update
    void Start()
    {
        // The ground check is the first child of the structure object
        if (gameObject.transform.GetChild(0))
            wallCheckLeft = gameObject.transform.GetChild(0);
        if (gameObject.transform.GetChild(1))

            wallCheckRight = gameObject.transform.GetChild(1);
        // The check to see if the player (constructor) is in range
        if (gameObject.transform.GetChild(2))
            playerCheck = gameObject.transform.GetChild(2);

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = ( Physics2D.OverlapCircle(wallCheckLeft.position, k_GroundedRadius, whatIsGround) || Physics2D.OverlapCircle(wallCheckRight.position, k_GroundedRadius, whatIsGround) );
        inPlayerInRange = Physics2D.OverlapCircle(playerCheck.position, k_PlayerRadius, whatIsPlayer);

        if (isGrounded && inPlayerInRange && LevelResources.numOfBridge > 0)
        {
            source.clip = metalSound;
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
        source.PlayOneShot(metalSound, 0.7f);
        smokeInstance = Instantiate(smoke, gameObject.transform.position, Quaternion.identity);
        LevelResources.numOfBridge--;  // Remove by 1 the number of available structures of boxes
        StructureController.spawnedStructures.Push(gameObject);
        this.enabled = false; // Disable the script so now that its finally instanciated, it doesnt need to keep checking conditions

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true; // If it is grounded, make it now visible
        gameObject.GetComponent<BoxCollider2D>().enabled = true; // If it is grounded, make it now interactible 
        Destroy(smokeInstance);
    }

    private void OnDrawGizmos()
    {
        if (wallCheckLeft == null)
            return;

        Gizmos.DrawWireSphere(wallCheckLeft.position, k_GroundedRadius);
    }
}
