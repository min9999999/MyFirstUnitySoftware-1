using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Corgi가 현재 위치에서부터 목적지 까지 특정 속도로 이동
/// 속성: 목적지, 속도
/// </summary>
public class CorgiMove : MonoBehaviour
{
    public Animator animator;
    public GameObject detination;
    [SerializeField] private float speed; // SerializeField 속성(attribute): 인스펙터창에 보여주기 위한 속성
    public bool isWalking = false;

    // Corgi가 현재 위치에서부터 목적지 까지 특정 속도로 이동
    void Update()
    {
        // 버튼 p를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.P))
        {
            isWalking = !isWalking;
        }

        if (isWalking)
        {
            Move();

            animator.SetBool("isWalking", true); // 애니메이션 작동
        }
        else
        {
            animator.SetBool("isWalking", false);// 애니메이션 멈춤
        }
    }

    private void Move()
    {
        Vector3 direction = (detination.transform.position - transform.position);
        float distance = direction.magnitude;
        print("거리: " + distance);
        
        if(distance < 0.5f)
        {
            isWalking = false;
            animator.SetBool("isWalking", false);// 애니메이션 멈춤
        }

        Vector3 newDir = new Vector3(direction.normalized.x, 0, direction.normalized.z);
        transform.position = transform.position + (newDir * speed * Time.deltaTime);

        //transform.LookAt(detination.transform);
        transform.forward = newDir;
    }
}
