using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Labs.ACW
{
    public class CreateCylinder
    {
        public CreateCylinder(Vector3 position, Vector3 scale, Vector3 rotation, int textureID, ref List<Objects> objects, ref List<Objects> cylinders)
        {
            ObjVolume cylinder1 = ObjVolume.LoadFromFile(@"Utility/Models/cylinder.obj");
            cylinder1.Position = position;
            cylinder1.Scale = scale;
            cylinder1.Rotation = rotation; // The rotation funciton, pretty similar to the other functions
            cylinder1.TextureID = textureID;
            objects.Add(cylinder1);
            cylinders.Add(cylinder1); // Extra list, this is for X-axis aligned cylinders
        }
    }
}
