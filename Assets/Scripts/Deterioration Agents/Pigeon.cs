using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour
{
    private float speed = 5.0f;
    private Vector2 target;

    private float distanceToRestorer;
    private float rangeToFly = 2f;
    private bool mustFly = false;
    private GameObject restorer;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(91f, 15f);
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (restorer == null)
        {
            restorer = GameObject.FindGameObjectWithTag("Restorer");
            Debug.Log("looking for restorer");
        }
        
        else
        {
            distanceToRestorer = Vector3.Distance(gameObject.transform.position, restorer.transform.position);

            if (distanceToRestorer < rangeToFly)
            {
                mustFly = true;
                anim.SetBool("mustFly", true);
            }

            if (mustFly)
            {
                float step = speed * Time.deltaTime;

                // move sprite towards the target location
                transform.position = Vector2.MoveTowards(transform.position, target, step);

                if (new Vector2(transform.position.x, transform.position.y) == target)
                {
                    Destroy(gameObject);
                }
            }
        }
        
        
    }
}
