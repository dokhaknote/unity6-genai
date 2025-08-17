using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    // 플레이어 추적을 위한 변수
    private Transform _player;
    
    // 네비게이션 에이전트
    private NavMeshAgent _agent;
    
    // 몬스터 상태
    private bool _isActive = false;
    
    // 트리거 영역 설정
    public float triggerRadius = 6.0f;  // 감지 범위
    
    
    public float KnockbackForce = 10f;  // 튕겨내는 힘
// 파티클 효과 추가
    [Header("파티클 효과")]
    public GameObject CollisionParticle; // 충돌 시 생성할 파티클 프리팹
    
    private void Start()
    {
        // player 게임 오브젝트의 transform 컴포넌트 가져오기
        _player = GameObject.FindWithTag("Player").transform;
        
        // NavMeshAgent 컴포넌트 가져오기
        _agent = GetComponent<NavMeshAgent>();
        
        // 시작 시 추적 비활성화
        _agent.isStopped = true;
    }
    
    private void Update()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        
        // 트리거 범위 내에 플레이어가 들어오면 활성화
        if (!_isActive && distanceToPlayer <= triggerRadius)
        {
            _isActive = true;
            _agent.isStopped = false;
            Debug.Log(gameObject.name + ": 몬스터가 활성화되었습니다!");
        }
        
        // 활성화된 상태일 때만 플레이어 추적
        if (_isActive)
        {
            _agent.SetDestination(_player.position);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Player 태그를 가진 오브젝트와 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            
            if (playerRb != null)
            {
                // 충돌 방향 계산 (몬스터에서 플레이어 방향)
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                
                // 약간의 위쪽 방향 추가
                direction += Vector3.up * 0.5f;
                
                // 플레이어에게 힘 가하기
                playerRb.AddForce(direction * KnockbackForce, ForceMode.Impulse);
                
                // 파티클 효과 생성
                if (CollisionParticle != null)
                {
                    // 몬스터와 플레이어 사이 지점에 파티클 생성
                    Vector3 particlePosition = Vector3.Lerp(transform.position, collision.transform.position, 0.5f);
                    Instantiate(CollisionParticle, particlePosition, Quaternion.identity);
                }
                
                SoundManager.Instance.PlayMonsterHitSfx();
                
                Debug.Log("플레이어가 몬스터와 충돌했습니다!");
            }
        }
    }
}