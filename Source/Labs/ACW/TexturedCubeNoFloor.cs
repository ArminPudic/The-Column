using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Labs.ACW
{
    /// <summary>
    /// Exactly the same as TexturedCube.cs only with a minor adjustment for no floor
    /// </summary>
    public class TexturedCubeNoFloor : Cube
    {
        public TexturedCubeNoFloor() : base()
        {
            VertCount = 24;
            IndiceCount = 36;
            TextureCoordsCount = 24;
        }

        public override Vector3[] GetVerts()
        {
            return new Vector3[] {
                //left
                new Vector3(-0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, 0.5f,  -0.5f),
                new Vector3(0.5f, -0.5f,  -0.5f),
                new Vector3(-0.5f, 0.5f,  -0.5f),
 
                //back
                new Vector3(0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, 0.5f,  -0.5f),
                new Vector3(0.5f, 0.5f,  0.5f),
                new Vector3(0.5f, -0.5f,  0.5f),
 
                //right
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, 0.5f,  0.5f),
                new Vector3(-0.5f, 0.5f,  0.5f),
 
                //top
                new Vector3(0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, 0.5f,  -0.5f),
                new Vector3(0.5f, 0.5f,  0.5f),
                new Vector3(-0.5f, 0.5f,  0.5f),
 
                //front
                new Vector3(-0.5f, -0.5f,  -0.5f), 
                new Vector3(-0.5f, 0.5f,  0.5f), 
                new Vector3(-0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
 
                //bottom
                new Vector3(-0.5f, -0.5f,  -0.5f), 
                new Vector3(0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, -0.5f,  0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f)
 
            };
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] inds = new int[] {
                //left
                0,2,1,0,1,13,
 
                //back
                4,6,5,4,7,6,
 
                //right
                8,10,9,8,11,10,
 
                //top
                13,12,14,13,14,15,
 
                //front
                16,18,17,16,17,19,
 
                //bottom 
                17,17,17,17,17,17 // Minor adjustment that prevents the floor from being drawn
            };

            if (offset != 0)
            {
                for (int i = 0; i < inds.Length; i++)
                {
                    inds[i] += offset;
                }
            }

            return inds;
        }

        public override Vector2[] GetTextureCoords()
        {
            return new Vector2[] {
                // left
                new Vector2(0.0f, 0.0f),
                new Vector2(-1.0f, 1.0f),
                new Vector2(-1.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
 
                // back
                new Vector2(0.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
                new Vector2(-1.0f, 0.0f),
 
                // right
                new Vector2(-1.0f, 0.0f),
                new Vector2(0.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
 
                // top
                new Vector2(0.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(-1.0f, 0.0f),
                new Vector2(-1.0f, 1.0f),
 
                // front
                new Vector2(0.0f, 0.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(1.0f, 0.0f),
 
                // bottom
                new Vector2(0.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
                new Vector2(-1.0f, 0.0f)
            };
        }
    }
}
