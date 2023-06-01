using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    public static anim instance;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{

        //    animator.SetBool("attack", true);
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    animator.SetBool("attack", false);

        //}
    }
}
