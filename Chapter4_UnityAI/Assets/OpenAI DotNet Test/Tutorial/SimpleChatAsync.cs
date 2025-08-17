using OpenAI.Chat;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleChatAsync : MonoBehaviour
{
    async void Start()
    {
        await GetChatRespnesAsync();
    }
    async Task GetChatRespnesAsync()
    {
        // ChatClient 인스턴스를 생성합니다.
        ChatClient client = new ChatClient(model: "gpt-4o", apiKey: "Your OpenAI Key");

        // AI 응답을 요청합니다.
        ChatCompletion completion = await client.CompleteChatAsync("안녕하세요!");

        // AI 응답을 출력합니다
        Debug.Log(completion.Content[0].Text);
    }
}
