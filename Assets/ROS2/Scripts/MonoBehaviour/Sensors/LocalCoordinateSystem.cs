using System;
using UnityEngine;

public class LocalCoordinateSystem
{
    private readonly LatLong origin;
    private readonly double metersPerLatitude;
    private readonly double metersPerLongitude;

    public LocalCoordinateSystem(LatLong origin)
    {
        this.origin = origin;

        double latitudeInRadians = origin.Latitude * Math.PI / 180;
        metersPerLatitude = 111132.92 - 559.82 * Math.Cos(2 * latitudeInRadians) + 1.175
            * Math.Cos(4 * latitudeInRadians) - 0.0023 * Math.Cos(6 * latitudeInRadians);
        metersPerLongitude = 111412.84 * Math.Cos(latitudeInRadians) - 93.5 * Math.Cos(3 * latitudeInRadians) + 0.118
            * Math.Cos(5 * latitudeInRadians);
    }

    public LatLong Convert(Vector3 point)
    {
        return Convert(point.x, point.y, point.z);
    }

    public LatLong Convert(double x, double y, double z)
    {
        double latitude = -(x / metersPerLatitude) + origin.Latitude;
        double longitude = (z / metersPerLongitude) + origin.Longitude;
        double altitude = y;
        return new LatLong(latitude, longitude);
    }
}