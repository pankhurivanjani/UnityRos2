using System;
using rclcs;
using ROS2.Interfaces;

public class MessageWrapper<T> : Object where T : Message
{
    public T Message;
    public RosTime Timestamp;

    public MessageWrapper(T message, RosTime timestamp)
    {
        Message = message;
        Timestamp = timestamp;
    }
}
