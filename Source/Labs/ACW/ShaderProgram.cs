using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace Labs.ACW
{
    public class ShaderProgram
    {
        public int ProgramID = -1;
        public int VShaderID = -1;
        public int FShaderID = -1;
        public int AttributeCount = 0;
        public int UniformCount = 0;

        public Dictionary<String, AttributeInfo> Attributes = new Dictionary<string, AttributeInfo>();
        public Dictionary<String, UniformInfo> Uniforms = new Dictionary<string, UniformInfo>();
        public Dictionary<String, uint> Buffers = new Dictionary<string, uint>();

        /// <summary>
        /// Constructor for the shader
        /// </summary>
        public ShaderProgram()
        {
            ProgramID = GL.CreateProgram();
        }

        /// <summary>
        /// Sets all the attribute information of the shader
        /// </summary>
        public class AttributeInfo
        {
            public String name = "";
            public int address = -1;
            public int size = 0;
            public ActiveAttribType type;
        }

        /// <summary>
        /// Sets all the uniform information of the shader
        /// </summary>
        public class UniformInfo
        {
            public String name = "";
            public int address = -1;
            public int size = 0;
            public ActiveUniformType type;
        }

        /// <summary>
        /// This function loads the shader 
        /// </summary>
        /// <param name="code">All the provided code</param>
        /// <param name="type">What type of shader it is (i.e vertex shader, fragment shader, etc)</param>
        /// <param name="address">The ID address of the shader</param>
        private void loadShader(String code, ShaderType type, out int address)
        {
            address = GL.CreateShader(type);
            GL.ShaderSource(address, code);
            GL.CompileShader(address);
            GL.AttachShader(ProgramID, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        /// <summary>
        /// This function loads the shader from a provided string
        /// </summary>
        /// <param name="code">All the provided code</param>
        /// <param name="type">The type of the shader</param>
        public void LoadShaderFromString(String code, ShaderType type)
        {
            if (type == ShaderType.VertexShader)
            {
                loadShader(code, type, out VShaderID);
            }
            else if (type == ShaderType.FragmentShader)
            {
                loadShader(code, type, out FShaderID);
            }
        }

        /// <summary>
        /// This function loads the shader from a file
        /// </summary>
        /// <param name="filename">The filename of the shader provided</param>
        /// <param name="type">The type of shader it is</param>
        public void LoadShaderFromFile(String filename, ShaderType type)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                if (type == ShaderType.VertexShader)
                {
                    loadShader(sr.ReadToEnd(), type, out VShaderID);
                }
                else if (type == ShaderType.FragmentShader)
                {
                    loadShader(sr.ReadToEnd(), type, out FShaderID);
                }
            }
        }

        /// <summary>
        /// This function links the shader code to the program
        /// </summary>
        public void Link()
        {
            GL.LinkProgram(ProgramID);

            Console.WriteLine(GL.GetProgramInfoLog(ProgramID));

            GL.GetProgram(ProgramID, GetProgramParameterName.ActiveAttributes, out AttributeCount);
            GL.GetProgram(ProgramID, GetProgramParameterName.ActiveUniforms, out UniformCount);

            for (int i = 0; i < AttributeCount; i++)
            {
                AttributeInfo info = new AttributeInfo();
                int length = 0;

                StringBuilder name = new StringBuilder();

                GL.GetActiveAttrib(ProgramID, i, 256, out length, out info.size, out info.type, name);

                info.name = name.ToString();
                info.address = GL.GetAttribLocation(ProgramID, info.name);
                Attributes.Add(name.ToString(), info);
            }

            for (int i = 0; i < UniformCount; i++)
            {
                UniformInfo info = new UniformInfo();
                int length = 0;

                StringBuilder name = new StringBuilder();

                GL.GetActiveUniform(ProgramID, i, 256, out length, out info.size, out info.type, name);

                info.name = name.ToString();
                Uniforms.Add(name.ToString(), info);
                info.address = GL.GetUniformLocation(ProgramID, info.name);
            }
        }

        /// <summary>
        /// This function generates the buffers based on the attributes and uniforms that were linked above
        /// </summary>
        public void GenBuffers()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                uint buffer = 0;
                GL.GenBuffers(1, out buffer);

                Buffers.Add(Attributes.Values.ElementAt(i).name, buffer);
            }

            for (int i = 0; i < Uniforms.Count; i++)
            {
                uint buffer = 0;
                GL.GenBuffers(1, out buffer);

                Buffers.Add(Uniforms.Values.ElementAt(i).name, buffer);
            }
        }

        /// <summary>
        /// Simple function that enables the vertex attribute arrays
        /// </summary>
        public void EnableVertexAttribArrays()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                GL.EnableVertexAttribArray(Attributes.Values.ElementAt(i).address);
            }
        }

        /// <summary>
        /// Simple function that disables the vertex attribute arrays
        /// </summary>
        public void DisableVertexAttribArrays()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                GL.DisableVertexAttribArray(Attributes.Values.ElementAt(i).address);
            }
        }

        /// <summary>
        /// A simple getter function that gets and returns the attribute
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <returns>Attributes[name].address - The address of the attribute got</returns>
        public int GetAttribute(string name)
        {
            if (Attributes.ContainsKey(name))
            {
                return Attributes[name].address;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// A simple getter function that gets and returns the uniform
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <returns>Uniform[name].address - The address of the uniform got</returns>
        public int GetUniform(string name)
        {
            if (Uniforms.ContainsKey(name))
            {
                return Uniforms[name].address;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// A simple getter function that gets and returns the buffer
        /// </summary>
        /// <param name="name">The name of the buffer</param>
        /// <returns>Buffers[name] - The buffer got</returns>
        public uint GetBuffer(string name)
        {
            if (Buffers.ContainsKey(name))
            {
                return Buffers[name];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Override constructor that is called when creating a new Shader object
        /// </summary>
        /// <param name="vshader">The filename of the vertex shader</param>
        /// <param name="fshader">The filename of the fragment shader</param>
        /// <param name="fromFile">Whether or not it is from a file</param>
        public ShaderProgram(String vshader, String fshader, bool fromFile = false)
        {
            ProgramID = GL.CreateProgram();

            if (fromFile)
            {
                LoadShaderFromFile(vshader, ShaderType.VertexShader);
                LoadShaderFromFile(fshader, ShaderType.FragmentShader);
            }
            else
            {
                LoadShaderFromString(vshader, ShaderType.VertexShader);
                LoadShaderFromString(fshader, ShaderType.FragmentShader);
            }

            Link();
            GenBuffers();
        }

        public void Delete()
        {
            GL.DetachShader(ProgramID, VShaderID);
            GL.DetachShader(ProgramID, FShaderID);
            GL.DeleteShader(VShaderID);
            GL.DeleteShader(FShaderID);
            GL.DeleteProgram(ProgramID);
        }
    }
}
