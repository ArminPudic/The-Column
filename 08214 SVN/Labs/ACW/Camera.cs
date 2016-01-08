using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Labs.ACW
{
    public class Camera
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 Orientation = new Vector3((float)Math.PI, 0f, 0f);
        public float MoveSpeed = 0.1f;
        public float MouseSensitivity = 0.01f;

        /// <summary>
        /// Very simple, gets the viewing matrix for the camera
        /// </summary>
        /// <returns>Martrix4.LookAt - The view matrix for the camera</returns>
        public Matrix4 GetViewMatrix()
        {
            Vector3 lookat = new Vector3();

            // Creates all the views of all the axies
            lookat.X = (float)(Math.Sin((float)Orientation.X) * Math.Cos((float)Orientation.Y));
            lookat.Y = (float)Math.Sin((float)Orientation.Y);
            lookat.Z = (float)(Math.Cos((float)Orientation.X) * Math.Cos((float)Orientation.Y));

            return Matrix4.LookAt(Position, Position + lookat, Vector3.UnitY);
        }

        /// <summary>
        /// This function handles the movement of the camera based on OnKeyPress() method in ACWWindow.cs
        /// </summary>
        /// <param name="x">The x axis movement value</param>
        /// <param name="y">The y axis movement value</param>
        /// <param name="z">The z axis movement value</param>
        public void Move(float x, float y, float z)
        {
            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float)Math.Sin((float)Orientation.X), 0, (float)Math.Cos((float)Orientation.X)); // Forward and backward movement on the x and z axis
            Vector3 right = new Vector3(-forward.Z, 0, forward.X); // Right and left movement on the x and z axis

            // Actually moves the camera using the parameter values
            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, MoveSpeed);

            // Sets the new position of the camera based on the above calculations
            Position += offset;
        }

        /// <summary>
        /// This function locks the camera on a downwards scrolling path
        /// </summary>
        public void Scrolling()
        {
            Vector3 offset = new Vector3(0f, -0.02f, 0f); // Constant downard movement
            Position += offset;
            if (Position.Y <= -4f || Position.Z < 2 || Position.Z > 2 || Position.X < 0 || Position.X > 0) // Resets the camera if it reaches the bottom or moves away from path
            {
                Position = new Vector3(0f, 0f, 2f);
                Orientation = new Vector3((float)Math.PI, 0f, 0f);
            }
        }

        /// <summary>
        /// This function locks the camera at a static position
        /// </summary>
        public void StaticCam()
        {
            Position = new Vector3(0f, -2f, 3f);
            Orientation = new Vector3((float)Math.PI, 0f, 0f);
        }

        /// <summary>
        /// This function sets the position of the camera to follow a ball
        /// </summary>
        /// <param name="ballPosition">The positional vector of the ball that camera follows</param>
        public void FollowingCam(Vector3 ballPosition)
        {
            ballPosition = ballPosition + new Vector3(0f, 0f, 0.5f);
            Position = ballPosition;
            Orientation = new Vector3((float)Math.PI, 0f, 0f);
        }

        /// <summary>
        /// Simple method that takes in the delta from the mouse gained from the if(Focused) statement in the OnUpdateFrame() method in ACWWindow.cs
        /// </summary>
        /// <param name="x">The x axis rotation value</param>
        /// <param name="y">The y axis rotation value</param>
        public void AddRotation(float x, float y)
        {
            x = x * MouseSensitivity;
            y = y * MouseSensitivity;

            // Sets the new orientation of the camera based on the parameter values given
            Orientation.X = (Orientation.X + x) % ((float)Math.PI * 2.0f);
            Orientation.Y = Math.Max(Math.Min(Orientation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }
    }
}
