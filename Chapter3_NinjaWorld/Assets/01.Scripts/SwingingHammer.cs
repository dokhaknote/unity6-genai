using UnityEngine;

public class SwingingHammer : MonoBehaviour
{
    [Header("회전 설정")]
    public float SwingSpeed = 2.0f; // 흔들리는 속도
    public float MaxAngle = 60.0f; // 최대 회전 각도
    
    // 해머 오브젝트 (회전할 실제 오브젝트)
    public GameObject HammerObject;
    
    // 시간을 기록하는 변수
    private float _timer = 0;
    
    private void Update()
    {
        // 시간 업데이트
        _timer += Time.deltaTime * SwingSpeed;
        
        // 사인 함수를 사용하여 -1에서 1 사이의 값을 얻음
        float swingFactor = Mathf.Sin(_timer);
        
        // 회전 각도 계산 (-MaxAngle에서 MaxAngle 사이)
        float newAngle = swingFactor * MaxAngle;
        
        // 회전 적용 (X축 회전만 변경)
        Vector3 angles = HammerObject.transform.localEulerAngles;
        angles.x = newAngle;
        HammerObject.transform.localEulerAngles = angles;
    }
}