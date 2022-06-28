using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebManager : Singleton<WebManager>
{
    private readonly Queue<WebBaseTask> queue;

    private WebManager()
    {
        queue = new();
    }

    public void AddTask(WebBaseTask task)
    {
        queue.Enqueue(task);
        if (queue.Count == 1)
        {
            Main.Instance.StartCoroutine(Download());
        }
    }

    public IEnumerator Download()
    {
        while (queue.Count > 0)
        {
            WebBaseTask task = queue.Dequeue();
            yield return task.Download();
        }
    }
}
