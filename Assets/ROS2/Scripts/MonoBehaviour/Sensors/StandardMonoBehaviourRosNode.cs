using System.Collections;
using System.Collections.Generic;
using rclcs;
using ROS2.Interfaces;
using UnityEngine;

public abstract class StandardMonoBehaviourRosNode<T> : MonoBehaviourRosNode where T : Message, new()
{
    public string NodeName;
    public string Topic;
    public float ReadFrequency = 10.0f;
    public float PublisherDelay = 0.1f; // To prevent lookup into future tf2 errors

    private bool shouldRead;
    private Publisher<T> msgPublisher;
    private T lastSentMsg;
    private Queue<MessageWrapper<T>> msgQueue;

    protected abstract T Read();

    protected override string nodeName => NodeName;

    protected override void StartRos()
    {
        msgQueue = new Queue<MessageWrapper<T>>();
        msgPublisher = node.CreatePublisher<T>(Topic);

        StartCoroutine("TriggerRead");
        StartCoroutine("PublishReadingsIfOldEnough");
    }

    IEnumerator TriggerRead()
    {
        for (;;)
        {
            shouldRead = true;
            yield return new WaitForSeconds(1.0f / ReadFrequency);
        }
    }

    IEnumerator PublishReadingsIfOldEnough()
    {
        for (;;)
        {
            if ((msgQueue.Count > 0) && msgQueue.Peek().Timestamp.Delay(PublisherDelay).IsInThePast)
            {
                lastSentMsg = msgQueue.Dequeue().Message;
                msgPublisher.Publish(lastSentMsg);
            }

            yield return new WaitForSeconds(0.1f / ReadFrequency);
        }
    }

    private void Update()
    {
        if (shouldRead)
        {
            shouldRead = false;
            T message = Read();
            if (message != null)
            {
                msgQueue.Enqueue(new MessageWrapper<T>(message, clock.Now));
            }
        }
    }
}
