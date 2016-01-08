# The Column

The Column is a 3D simulation of varying sized and mass balls falling down a tube. Inside the tube there are several obstacles in the way that the balls interact with. The balls are affected by a gravity force that keeps them falling down the tube and bouncing around.

The first obstacle is the axis aligned cylinders. Some are larger than the others but they all have the same principle, an easy collision box around them that the balls can collide and bounce off at, depending on their approach and speed.

The second obstacle is the non-axis aligned cylinders. A simple bounding box doesn't cut it for these and instead an algorithm to calculate the exact angle and positions was made. The balls bounce off of these in the same way as the axis aligned cylinders.

The third obstacle is the death ball. The balls collide with the deathball but rather than bouncing off it they instead continue to try and fall down it. Meanwhile the deathball is "eating away" at the balls continuously shrinking their size. If the ball gets too small then it bursts and is deleted from the simulation at which point a new ball is spawned at the top to replace it. However, the ball does have a chance to get past the deathball after being shrunk a certain amount. Originally, the ball may not have been able to get past but after shrinking a bit it may then be able to get past and continue on.

The final part of the column is a teleporter. The teleporter instantly moves the ball entering it back to the top of the column. The ball retains its velocity and size when coming out of the teleporter back at the top allowing it to keep going and keep gaining momentum.

There are several cameras in the program:
-An fps-like free camera
-A static camera that allows you to see the entire column
-A scrolling camera that scrolls down the column slowly and loops back to the top when reaching the bottom
-A follow camera that follows and is locked to one of the balls

The program was created in OpenGL using OpenTK and C#. The shaders were done using GLSL that all the textured objects use.
