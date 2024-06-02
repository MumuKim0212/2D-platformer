using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // 4���� �����̴� ���
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
            // addforce�� ��� ����� ������ �پ����
            case Type.AddForce:
                body.AddForceX(inputValue * speed);
                body.totalForce = Vector2.right * Mathf.Min(body.totalForce.x, 10f);
                break;
            // ������ǥ�� �����ϱ� ������ y����� ���� �����(�ܼ��̵�, ��ֹ��̳� ���͵�� ���)
            case Type.MovePosition:
                body.MovePosition(body.position
                    + Vector2.right * inputValue * speed * Time.deltaTime
                    + Vector2.up * Physics2D.gravity.y * Time.deltaTime);
                break;

        }
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

        // ��翡�� �̲������� �ʰ� �Ϸ������� �۵��� ���� �� ���߿� ������
        //body.gravityScale = Mathf.Abs(inputValue) * 3;
        brake.SetActive(inputValue == 0);
    }
}
