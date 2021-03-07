using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsController : MonoBehaviour
{
    private float timeToFix;
    private float timeToFixRemaining;
    private float distanceToRestorer;
    private float rangeToFix = 1f;
    public float agentTimtToFix;

    private bool isPaused;

    private GameObject restorer;

    public Sprite[] animationSprites;
    private float secondsPerSprite;
    private float secondsPerSpriteRemaining;
    private int i = 0;

    private AudioSource source;
    public AudioClip doneSound;

    public int pointsValue;

    public GameObject textPrefab;

    private bool inRange;
    const float k_AgentRadius = 0.5f; // Radius of the overlap circle to determine if player in range
    [SerializeField] private LayerMask whatIsRestorer;

    private Animator restorerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        inRange = false;
        isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        inRange = Physics2D.OverlapCircle(gameObject.transform.position, k_AgentRadius, whatIsRestorer);

        // If the restorer is in range and he has not deleted the agent
        if (inRange && gameObject.transform.childCount == 1)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if (Input.GetKeyDown("f"))
            {
                TakeDamage(agentTimtToFix);
            }
        }

        else if (!inRange && gameObject.transform.childCount == 1)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            isPaused = true;
        }

        // Update the fixing animation if he leaves the range of the agent (also reset the restorerAnimator so other agents in game dont the variable)
        if (restorerAnimator != null)
        {
            if (!inRange)
            {
                restorerAnimator.SetBool("isFixing", false);
                restorerAnimator = null;
            }
        }


        if (timeToFixRemaining > 0 && !isPaused)
        {
            timeToFixRemaining -= Time.deltaTime;
            secondsPerSpriteRemaining -= Time.deltaTime;

            if (timeToFixRemaining <= 0)
            {
                // Play a done sound effect
                source.clip = doneSound;
                source.PlayOneShot(doneSound, 1f);

                // Add the points to the total score
                LevelResources.score += pointsValue;

                // Decrease the number of agents for the total count (use a switch statement)
                switch (gameObject.tag)
                {
                    case "Crack":
                        LevelResources.crackAgents--;
                        break;
                    case "Moss":
                        LevelResources.mossAgents--;
                        break;
                    case "Poop":
                        LevelResources.poopAgents--;
                        break;
                    default:
                        break;
                }


                // Stop the fixing animation and remove the highlight child object
                restorerAnimator.SetBool("isFixing", false);
                Destroy(gameObject.transform.GetChild(0).gameObject);

                // Disable the renderer, so we can wait for the sound to finish and THEN delete the gameobject
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject, doneSound.length);
            }

            if (secondsPerSpriteRemaining <= 0)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = animationSprites[i++];
                secondsPerSpriteRemaining = secondsPerSprite;
            }

            //Debug.Log(timeToFixRemaining);
        }



    }

    public void TakeDamage(float time)
    {
        timeToFix = time;
        isPaused = false;

        // If its the first time he tries to restore the agent, the time is the full restoring time
        if (timeToFixRemaining == 0)
        {
            timeToFixRemaining = timeToFix;
            secondsPerSprite = timeToFix / animationSprites.Length;
            secondsPerSpriteRemaining = secondsPerSprite;
        }

        if (restorerAnimator == null)
        {
            restorerAnimator = GameObject.FindGameObjectWithTag("Restorer").GetComponent<Animator>();
        }

        restorerAnimator.SetBool("isFixing", true);


    }

    private void ShowText()
    {
        if (textPrefab)
        {

            Instantiate(textPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }

}

