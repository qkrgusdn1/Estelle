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

    private int mineChatCount = 0;      // 마인의 채팅 카운트
    private int customerChatCount = 0;  // 커스터머의 채팅 카운트
    public int spawnChatCount;

    private int chatOrderIndex = 0;     // 현재 차례의 인덱스
    private int currentRepeatCount = 0; // 현재 말하는 차례에서의 반복 횟수

    public GameObject pool;
    public GameObject bear;

    private void Start()
    {
        InitializePool(mineChatPool, chatPrefabMine, 5, mineChatTransform);
        InitializePool(customerChatPool, chatPrefabCustomer, 5, customerChatTransform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var currentChatText = chatText[DonDestory.Instance.day - 1];
            if (mineChatCount < currentChatText.chatTextsMine.Length || customerChatCount < currentChatText.chatTextsCustomer.Length)
            {
                HandleChatToggle();
            }
            else
            {
                if (DonDestory.Instance.day == 1)
                {
                    pool.SetActive(true);
                }
                else if (DonDestory.Instance.day == 2)
                {
                    bear.SetActive(true);
                }
            }
        }
    }

    private void HandleChatToggle()
    {
        var currentChatText = chatText[DonDestory.Instance.day - 1]; // 현재 day's ChatText

        // 현재 차례에 따라 마인 또는 고객이 말하는 로직
        int currentChatOrder = currentChatText.chatOrder[chatOrderIndex];

        if (currentRepeatCount < currentChatOrder)
        {
            if (chatOrderIndex % 2 == 0) // 마인의 차례
            {
                DeactivateChats(mineChatPool);
                Chat chat = GetChatFromPool(mineChatPool, mineChatTransform);
                chat.text.text = currentChatText.chatTextsMine[mineChatCount]; // 마인 채팅 텍스트
                chat.AdjustWidthBasedOnText();
                chat.animator.Play("Chat");
                activeChats.Add(chat);
                currentRepeatCount++;
                mineChatCount++;  // 마인 채팅 카운트 증가
            }
            else // 고객의 차례
            {
                DeactivateChats(customerChatPool);
                Chat chat = GetChatFromPool(customerChatPool, customerChatTransform);
                chat.text.text = currentChatText.chatTextsCustomer[customerChatCount]; // 고객 채팅 텍스트
                chat.AdjustWidthBasedOnText();
                chat.animator.Play("Chat");
                activeChats.Add(chat);
                currentRepeatCount++;
                customerChatCount++;  // 고객 채팅 카운트 증가
            }
        }

        if (currentRepeatCount >= currentChatOrder)
        {
            currentRepeatCount = 0; // 반복 횟수 초기화
            chatOrderIndex++; // 다음 차례로 이동
        }

        // chatOrderIndex가 마지막 차례를 넘지 않도록 처리
        if (chatOrderIndex >= currentChatText.chatOrder.Count)
        {
            chatOrderIndex = 0;
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
    public string[] chatTextsMine;      // 마인의 채팅 텍스트 배열
    public string[] chatTextsCustomer;  // 고객의 채팅 텍스트 배열
    public List<int> chatOrder = new List<int>(); // 말할 순서 리스트 (1, 2, 3 등)
}

public enum GameType
{
    Ball,
    Fall
}
