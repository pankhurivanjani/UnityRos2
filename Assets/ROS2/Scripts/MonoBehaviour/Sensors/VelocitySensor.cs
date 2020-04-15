using rclcs;
using UnityEngine;

public class VelocitySensor : StandardMonoBehaviourRosNode<geometry_msgs.msg.Twist>
{
    private TwistBaseController twistBaseController;

    protected override void StartRos()
    {
        twistBaseController = GetComponent<TwistBaseController>();
        if (twistBaseController == null)
        {
            Debug.LogWarning("TwistBaseController not found on GameObject {name}");
        }

        base.StartRos();
    }

    protected override geometry_msgs.msg.Twist Read()
    {
        if (twistBaseController == null)
        {
            return null;
        }

        geometry_msgs.msg.Twist twist = new geometry_msgs.msg.Twist();
        twist.Linear.Unity2Ros(twistBaseController.commandVelocityLinear);
        twist.Angular.Unity2Ros(twistBaseController.commandVelocityAngular);
        return twist;
    }
}