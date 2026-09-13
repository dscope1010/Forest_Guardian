using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float MoveSpeed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //玩家输入监听
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 position = transform.position;
        // 方向
        Vector2 MoveDir = new Vector2(horizontal,vertical).normalized;
        // 位置每帧变化
        position += MoveDir*MoveSpeed*Time.fixedDeltaTime;
        transform.position = position;

        if(!Mathf.Approximately(MoveDir.x,0))
        {
            animator.SetFloat("Look X",MoveDir.x);
            animator.SetFloat("Look Y",0);
        }
        else
        {
            animator.SetFloat("Look X",0);
            if(!Mathf.Approximately(0,MoveDir.y))
            {
                animator.SetFloat("Look Y",MoveDir.y);
            }
        }

    }
}
