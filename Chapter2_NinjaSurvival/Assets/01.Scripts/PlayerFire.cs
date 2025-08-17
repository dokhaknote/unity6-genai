using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject ShurikenPrefab;     // 표창 프리팹
    public float FireRate = 1f;          // 발사 주기 (초)
    public Transform FirePoint;           // 발사 위치 (없으면 자기 자신)
    
    private MonsterScanner _scanner;        // 적 탐지용 스캐너
    private float _timer = 0f;            // 발사 타이머
    
    void Start()
    {
        // 스캐너 컴포넌트 가져오기
        _scanner = GetComponent<MonsterScanner>();
        
        // 스캐너가 없으면 경고
        if (_scanner == null)
        {
            Debug.LogError("MonsterScanner 컴포넌트가 없습니다!");
        }
        
        // 발사 위치가 지정되지 않았으면 자기 자신으로 설정
        if (FirePoint == null)
        {
            FirePoint = transform;
        }
    }
    
    void Update()
    {
        // 타이머 증가
        _timer += Time.deltaTime;
        
        // 발사 주기마다 표창 발사
        if (_timer >= FireRate)
        {
            FireShuriken();
            _timer = 0f;  // 타이머 초기화
        }
    }
    
    void FireShuriken()
    {
        // 가장 가까운 몬스터 탐색
        Transform targetEnemy = _scanner.GetNearestEnemy();
        
        // 몬스터가 없으면 발사하지 않음
        if (targetEnemy == null)
            return;
        
        // 몬스터 방향 계산
        Vector2 direction = (targetEnemy.position - FirePoint.position).normalized;
        
        // 표창 생성
        GameObject shuriken = Instantiate(ShurikenPrefab, FirePoint.position, Quaternion.identity);
        
        // 표창 초기화
        Shuriken shurikenComponent = shuriken.GetComponent<Shuriken>();
        if (shurikenComponent != null)
        {
            shurikenComponent.Initialize(direction);
        }
    }
}