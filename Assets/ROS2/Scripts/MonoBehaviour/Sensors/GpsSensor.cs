using geometry_msgs.msg;
using rclcs;
using sensor_msgs.msg;

public class GpsSensor : MonoBehaviourRosSensorNode<NavSatFix>
{
    private readonly LocalCoordinateSystem localCoordinateSystem = new LocalCoordinateSystem(
        new LatLong(1.3521, 103.8198));
    
    protected override NavSatFix Read()
    {
        NavSatFix navSatFix = new NavSatFix();
        navSatFix.Header.Update(clock);
        navSatFix.Header.Frame_id = "base_footprint";

        navSatFix.Status.Status = 0; // STATUS_FIX = 0
        navSatFix.Status.Service = 1; // SERVICE_GPS = 1
        
        LatLong latLong = localCoordinateSystem.Convert(transform.position);

        navSatFix.Latitude = latLong.Latitude;
        navSatFix.Longitude = latLong.Longitude;
        navSatFix.Altitude = 0;

        navSatFix.Position_covariance_type = 0; // COVARIANCE_TYPE_UNKNOWN
        
        return navSatFix;
    }
}
