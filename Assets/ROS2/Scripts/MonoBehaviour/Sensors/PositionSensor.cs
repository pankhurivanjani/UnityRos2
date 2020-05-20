using geometry_msgs.msg;
using rclcs;

public class PositionSensor : MonoBehaviourRosSensorNode<PoseWithCovarianceStamped>
{
    protected override PoseWithCovarianceStamped Read()
    {
        PoseWithCovarianceStamped poseWithCovarianceStamped = new PoseWithCovarianceStamped();
        poseWithCovarianceStamped.Header.Update(clock);
        poseWithCovarianceStamped.Header.Frame_id = "base_footprint";

        PoseWithCovariance poseWithCovariance = poseWithCovarianceStamped.Pose;

        Pose pose = poseWithCovariance.Pose;
        pose.Position.Unity2Ros(transform.position);
        pose.Orientation.Unity2Ros(transform.rotation);
        
        return poseWithCovarianceStamped;
    }
}