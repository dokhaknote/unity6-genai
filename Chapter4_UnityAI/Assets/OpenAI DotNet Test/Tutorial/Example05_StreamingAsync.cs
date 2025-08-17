using OpenAI.Chat;
using System.ClientModel;
using UnityEngine;

public class Example05_StreamingAsync : MonoBehaviour
{
    void Start()
    {
        // ChatClient 인스턴스를 생성합니다.
        ChatClient client = new ChatClient(model: "gpt-4o", apiKey: "Your OpenAI Key");

        // AI 응답을 스트리밍 방식으로 요청합니다.
        CollectionResult<StreamingChatCompletionUpdate> completionUpdates = client.CompleteChatStreaming("안녕하세요!");

        // 스트리밍 응답을 하나씩 받아 처리합니다.
        foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
        {
            // AI가 새로운 텍스트를 생성했는지 확인합니다.
            if (completionUpdate.ContentUpdate.Count > 0)
            {
                // 생성된 텍스트를 Unity 콘솔에 출력합니다.
                Debug.Log(completionUpdate.ContentUpdate[0].Text);
            }
        }
    }
}
