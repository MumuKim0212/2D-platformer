using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public bool isGround;
    [SerializeField]
    float power;

    Rigidbody2D body;
    Animator anim;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
            return;

        // y축속도가 0있을때(점프중일때)
        if (body.velocityY > 0.5f)
            return;

        // 충돌의 y축 노말벡터가 0보다 클때
        isGround = collision.contacts[0].normal.y > 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
            return;
        isGround = false;
    }

    private void LateUpdate()
    {
        anim.SetBool("Ground", isGround);
    }
    void OnJump()
    {
        if (!isGround)
            return;
        body.AddForceY(power, ForceMode2D.Impulse);
    }
}
