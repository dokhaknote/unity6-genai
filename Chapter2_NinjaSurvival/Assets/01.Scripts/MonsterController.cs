using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public float MoveSpeed = 2f;            // 몬스터 이동 속도
    public int DamageAmount = 1;           // 플레이어에게 줄 데미지 양
    public int MaxHealth = 3;              // 몬스터 최대 체력
    public int CurrentHealth;              // 현재 체력
    private bool _isDead = false;          // 사망 상태 확인 변수

        
    // 효과음을 위한 AudioSource 추가
    private AudioSource _audioSource;
    public AudioClip hitSound;
    
    public int scoreValue = 100;  // 몬스터 처치 시 획득 점수
    
    private ScoreManager _scoreManager;  // 점수 관리자 참조
    
    private Transform _target;               // 추적할 대상 (플레이어)
    private Rigidbody2D _rigidbody2D;        // 물리 처리를 위한 컴포넌트
    private SpriteRenderer _spriteRenderer;  // 스프라이트 반전을 위한 컴포넌트
    
    void Start()
    {
        CurrentHealth = MaxHealth;
        
        // 컴포넌트 가져오기
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        // 플레이어 찾기 (Player 태그가 있어야 함)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _target = player.transform;
        }
        else
        {
            Debug.LogError("플레이어를 찾을 수 없습니다. Player 태그가 설정되어 있는지 확인하세요.");
        }
        
        // ScoreManager 찾기
        _scoreManager = FindObjectOfType<ScoreManager>();
        if (_scoreManager == null)
        {
            Debug.LogWarning("ScoreManager를 찾을 수 없습니다!");
        }
        
        // AudioSource 컴포넌트 가져오기
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            // 없으면 추가
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void FixedUpdate()
    {
        // 사망 상태면 이동하지 않음
        if (_isDead || _target == null)
        {
            return;
        }
            
        // 플레이어 방향으로 이동
        Vector2 direction = (_target.position - transform.position).normalized;
        _rigidbody2D.MovePosition(_rigidbody2D.position + direction * MoveSpeed * Time.fixedDeltaTime);
        
        // 이동 방향에 따라 스프라이트 반전
        if (direction.x > 0)
        {
            _spriteRenderer.flipX = false;  // 오른쪽으로 이동 시 원래 방향
        }
        else if (direction.x < 0)
        {
            _spriteRenderer.flipX = true;   // 왼쪽으로 이동 시 스프라이트 반전
        }
    }
    
    // 플레이어와 충돌했을 때 호출되는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 컴포넌트 가져오기
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            
            // 플레이어가 존재한다면 데미지 주기
            if (player != null)
            {
                player.TakeDamage(DamageAmount);
            }
            
            // 몬스터 비활성화 (충돌 후 사라짐)
            gameObject.SetActive(false);
        }
    }
    
    // 몬스터가 데미지를 받는 함수
    public void TakeDamage(int damageAmount)
    {
        // 이미 사망했다면 데미지를 받지 않음
        if (_isDead)
            return;
            
        // 현재 체력 감소
        CurrentHealth -= damageAmount;
        
        // 디버그 로그로 확인
        Debug.Log(gameObject.name + "이(가) " + damageAmount + "의 데미지를 입었습니다. 남은 체력: " + CurrentHealth);
        
        // 체력이 0 이하로 떨어졌다면 사망 처리
        if (CurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            // 효과음 재생
            if (_audioSource != null && hitSound != null)
            {
                _audioSource.PlayOneShot(hitSound);
            }
            
            // 피격 효과를 추가할 수 있습니다
            StartCoroutine(HitEffect());
        }
    }
    
    // 몬스터 사망 처리 함수
    private void Die()
    {
        _isDead = true;
        
        // 디버그 로그로 확인
        Debug.Log(gameObject.name + "이(가) 사망했습니다!");
        
        // 점수 추가
        if (_scoreManager != null)
        {
            _scoreManager.AddScore(scoreValue);
        }
        
        // 게임 오브젝트 비활성화 (사망 시 바로 사라짐)
        gameObject.SetActive(false);
    }
    
    // 히트 효과를 위한 코루틴
    private IEnumerator HitEffect()
    {
        // 스프라이트 색상을 빨간색으로 변경 (피격 효과)
        _spriteRenderer.color = Color.red;
        
        // 0.2초 대기
        yield return new WaitForSeconds(0.2f);
        
        // 스프라이트 색상을 원래대로 복원
        _spriteRenderer.color = Color.white;
    }
}
