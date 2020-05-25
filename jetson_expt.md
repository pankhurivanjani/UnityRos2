##UnityROS2 folder is inside codes

#Submodules UnityROS2

* In case issue in downloading the the codes look into [git submodules](https://git-scm.com/book/en/v2/Git-Tools-Submodules)

#Youtube video link for the experiment



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
 
#To run the project, follow instructions on Experiments.md


* The docker side will run on jetson nano and unity hub will act as a simulator working on master PC. They should be on same network #not tested


