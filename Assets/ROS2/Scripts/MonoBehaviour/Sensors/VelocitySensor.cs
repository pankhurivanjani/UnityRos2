using geometry_msgs.msg;
using rclcs;
using UnityEngine;

public class VelocitySensor : MonoBehaviourRosSensorNode<TwistWithCovarianceStamped>
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

    protected override TwistWithCovarianceStamped Read()
    {
        if (twistBaseController == null)
        {
            return null;
        }

        TwistWithCovarianceStamped twistWithCovarianceStamped = new TwistWithCovarianceStamped();
        twistWithCovarianceStamped.Header.Update(clock);
        twistWithCovarianceStamped.Header.Frame_id = "base_footprint";

        TwistWithCovariance twistWithCovariance = twistWithCovarianceStamped.Twist;

        Twist twist = twistWithCovariance.Twist;
        twist.Linear.Unity2Ros(twistBaseController.commandVelocityLinear);
        twist.Angular.Unity2Ros(twistBaseController.commandVelocityAngular);

        return twistWithCovarianceStamped;
    }
}