using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;  // 점수 텍스트 UI 요소
    public Text HighScoreText;    // 최고 점수 텍스트

    private int _currentScore = 0;  // 현재 점수
    private int _highScore = 0;     // 최고 점수
    
    // PlayerPrefs에서 사용할 키
    private const string HIGH_SCORE_KEY = "HighScore";

    void Start()
    {
        // 저장된 최고 점수 불러오기
        _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        
        // UI 초기화
        UpdateScoreUI();
    }
    
    // 점수 추가 함수
    public void AddScore(int points)
    {
        _currentScore += points;
        
        // 최고 점수 갱신 확인
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, _highScore);
            PlayerPrefs.Save();  // 즉시 저장
        }
        
        UpdateScoreUI();    }
    
    // 점수 UI 업데이트 함수
    private void UpdateScoreUI()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "점수: " + _currentScore;
        }
        
        if (HighScoreText != null)
        {
            HighScoreText.text = "최고 점수: " + _highScore;
        }
    }
}