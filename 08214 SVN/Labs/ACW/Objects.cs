using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Labs.ACW
{
    /// <summary>
    /// The mother class of every object. Handles all the maintenence and creation of objects
    /// </summary>
    public abstract class Objects
    {
        public bool IsTextured = false;
        public int TextureID;
        public int originalTexture;
        public int TextureCoordsCount;
        public abstract Vector2[] GetTextureCoords(); // Refer to the function of that name in the Cube classes

        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;
        public Vector3 Velocity = Vector3.Zero;
        public Vector3 resetScale = Vector3.Zero;

        public virtual int VertCount { get; set; }
        public virtual int IndiceCount { get; set; }
        public virtual int ColorDataCount { get; set; }
        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public abstract Vector3[] GetVerts();
        public abstract int[] GetIndices(int offset = 0);
        public abstract Vector3[] GetColorData();
        public abstract void CalculateModelMatrix();
    }
}
