using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Labs.ACW
{
    public class CreateBall
    {
        public CreateBall(Random rnd, Vector3 scale, int textureID, ref List<Objects> objects, ref List<Objects> balls)
        {
            ObjVolume ball = ObjVolume.LoadFromFile(@"Utility/Models/sphere.obj");
            float X = rnd.Next(-40, 41); // This is 100x the normal speed, this is so that there is more variety in the random number
            X = X / 100; // Once divided by 100 it's far more varying than simple -0.2 to 0.2
            float Z = rnd.Next(-40, 41);
            Z = Z / 100;
            ball.Position = new Vector3(X, 0f, Z);
            ball.Scale = scale;
            ball.resetScale = scale;
            ball.Velocity = new Vector3(0.2f, -0.2f, 0.2f);
            ball.TextureID = textureID;
            objects.Add(ball);
            balls.Add(ball); // Add all balls to the balls list, makes handling all of them at once easier
        }
    }
}
