using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float Speed = 10f;      // 표창 이동 속도
    public int Damage = 1;         // 데미지 양
    public float LifeTime = 3f;    // 표창 지속 시간 (초)
    
    private Vector2 _direction;     // 이동 방향
    private float _timer = 0f;      // 지속 시간 타이머
    private float _rotateSpeed = 720f; // 회전 속도 (초당 각도)
    
    // 표창 초기화 (방향 설정)
    public void Initialize(Vector2 direction)
    {
        _direction = direction.normalized;
    }
    
    void Update()
    {
        // 표창 회전 (시각적 효과)
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
        
        // 지정된 방향으로 이동
        transform.Translate(_direction * Speed * Time.deltaTime, Space.World);
        
        // 지속 시간 체크
        _timer += Time.deltaTime;
        if (_timer >= LifeTime)
        {
            Destroy(gameObject);  // 지속 시간이 지나면 제거
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 대상이 몬스터인지 확인
        if (collision.CompareTag("Monster"))
        {
            // 몬스터에게 데미지 주기
            MonsterController monster = collision.GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.TakeDamage(Damage);
            }
            
            // 표창 제거 (관통하지 않음)
            Destroy(gameObject);
        }
    }
}