using Labs.Utility;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace Labs.ACW
{
    /// <summary>
    /// The main cube class which all sub cube classes inherit from
    /// </summary>
    public class Cube : Objects
    {
        /// <summary>
        /// Constructor for cube, sets the vertex, index and colour data for the cube
        /// </summary>
        public Cube()
        {
            VertCount = 8;
            IndiceCount = 36;
            ColorDataCount = 8;
        }

        /// <summary>
        /// This function is used by the TexturedCube classes
        /// </summary>
        /// <returns></returns>
        public override Vector2[] GetTextureCoords()
        {
            return new Vector2[] { };
        }

        /// <summary>
        /// Simple function that sets all the verticies for the cube
        /// </summary>
        /// <returns>Vector3[] - An array with all the verticies of the cube</returns>
        public override Vector3[] GetVerts()
        {
            return new Vector3[] {new Vector3(-0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, 0.5f,  0.5f),
                new Vector3(-0.5f, 0.5f,  0.5f),
            };
        }

        /// <summary>
        /// Another simple function that sets all the indicies for the cube
        /// </summary>
        /// <param name="offset">Number of vertices buffered before this object</param>
        /// <returns>inds - The indicies of the cube</returns>
        public override int[] GetIndices(int offset = 0)
        {
            int[] inds = new int[] {
                //left
                0, 2, 1,
                0, 3, 2,
                //back
                1, 2, 6,
                6, 5, 1,
                //right
                4, 5, 6,
                6, 7, 4,
                //top
                2, 3, 6,
                6, 3, 7,
                //front
                0, 7, 3,
                0, 4, 7,
                //bottom
                0, 1, 5,
                0, 5, 4
            };


            // Ensuring that all the indicies match their respective verticies
            if (offset != 0)
            {
                for (int i = 0; i < inds.Length; i++)
                {
                    inds[i] += offset;
                }
            }

            return inds;
        }

        /// <summary>
        /// Simple function that is now redundent. Sets the colour data for the cube
        /// </summary>
        /// <returns>Vector3[] - An array of all the colour data</returns>
        public override Vector3[] GetColorData()
        {
            return new Vector3[] {
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f)
            };
        }

        /// <summary>
        /// This function calculates the matrix of the cube based on scale, rotation and position
        /// </summary>
        public override void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
        }
    }
}
