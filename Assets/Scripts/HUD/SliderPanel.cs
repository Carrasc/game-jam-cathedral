using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPanel : MonoBehaviour
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
            bool show = anim.GetBool("show");
            anim.SetBool("show", !show);
        }
        
    }
}
