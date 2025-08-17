
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("이동 설정")]
    public float MoveSpeed = 5.0f;// 이동 속도

    [Header("점프 설정")]
    public float JumpForce = 7.0f;// 점프 힘
    public LayerMask GroundLayer;// 지면 레이어
    public float GroundCheckDistance = 0.2f;// 지면 확인 거리

    [Header("컴포넌트")]
    private Rigidbody _rigidbody;// Rigidbody 컴포넌트
    private Animator _animator;// Animator 컴포넌트
    
    // 상태 변수
    private Vector3 _moveDirection;

    private bool _isGrounded;
    private bool _jumpRequested;
    private bool _isJumping;// 점프 중인지 상태 확인

    private void Start()
    {
// 필요한 컴포넌트 가져오기
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

// 컴포넌트 유효성 검사
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody 컴포넌트가 없습니다. Player 오브젝트에 Rigidbody를 추가해주세요.");
        }
        if (_animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 없습니다. Player 오브젝트에 Animator를 추가해주세요.");
        }
    }

    private void Update()
    {
// 키보드 입력 감지
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

// 이동 방향 계산
        _moveDirection = new Vector3(horizontalInput, 0, verticalInput);

// 이동 벡터가 존재하는 경우 정규화
        if (_moveDirection.magnitude > 0.1f)
        {
            _moveDirection.Normalize();
        }

// 달리기 애니메이션 설정 (점프 중이 아닐 때만)
        if (!_isJumping)
        {
            _animator.SetBool("IsRunning", _moveDirection.magnitude > 0.1f);
        }

// 지면 체크
        CheckGrounded();

// 점프 입력 감지 - 지면에 있을 때만 점프 가능
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_isJumping)
        {
            _jumpRequested = true;
            _isJumping = true;
            _animator.SetTrigger("Jump");
        }

// 점프 완료 확인
        if (_isJumping && _isGrounded)
        {
// 점프 애니메이션이 끝나면 자동으로 Idle이나 Run으로 돌아감
            _isJumping = false;
        }
    }

    private void FixedUpdate()
    {
// 물리 기반 이동 처리
        Move();
        Rotate();

// 점프 처리
        if (_jumpRequested)
        {
            Jump();
            _jumpRequested = false;
        }
    }

    private void Move()
    {
// 이동 처리
        _rigidbody.MovePosition(_rigidbody.position +
                              _moveDirection * MoveSpeed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
// 이동 방향이 있는 경우에만 회전
        if (_moveDirection.magnitude > 0.1f)
        {
// 이동 방향으로 즉시 회전
            transform.forward = _moveDirection;
        }
    }

    private void Jump()
    {
        // 점프 힘 적용
        _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        _isGrounded = false;// 점프 직후 지면에서 떨어졌다고 표시
        
        SoundManager.Instance.PlayJumpSfx();
    }

    private void CheckGrounded()
    {
// 플레이어 아래쪽으로 레이캐스트를 쏴서 지면 체크
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;

        _isGrounded = Physics.Raycast(rayStart, Vector3.down, out hit,
                                    GroundCheckDistance + 0.1f, GroundLayer);
    }
}
