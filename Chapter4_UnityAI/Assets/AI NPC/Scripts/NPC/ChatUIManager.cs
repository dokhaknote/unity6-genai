using UnityEngine;
using UnityEngine.UI;

public class ChatUIManager : MonoBehaviour
{
    [Header("입력 UI")]
    public InputField InputField;    // 사용자가 텍스트를 입력하는 창
    public Button SendButton;        // 전송 버튼

    [Header("말풍선 UI")]
    public RectTransform Content;             // 말풍선이 배치될 부모 영역
    public GameObject ChatBubbleContainerLeftPrefab;   // AI 말풍선 프리팹
    public GameObject ChatBubbleContainerRightPrefab;  // 사용자 말풍선 프리팹
    
    // 사용자 말풍선을 화면에 표시합니다.
    public void AddUserBubble()
    {
        // 오른쪽 말풍선 프리팹을 Content 아래에 생성
        GameObject bubbleObject = Instantiate(ChatBubbleContainerRightPrefab, Content);

        // 생성된 말풍선의 컨트롤러를 가져와 텍스트를 업데이트
        ChatBubbleContainerController controller = bubbleObject.GetComponent<ChatBubbleContainerController>();
        controller.UpdateText(InputField.text);
    }

    // AI(NPC) 말풍선을 화면에 표시합니다.
    public void AddNpcBubble(string text)
    {
        // 왼쪽 말풍선 프리팹을 Content 아래에 생성
        GameObject bubbleObject = Instantiate(ChatBubbleContainerLeftPrefab, Content);

        // 생성된 말풍선의 컨트롤러를 가져와 텍스트를 업데이트
        ChatBubbleContainerController controller = bubbleObject.GetComponent<ChatBubbleContainerController>();
        controller.UpdateText(text);
    }

    // 버튼 활성화/비활성화를 제어합니다.
    public void SetButtonInteractable(bool enable)
    {
        SendButton.interactable = enable;
    }

    // 사용자가 입력값을 텍스트를 되돌려줍니다.
    public string GetUserText()
    {
        return InputField.text;
    }

    // 입력창을 초기화 합니다.
    public void ClearUserText()
    {
        // 입력창을 비워 다음 입력 준비
        InputField.text = string.Empty;
    }
}
