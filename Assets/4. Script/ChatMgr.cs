using System;
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
    public int removeCustomerChat;
    public int spawnChatCount;


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
        if(spawnChatCount > 2)
        {
            removeCustomerChat++;
        }
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
            chat.remove = false;
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
                chat.remove = false;
                spawnChatCount++;

                chat.animator.Rebind();
                chat.animator.Update(0f);

                chat.gameObject.SetActive(true);
                return chat;
            }
        }

        Chat newChat = Instantiate(pool[0], parent);
        newChat.remove = false;
        newChat.transform.position = parent.position;

        newChat.animator.Rebind();
        newChat.animator.Update(0f);

        pool.Add(newChat);
        return newChat;
    }

    private void DeactivateChats(List<Chat> pool)
    {
        foreach (Chat chat in pool)
        {
            if (chat.gameObject.activeInHierarchy)
            {
                if (chat.remove)
                {
                    StartCoroutine(CoChatUpAndDeactivate(chat, 310, 0.9f));
                    chat.remove = false;
                }
                else
                {
                    chat.remove = true;
                    StartCoroutine(CoChatUp(chat, 255, 0.4f));
                }
            }
        }
    }

    private IEnumerator CoChatUp(Chat chat, float upTime, float time)
    {
        float duration = 0.4f;
        float elapsedTime = 0f;

        Vector3 startPosition = chat.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + upTime, startPosition.z);

        while (elapsedTime < duration)
        {
            chat.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            chat.animator.Play("ChatUp");
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        chat.transform.position = endPosition;
    }

    private IEnumerator CoChatUpAndDeactivate(Chat chat, float upTime, float time)
    {
        yield return CoChatUp(chat, upTime, time);

        chat.gameObject.SetActive(false);
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
