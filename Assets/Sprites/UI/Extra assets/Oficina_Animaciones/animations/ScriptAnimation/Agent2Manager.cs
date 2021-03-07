using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPanelClick()
    {
        Animator anim = GetComponent<Animator>();

        if (anim != null)
        {
            bool open = anim.GetBool("open");
            anim.SetBool("open", !open);
        }
        
    }
}
