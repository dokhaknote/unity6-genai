using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Player 태그를 가진 오브젝트만 체크
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 데스존에 들어왔습니다!");
            
            // 게임 매니저에 플레이어 사망 알림
            GameManager.Instance.PlayerDied();
        }
    }
}