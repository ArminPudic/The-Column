using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.IO;

namespace Labs.ACW
{
    public class ObjVolume : Objects
    {
        Vector3[] vertices;
        Vector3[] colors;
        Vector2[] texturecoords;

        List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();

        public override int VertCount { get { return vertices.Length; } }
        public override int IndiceCount { get { return faces.Count * 3; } }
        public override int ColorDataCount { get { return colors.Length; } }

        /// <summary>
        /// This function gets the vertices for this object
        /// </summary>
        /// <returns></returns>
        public override Vector3[] GetVerts()
        {
            return vertices;
        }

        /// <summary>
        /// This function gets the indices for this object
        /// </summary>
        /// <param name="offset">Number of vertices buffered before this object</param>
        /// <returns>temp[] - An array of indices with the offset applied</returns>
        public override int[] GetIndices(int offset = 0)
        {
            List<int> temp = new List<int>();

            foreach (var face in faces)
            {
                temp.Add(face.Item1 + offset);
                temp.Add(face.Item2 + offset);
                temp.Add(face.Item3 + offset);
            }

            return temp.ToArray();
        }

        /// <summary>
        /// This simple function gets the color data
        /// </summary>
        /// <returns>colors - The colour data</returns>
        public override Vector3[] GetColorData()
        {
            return colors;
        }

        /// <summary>
        /// Another simple function that gets the texture coordinates
        /// </summary>
        /// <returns>texturecoords - The coordinates of the texture</returns>
        public override Vector2[] GetTextureCoords()
        {
            return texturecoords;
        }


        /// <summary>
        /// This function calculates the model matrix based on the scale, rotation and position
        /// </summary>
        public override void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
        }

        /// <summary>
        /// This function loads in the model objects from the filename provided
        /// </summary>
        /// <param name="filename">The filename of the object</param>
        /// <returns>obj - The object after being loaded in</returns>
        public static ObjVolume LoadFromFile(string filename)
        {
            // This creates a new object and proceeds to set it's verticies, indicies, etc based on the data from the file loaded
            ObjVolume obj = new ObjVolume();
            try
            {
                using (StreamReader reader = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
                {
                    obj = LoadFromString(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found: {0}", filename);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading file: {0}", filename);
            }

            return obj;
        }

        /// <summary>
        /// This function loads in all the data of the object that was read from the file
        /// </summary>
        /// <param name="obj">The object created previously in string form</param>
        /// <returns></returns>
        public static ObjVolume LoadFromString(string obj)
        {
            // Seperate lines from the file
            List<String> lines = new List<string>(obj.Split('\n'));

            // These lists hold the model data
            List<Vector3> verts = new List<Vector3>();
            List<Vector3> colors = new List<Vector3>();
            List<Vector2> texs = new List<Vector2>();
            List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();

            foreach (String line in lines)
            {
                if (line.StartsWith("v ")) // Vertex
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);

                    Vector3 vec = new Vector3();

                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a vertex
                    {
                        String[] vertparts = temp.Split(' ');

                        // Attempt to parse each part of the vertice
                        bool success = float.TryParse(vertparts[0], out vec.X);
                        success |= float.TryParse(vertparts[1], out vec.Y);
                        success |= float.TryParse(vertparts[2], out vec.Z);

                        // Dummy color/texture coordinates (Not using colour and texture coordinates from the file)
                        colors.Add(new Vector3((float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z)));
                        texs.Add(new Vector2((float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z)));

                        if (!success)
                        {
                            Console.WriteLine("Error parsing vertex: {0}", line);
                        }
                    }

                    verts.Add(vec);
                }
                else if (line.StartsWith("f ")) // Face definition
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);

                    Tuple<int, int, int> face = new Tuple<int, int, int>(0, 0, 0);

                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a face
                    {
                        String[] faceparts = temp.Split(' ');

                        int i1, i2, i3;

                        // Attempt to parse each part of the face
                        bool success = int.TryParse(faceparts[0], out i1);
                        success |= int.TryParse(faceparts[1], out i2);
                        success |= int.TryParse(faceparts[2], out i3);

                        if (!success)
                        {
                            Console.WriteLine("Error parsing face: {0}", line);
                        }
                        else
                        {
                            // Decrement to get zero-based vertex numbers
                            face = new Tuple<int, int, int>(i1 - 1, i2 - 1, i3 - 1);
                            faces.Add(face);
                        }
                    }
                }
            }

            // Create the ObjVolume
            ObjVolume vol = new ObjVolume();
            vol.vertices = verts.ToArray();
            vol.faces = new List<Tuple<int, int, int>>(faces);
            vol.colors = colors.ToArray();
            vol.texturecoords = texs.ToArray();

            return vol;
        }
    }
}
