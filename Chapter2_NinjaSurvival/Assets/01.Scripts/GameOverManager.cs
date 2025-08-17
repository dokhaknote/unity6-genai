using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameOverManager : MonoBehaviour
{
    public Canvas GameOverCanvas;  // 게임 오버 캔버스
    public Button RestartButton;   // 다시 시작 버튼
    
    private AudioSource _audioSource;  // 효과음 재생용

    
    void Start()
    {
        // 시작 시 게임 오버 화면 비활성화
        if (GameOverCanvas != null)
        {
            GameOverCanvas.gameObject.SetActive(false);
        }
        
        // 다시 시작 버튼에 이벤트 추가
        if (RestartButton != null)
        {
            RestartButton.onClick.AddListener(RestartGame);
        }
        
        // AudioSource 가져오기
        _audioSource = GetComponent<AudioSource>();
    }
    
    // 게임 오버 화면 표시
    public void ShowGameOver()
    {
        if (GameOverCanvas != null)
        {
            GameOverCanvas.gameObject.SetActive(true);
        }
        
        // 게임 오버 효과음 재생
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
        
        // 게임 일시 정지
        Time.timeScale = 0f;
    }
    
    // 게임 다시 시작
    public void RestartGame()
    {
        // 시간 스케일 복원
        Time.timeScale = 1f;
        
        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}