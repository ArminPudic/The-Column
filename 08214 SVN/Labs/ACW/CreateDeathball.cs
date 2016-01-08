using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Labs.ACW
{
    public class CreateDeathball
    {
        public CreateDeathball(Vector3 position, Vector3 scale, int textureID, ref List<Objects> objects)
        {
            ObjVolume deathBall = ObjVolume.LoadFromFile(@"Utility/Models/sphere.obj"); // Loading the model object using the object loader
            deathBall.Position = position;
            deathBall.Scale = scale;
            deathBall.TextureID = textureID;
            objects.Add(deathBall);
        }
    }
}
