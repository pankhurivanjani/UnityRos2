using rclcs;

public class PositionSensor : StandardMonoBehaviourRosNode<geometry_msgs.msg.Pose>
{
    protected override geometry_msgs.msg.Pose Read()
    {
        geometry_msgs.msg.Pose pose = new geometry_msgs.msg.Pose();
        pose.Position.Unity2Ros(transform.position);
        pose.Orientation.Unity2Ros(transform.rotation);
        return pose;
    }
}