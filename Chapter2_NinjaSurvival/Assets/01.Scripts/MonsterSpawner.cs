using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject MonsterPrefab;       // 생성할 몬스터 프리팹
    public Transform Player;               // 플레이어 위치 참조
    public float SpawnInterval = 3f;       // 몬스터 생성 간격 (초)
    public float SpawnDistance = 10f;      // 플레이어로부터 몬스터가 생성될 거리
    
    private float _timer = 0f;              // 타이머 변수
    
    void Update()
    {
        // 타이머 증가
        _timer += Time.deltaTime;
        
        // 일정 시간이 지나면 몬스터를 생성합니다
        if (_timer >= SpawnInterval)
        {
            SpawnMonster();
            _timer = 0f;  // 타이머 초기화
        }
    }
    
    void SpawnMonster()
    {
        // 플레이어가 없으면 생성하지 않음
        if (Player == null)
        {
            return;
        }
            
        // 랜덤한 각도 생성 (0 ~ 360도)
        float randomAngle = Random.Range(0f, 360f);
        
        // 각도를 라디안으로 변환
        float radians = randomAngle * Mathf.Deg2Rad;
        
        // 원형 범위 내에서의 위치 계산 (삼각함수 활용)
        float x = Mathf.Cos(radians) * SpawnDistance;
        float y = Mathf.Sin(radians) * SpawnDistance;
        
        // 플레이어 위치를 기준으로 스폰 위치 설정
        Vector3 spawnPosition = Player.position + new Vector3(x, y, 0f);
        
        GameObject monster = Instantiate(MonsterPrefab);
        monster.transform.position = spawnPosition;
    }
}