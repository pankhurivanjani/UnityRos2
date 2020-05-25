## **Instructions to build and run this experiment**

NOTE: These instructions are for Ubuntu 18.04

# Pre-requisites

OS: Linux and OSX (not tested)

* Docker 19.03.8 (and above)
    * [Ubuntu] https://docs.docker.com/engine/install/ubuntu/
* docker-compose 1.17.1 (and above)
    * https://docs.docker.com/compose/install/
* Unity3D 2019.3.0f6 (and above)
    * https://docs.unity3d.com/Manual/GettingStartedInstallingHub.html
* ROS2
    * [Ubuntu/Debian] https://index.ros.org/doc/ros2/Installation/Dashing/Linux-Install-Debians/
    * Extra packages:
        * `ros-dashing-rmw-cyclonedds-cpp`

# Setup

Before running rviz2, rqt, rqt_graph (on the host only). This is
**NOT REQUIRED** for the Unity3D simulator and the Docker containers.

```
export RMW_IMPLEMENTATION=rmw_cyclonedds_cpp
```

Unset the ROS_DOMAIN_ID (**Optional in case of errors**)

```
unset ROS_DOMAIN_ID

```


# Launch

## ROS2 stack

```
cd docker/turtlebot3_navigation
docker-compose up
```

Press `CTRL-C` to stop.

### Cleanup and reset
```
cd docker/turtlebot3_navigation
docker-compose down
```

## rviz2

```
. /opt/ros/dashing/setup.bash
export RMW_IMPLEMENTATION=rmw_cyclonedds_cpp
rviz2
```

* Create visualizations:
    * Click the `Add` button in the lower-left corner, and
        * Create visualization `By topic`, `/global_costmap`/`/costmap`/`Map`.
            * Name: `Global costmap`
            * Color Scheme: `costmap`
        * Create visualization `By topic`, `/plan`/`Path`.
            * Name: Plan
        * Create visualization `By topic`, `/move_base_simple`/`/goal`/`Pose`.
            * Name: 2D Nav Goal

## Unity3D

* Open the Unity project (select the repository folder).
* In the `Projects` tab (bottom panel), navigate to `Assets/ROS2/Examples/TurtlebotNavigation` and double click on RosNavigationExample.unity
* Press the `Play` button.
* In the `Hierarchy` panel, select `map`, `NavigationGoal`.
    * Move this entity (or change its position via the `Inspector`) and click `Send Navigation Goal` to send a nagivation goal.

## For working with new maps including geofence

* Give gps points in i3.json

* Run geofenceROS2.py

* Use the generated .pgm file in UnityRos2/docker/turtlebot3_navigation/turtlebot3_unity_bringup/launch/test_map.yaml

