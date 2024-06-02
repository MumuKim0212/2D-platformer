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

        // y��ӵ��� 0������(�������϶�)
        if (body.velocityY > 0.5f)
            return;

        // �浹�� y�� �븻���Ͱ� 0���� Ŭ��
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
