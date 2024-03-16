using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace RayTracing
{
    class View
    {
        int vbo_position;
        int BasicProgramID;
        uint BasicVertexShader;
        uint BasicFragmentShader;
        Vector3 campos;
        int attribute_vpos;
        int uniform_pos;
        int uniform_aspect;
        int aspect;

        public int tetr, cube, bigSphere, smallSphere;


        public int BigSphere;
        public Vector3 ColorBigSphere;

        public int SmallSphere;
        public Vector3 ColorSmallSphere;

        public int Tetraeder;
        public Vector3 ColorTetraeder;

        public int Cube;
        public Vector3 ColorCube;

        public int BackWall;
        public Vector3 ColorBackWall;

        public int FrontWall;
        public Vector3 ColorFrontWall;

        public int LeftWall;
        public Vector3 ColorLeftWall;

        public int RightWall;
        public Vector3 ColorRightWall;

        public int TopWall;
        public Vector3 ColorTopWall;

        public int BottomWall;
        public Vector3 ColorBottomWall;


        public int RTDepth;

        public int bigCf;
        public int bigRCf;

        public int wallCf;
        public int wallRCf;

        public View()
        {
            vbo_position = 0;
            BasicProgramID = 0;
            BasicVertexShader = 0;
            BasicFragmentShader = 0;
            campos = new Vector3(0, 0, 0);
            attribute_vpos = 0;
            uniform_pos = 0;
            uniform_aspect = 0;
            aspect = 0;
            ColorBigSphere = new Vector3(1.0f, 1.0f, 0.0f);
            SmallSphere = 2;
            BigSphere = 2;
            ColorSmallSphere = new Vector3(0.0f, 1.0f, 0.0f);
            tetr = 0; 
            cube = 0;
            bigSphere = 0; 
            smallSphere = 0;

            Tetraeder = 2;
            ColorTetraeder = new Vector3(0.0f, 1.0f, 0.0f);
            Cube = 2;
            ColorCube = new Vector3(1.0f, 0.0f, 0.0f);

            ColorBackWall = ColorFrontWall = ColorLeftWall = ColorRightWall = ColorTopWall = ColorBottomWall = new Vector3(1.0f, 1.0f, 1.0f);
            BackWall = FrontWall = LeftWall = RightWall = TopWall = BottomWall = 2;

            RTDepth = 6;
            bigCf = 100;
            bigRCf = 130;
            wallCf = 100;
            wallRCf = 130;
        }
        public void DrawNewFrame()
        {
            GL.UseProgram(BasicProgramID);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "BigSphere"), BigSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColBigSphere"), ref ColorBigSphere);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "SmallSphere"), SmallSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColSmallSphere"), ref ColorSmallSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColCube"), ref ColorCube);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColTetr"), ref ColorTetraeder);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Cube"), Cube);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Tetr"), Tetraeder);

            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "utetr"), tetr);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "ubigs"), bigSphere);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "usmalls"), smallSphere);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "ucube"), cube);

            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColBack"), ref ColorBackWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColFront"), ref ColorFrontWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColLeft"), ref ColorLeftWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColRight"), ref ColorRightWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColTop"), ref ColorTopWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColBot"), ref ColorBottomWall);

            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Back"), BackWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Front"), FrontWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Left"), LeftWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Right"), RightWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Top"), TopWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Bot"), BottomWall);

            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "RTDepth"), RTDepth);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "bigCf"), bigCf);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "bigRCf"), bigRCf);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "wallCf"), wallCf);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "wallRCf"), wallRCf);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.Texture2D);
            GL.EnableVertexAttribArray(attribute_vpos);
            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            GL.DisableVertexAttribArray(attribute_vpos);
        }


        void loadShader(String filename, ShaderType shaderType, uint program, out uint address)
        {
            address = (uint)GL.CreateShader(shaderType);
            using (System.IO.StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource((int)address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog((int)address));
        }

        public void InitShaders()
        {
            BasicProgramID = GL.CreateProgram();
            loadShader("D:\\Рабочий стол\\ННГУ_УЧЕБА\\Компьютерная Графика\\Лаб.Раб(3) - Трассировка Лучей\\RayTracing\\RayTracing\\raytracing.vert", 
                ShaderType.VertexShader, (uint)BasicProgramID, out BasicVertexShader);
            loadShader("D:\\Рабочий стол\\ННГУ_УЧЕБА\\Компьютерная Графика\\Лаб.Раб(3) - Трассировка Лучей\\RayTracing\\RayTracing\\raytracing.frag", 
                ShaderType.FragmentShader, (uint)BasicProgramID, out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);

            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
        }

        public void initVBO()
        {
            Vector3[] vertdata = new Vector3[]
            {
                new Vector3(-1f, -1f, 0f), 
                new Vector3(1f, -1f, 0f),
                new Vector3(1f, 1f, 0f), 
                new Vector3(-1f, 1f, 0f)
            };
            GL.GenBuffers(1, out vbo_position);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
                                    (IntPtr)(vertdata.Length * Vector3.SizeInBytes),
                                    vertdata,
                                    BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.Uniform3(uniform_pos, campos);
            GL.Uniform1(uniform_aspect, aspect);

            GL.UseProgram(BasicProgramID);

            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "BigSphere"), BigSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColBigSphere"), ref ColorBigSphere);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "SmallSphere"), SmallSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColSmallSphere"), ref ColorSmallSphere);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColCube"), ref ColorCube);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColTetr"), ref ColorTetraeder);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Cube"), Cube);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Tetr"), Tetraeder);

            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "utetr"), tetr);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "ubigs"), bigSphere);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "usmalls"), smallSphere);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "ucube"), cube);

            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColBack"), ref ColorBackWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColFront"), ref ColorFrontWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColLeft"), ref ColorLeftWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColRight"), ref ColorRightWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColTop"), ref ColorTopWall);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "ColBot"), ref ColorBottomWall);

            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Back"), BackWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Front"), FrontWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Left"), LeftWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Right"), RightWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Top"), TopWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "Bot"), BottomWall);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "RTDepth"), RTDepth);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "bigCf"), bigCf);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "bigRCf"), bigRCf);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "wallCf"), wallCf);
            GL.Uniform1(GL.GetUniformLocation(BasicProgramID, "wallRCf"), wallRCf);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        
    }
}
