using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float speed;
    float inputValue;

    Rigidbody2D body;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        body.velocityX = inputValue * speed;
    }

    private void LateUpdate()
    {
        // Update�� FixedUpdate���� ����� ���� ó���ϱ� ���� LateUpdate
        anim.SetFloat("Speed", Mathf.Abs(inputValue));

        if (inputValue != 0)
            spriter.flipX = inputValue < 0;
    }

    void OnMove(InputValue value)
    {
        inputValue = value.Get<Vector2>().x;

        body.gravityScale = Mathf.Abs(inputValue) * 3;
    }
}
