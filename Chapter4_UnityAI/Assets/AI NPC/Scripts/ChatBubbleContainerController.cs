using UnityEngine;
using UnityEngine.UI;

public class ChatBubbleContainerController : MonoBehaviour
{
    // 말풍선에 들어갈 텍스트 오브젝트
    public Text targetText;

    // 텍스트를 변경하고 크기를 다시 조절
    public void UpdateText(string t)
    {
        targetText.text = t;        
    }
}
