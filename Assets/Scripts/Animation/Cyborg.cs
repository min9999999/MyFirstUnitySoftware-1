using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// FSM(유한상태머신)을 사용한 NPC 움직임
/// 속성: 상태, 속도
/// </summary>
public class Cyborg : MonoBehaviour
{
    enum CyborgFSM
    {
        IDLE,
        WALK,
        RUN,
        POINT
    }
    [SerializeField] Transform player;
    [SerializeField] CyborgFSM state = CyborgFSM.IDLE;
    [SerializeField] float walkSpeed = 2;
    [SerializeField] float runSpeed = 10;
    public bool isPointed = false;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (player.position - transform.position).magnitude;

        // 거리가 10m 이상이면 NPC의 상태는 IDLE
        if (distance > 10)
        {
            state = CyborgFSM.IDLE;
        }
        // 거리가 10m에서 3m사이 이면 NPC의 상태는 RUN
        else if (distance > 4 && distance < 10)
        {
            state = CyborgFSM.RUN;
        }
        // 거리가 3m에서 1m사이 이면 NPC의 상태는 WALK
        else if( distance > 2.5f && distance < 4)
        {
            state = CyborgFSM.WALK;
        }
        else if( distance < 2.5f)
        {
            state = CyborgFSM.IDLE;
        }

        switch (state)
        {
            case CyborgFSM.IDLE:
                animator.SetBool("isWalking", false);
                animator.SetBool("walking2Running", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isPointing", false);

                isPointed = false;
                print("현재 상태: IDLE");
                break;
            case CyborgFSM.WALK:
                Walk();

                animator.SetBool("isWalking", true);
                animator.SetBool("walking2Running", false);
                animator.SetBool("isRunning", false);

                isPointed = false;
                print("현재 상태: WALK");
                break;
            case CyborgFSM.RUN:
                Run();

                animator.SetBool("walking2Running", true);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);

                isPointed = false;
                print("현재 상태: RUN");
                break;
            case CyborgFSM.POINT:
                if(isPointed == false)
                {
                    StartCoroutine(GoBackToIdle());
                    animator.SetBool("isPointing", true);
                    isPointed = true;
                }

                animator.SetBool("isWalking", false);
                animator.SetBool("walking2Running", false);

                print("현재 상태: POINT");
                break;
        }
    }

    private IEnumerator GoBackToIdle()
    {
        yield return new WaitForSeconds(1);

        state = CyborgFSM.IDLE;

        isPointed = false;
    }

    private void Run()
    {
        Vector3 dir = player.position - transform.position;
        Vector3 newDir = new Vector3(dir.x, 0, dir.z).normalized;
        transform.position += newDir * runSpeed * Time.deltaTime;
        transform.forward = newDir;
    }

    private void Walk()
    {
        Vector3 dir = player.position - transform.position;
        Vector3 newDir = new Vector3(dir.x, 0, dir.z).normalized;
        transform.position += newDir * walkSpeed * Time.deltaTime;
        transform.forward = newDir;
    }
}
