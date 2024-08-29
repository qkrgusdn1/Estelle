using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChatMgr : MonoBehaviour
{
    public Chat chatPrefabMine;              
    public Chat chatPrefabCustomer;           
    public Transform mineChatTransform;       
    public Transform customerChatTransform;   
    public ChatText[] chatText;               

    private List<Chat> mineChatPool = new List<Chat>();       
    private List<Chat> customerChatPool = new List<Chat>();   
    private List<Chat> activeChats = new List<Chat>();         

    private bool mine = true;
    private int chatCount = 0;
    GameType gameType;


    private void Start()
    {
        InitializePool(mineChatPool, chatPrefabMine, 5, mineChatTransform);
        InitializePool(customerChatPool, chatPrefabCustomer, 5, customerChatTransform);
        if(DonDestory.Instance.day == 1)
        {
            gameType = GameType.Ball;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(chatCount < chatText[DonDestory.Instance.day - 1].chatTextsMine.Length)
            {
                HandleChatToggle();
            }
            else if(chatCount >= chatText[DonDestory.Instance.day - 1].chatTextsMine.Length)
            {
                SceneManager.LoadScene(gameType.ToString());
            }
            
        }
    }

    private void HandleChatToggle()
    {
        if (mine)
        {
            DeactivateChats(mineChatPool);
            Chat chat = GetChatFromPool(mineChatPool, mineChatTransform);
            chat.text.text = chatText[DonDestory.Instance.day - 1].chatTextsMine[chatCount];
            chat.animator.Play("Chat");
            activeChats.Add(chat);
            mine = false;
        }
        else
        {
            DeactivateChats(customerChatPool);
            Chat chat = GetChatFromPool(customerChatPool, customerChatTransform);
            chat.text.text = chatText[DonDestory.Instance.day - 1].chatTextsCustomer[chatCount];
            chat.animator.Play("Chat");
            activeChats.Add(chat);
            chatCount++;
            mine = true;
        }
    }

    private void InitializePool(List<Chat> pool, Chat prefab, int size, Transform parent)
    {
        for (int i = 0; i < size; i++)
        {
            Chat chat = Instantiate(prefab, parent);
            chat.gameObject.SetActive(false);
            pool.Add(chat);
        }
    }

    private Chat GetChatFromPool(List<Chat> pool, Transform parent)
    {
        foreach (Chat chat in pool)
        {
            if (!chat.gameObject.activeInHierarchy)
            {
                chat.transform.position = parent.position;
                chat.gameObject.SetActive(true);
                return chat;
            }
        }

        Chat newChat = Instantiate(pool[0], parent);
        pool.Add(newChat);
        return newChat;
    }

    private void DeactivateChats(List<Chat> pool)
    {
        foreach (Chat chat in pool)
        {
            if (chat.gameObject.activeInHierarchy)
            {
                chat.gameObject.SetActive(false);
            }
        }
    }
}
[System.Serializable]
public class ChatText
{
    public string[] chatTextsMine;
    public string[] chatTextsCustomer;
}
public enum GameType
{
    Ball,

}
