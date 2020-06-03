##UnityROS2 folder is inside codes

#Submodules UnityROS2

* In case issue in downloading the the codes look into [git submodules](https://git-scm.com/book/en/v2/Git-Tools-Submodules)





#Installation and setup on Nvidia Jetson Nano 

* Download latest nvidia jetson ubuntu 18.04 image 

* Using etcher load this image in micro sd card

* Insert the SD card into Jetson Nano, attach computer display and keyboard, mouse

* Make sure to use power supply of atleast (5V, 2A) 

* Boot the board and log into it

#ROS2 Installation on Jetson Nano

```
sudo locale-gen en_US en_US.UTF-8
sudo update-locale LC_ALL=en_US.UTF-8 LANG=en_US.UTF-8
export LANG=en_US.UTF-8

sudo apt update && sudo apt install curl gnupg2 lsb-release
curl -s https://raw.githubusercontent.com/ros/rosdistro/master/ros.asc | sudo apt-key add -

sudo sh -c 'echo "deb http://packages.ros.org/ros2/ubuntu `lsb_release -cs` main" > /etc/apt/sources.list.d/ros2-latest.list'

sudo apt update

sudo apt install ros-dashing-desktop

```

```

sudo apt install ros-dashing-ros-base #optional instead of above installation- No GUI tools.

source /opt/ros/dashing/setup.bash

sudo apt install python3-argcomplete #optional - command autocompletetion


```
 
##################To run the project, follow these instructions############################

* Switch to experiment branch in Unity ROS2 from codes

## **Instructions to build and run this experiment**

NOTE: These instructions are for Ubuntu 18.04



# Installation on Desktop PC (Host)

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

# More installation on Jetson Nan

OS: Linux and OSX (not tested)

* Docker 19.03.8 (and above)
    * [Ubuntu] https://docs.docker.com/engine/install/ubuntu/
* docker-compose 1.17.1 (and above)
    * https://docs.docker.com/compose/install/

* Extra packages:
        * `ros-dashing-rmw-cyclonedds-cpp`

*Note:Docker can be avoided in later stages to save memory on Jetson Nano*

# Setup for Desktop PC

Before running rviz2, rqt, rqt_graph (on the host only). This is
**NOT REQUIRED** for the Unity3D simulator and the Docker containers.

```
export RMW_IMPLEMENTATION=rmw_cyclonedds_cpp
```

Set the required ROS_DOMAIN_ID (**testing required**)

```
unset ROS_DOMAIN_ID 

```

#Setup for Jetson

```
export RMW_IMPLEMENTATION=rmw_cyclonedds_cpp
```

Set the required ROS_DOMAIN_ID (**testing required**)

```
unset ROS_DOMAIN_ID 

```

*Note: ROS_DOMAIN_ID for setting up communication between Jetson and Host PC needs to be checked*


# Launch

## ROS2 stack (on Jetson)

```
cd docker/turtlebot3_navigation
docker-compose up
```

Press `CTRL-C` to stop.

### Cleanup and reset (on Jetson)
```
cd docker/turtlebot3_navigation
docker-compose down
```

## rviz2 (Host PC)

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

## Unity3D (Host PC) 

* Open the Unity project (select the repository folder).
* In the `Projects` tab (bottom panel), navigate to `Assets/ROS2/Examples/TurtlebotNavigation` and double click on RosNavigationExample.unity
* Press the `Play` button.
* In the `Hierarchy` panel, select `map`, `NavigationGoal`.
    * Move this entity (or change its position via the `Inspector`) and click `Send Navigation Goal` to send a nagivation goal.

## For working with new maps including geofence (Jetson Nano)

* Give gps points in i3.json

* Run geofenceROS2.py

* Use the generated .pgm file in UnityRos2/docker/turtlebot3_navigation/turtlebot3_unity_bringup/launch/test_map.yaml


*Note: The idea is to run Unity Simulator and rviz on Host PC and navigation stack on Jetson Nano, the communication between both can be established by using proper ROS_DOMAIN_ID for both, it has not been tested yet due to lack of hardware*
