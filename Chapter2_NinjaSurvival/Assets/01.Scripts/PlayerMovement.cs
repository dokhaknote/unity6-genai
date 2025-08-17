using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;            // 이동 속도 변수
    public int MaxHealth = 5;               // 최대 체력
    public int CurrentHealth;               // 현재 체력
    
    private Rigidbody2D _rigidbody2D;       // 물리 처리를 위한 컴포넌트
    private Vector2 _moveDirection;         // 이동 방향
    private SpriteRenderer _spriteRenderer; // 스프라이트 렌더러 컴포넌트
    private Animator _animator;             // 애니메이터 컴포넌트
    private bool _isDead = false;           // 플레이어 사망 상태 확인 변수

    void Start()
    {
        // 게임 오브젝트에서 Rigidbody2D 컴포넌트를 가져옵니다
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        // 게임 오브젝트에서 SpriteRenderer 컴포넌트를 가져옵니다.
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        // 게임 오브젝트에서 Animator 컴포넌트를 가져옵니다.
        _animator = GetComponent<Animator>();
        
        // 게임 시작 시 현재 체력을 최대 체력으로 설정
        CurrentHealth = MaxHealth;
    }
    
    void Update()
    {
        // 사망 상태면 이동 처리하지 않음
        if (_isDead)
        {
            return;
        }
        
        // 입력 처리만 여기서 진행합니다
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 이동 방향 계산
        _moveDirection = new Vector2(horizontalInput, verticalInput);

        // 대각선 이동 시 속도 정규화
        if (_moveDirection.magnitude > 0)
        {
            _moveDirection.Normalize();
            
            // 플레이어가 움직이고 있으면 애니메이션 상태를 Run으로 변경
            _animator.SetBool("Run", true);
        }
        else
        {
            // 플레이어가 멈추면 애니메이션 상태를 Idle로 변경
            _animator.SetBool("Run", false);
        }
        
        // 이동 방향에 따라 스프라이트 뒤집기
        if (horizontalInput != 0)
        {
            _spriteRenderer.flipX = horizontalInput < 0;
        }
        
        // 테스트용: Space 키를 누르면 Death 애니메이션 재생
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        // 사망 상태면 이동 처리하지 않음
        if (_isDead)
        {
            return;
        }
        
        // 실제 물리 이동은 FixedUpdate에서 처리합니다
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveDirection * MoveSpeed * Time.fixedDeltaTime);
    }

		// 데미지를 받는 함수
    public void TakeDamage(int damageAmount)
    {
        // 이미 사망한 상태라면 데미지를 받지 않음
        if (_isDead)
            return;
            
        // 현재 체력에서 데미지만큼 감소
        CurrentHealth -= damageAmount;
        
        // 현재 체력이 0 이하로 떨어졌다면 사망 처리
        if (CurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            // 추후 피격 애니메이션, 효과음 등을 여기에 추가할 수 있음
            Debug.Log("플레이어가 데미지를 입었습니다. 남은 체력: " + CurrentHealth);
        }
    }
    
    void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Death");
        Debug.Log("플레이어가 사망했습니다!");
    
        // 충돌체를 비활성화하여 더 이상 몬스터와 충돌하지 않도록 함
        GetComponent<Collider2D>().enabled = false;
    
        // 게임 오버 화면 표시
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }

    
}

