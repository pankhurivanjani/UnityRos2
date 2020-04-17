using System;
using rclcs;
using sensor_msgs.msg;
using UnityEngine;
using Quaternion = geometry_msgs.msg.Quaternion;

public class ImuSensor : MonoBehaviourRosSensorNode<Imu>
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

    private Quaternion fromRPY(double roll, double pitch, double yaw)
    {
        double qx = Math.Sin(roll / 2) * Math.Cos(pitch / 2) * Math.Cos(yaw / 2) -
             Math.Cos(roll / 2) * Math.Sin(pitch / 2) * Math.Sin(yaw / 2);
        double qy = Math.Cos(roll / 2) * Math.Sin(pitch / 2) * Math.Cos(yaw / 2) +
             Math.Sin(roll / 2) * Math.Cos(pitch / 2) * Math.Sin(yaw / 2);
        double qz = Math.Cos(roll / 2) * Math.Cos(pitch / 2) * Math.Sin(yaw / 2) -
             Math.Sin(roll / 2) * Math.Sin(pitch / 2) * Math.Cos(yaw / 2);
        double qw = Math.Cos(roll / 2) * Math.Cos(pitch / 2) * Math.Cos(yaw / 2) +
             Math.Sin(roll / 2) * Math.Sin(pitch / 2) * Math.Sin(yaw / 2);

        return new Quaternion {X = qx, Y = qy, Z = qz, W = qw};
    }

    private double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
    
    protected override Imu Read()
    {
        if (twistBaseController == null)
        {
            return null;
        }

        Imu imu = new Imu();
        imu.Header.Update(clock);
        imu.Header.Frame_id = "base_footprint";

        double heading = transform.rotation.eulerAngles.y;
        while (heading >= 360)
        {
            heading -= 360;
        }

        heading = 360 - heading;

        imu.Orientation = fromRPY(ToRadians(0), ToRadians(0), ToRadians(heading));

        imu.Angular_velocity.Unity2Ros(twistBaseController.commandVelocityAngular);

        imu.Linear_acceleration_covariance[0] = -1;

        return imu;
    }
}
