using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMessages : MonoBehaviour
{
    public TMP_Text textObject;
    Queue<string> queue;
     Coroutine messageCoroutine;

    private void Awake()
    {
        queue = new Queue<string>();
    }

    private void OnEnable()
    {
        StartMessageCoroutine();
    }

    private void OnDisable()
    {
        StopMessageCoroutine();
    }
    
    public void Say(string msg)
    {
        print($"say {msg}");
        queue.Enqueue(msg);
    }

    public void ClearQueue()
    {
        queue.Clear();
        textObject.text = "";
    }


    private void StartMessageCoroutine()
    {
        if (messageCoroutine == null)
        {
            messageCoroutine = StartCoroutine(ProcessMessages());
        }
    }


    private void StopMessageCoroutine()
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
            messageCoroutine = null;
        }
    }

    IEnumerator ProcessMessages()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            if(queue.Count > 0)
            {
                textObject.text = queue.Dequeue();
                yield return new WaitForSeconds(2f);
                textObject.text = "";
            }
        }
    }

}
