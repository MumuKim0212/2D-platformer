using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // 4가지 움직이는 방식
    enum Type { Velocity, MovePosition, AddForce, Slide }
    [SerializeField]
    Type logicType;

    [SerializeField]
    float speed;
    float inputValue;

    Rigidbody2D body;
    Animator anim;
    SpriteRenderer spriter;

    [SerializeField]
    GameObject brake;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        switch (logicType)
        {
            case Type.Velocity:
                body.velocityX = inputValue * speed;
                break;
            // addforce는 제어가 힘들고 가속이 붙어야함
            case Type.AddForce:
                body.AddForceX(inputValue * speed);
                body.totalForce = Vector2.right * Mathf.Min(body.totalForce.x, 10f);
                break;
            // 절대좌표를 제어하기 때문에 y축까지 같이 제어됨(단순이동, 장애물이나 몬스터등에만 사용)
            case Type.MovePosition:
                body.MovePosition(body.position
                    + Vector2.right * inputValue * speed * Time.deltaTime
                    + Vector2.up * Physics2D.gravity.y * Time.deltaTime);
                break;

        }
    }

    private void LateUpdate()
    {
        // Update와 FixedUpdate에서 연산된 값을 처리하기 위해 LateUpdate
        anim.SetFloat("Speed", Mathf.Abs(inputValue));

        if (inputValue != 0)
            spriter.flipX = inputValue < 0;
    }

    void OnMove(InputValue value)
    {
        inputValue = value.Get<Vector2>().x;

        // 경사에서 미끄러지지 않게 하려했으나 작동이 없을 때 공중에 떠버림
        //body.gravityScale = Mathf.Abs(inputValue) * 3;
        brake.SetActive(inputValue == 0);
    }
}
