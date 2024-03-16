using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        View view;
        private bool loaded;
        Vector3 selectedColor;

        public Form1()
        {
            InitializeComponent();
            view = new View();
            loaded = false;
            selectedColor = new Vector3(0.7f, 0.5f, 0.9f);
            comboBox2.SelectedIndex = 1;

            trackBar1.Maximum = 1;
            trackBar1.Maximum = 20;

            trackBar1.Value = view.RTDepth;

            trackBar2.Minimum = 1;
            trackBar2.Maximum = 750;

            trackBar3.Minimum = 1;
            trackBar3.Maximum = 400;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
        }
        private void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
            {
                glControl1.Invalidate();
            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            view.InitShaders();
            view.initVBO();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                view.DrawNewFrame();
                glControl1.SwapBuffers();
                GL.UseProgram(0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedColor.X = colorDialog1.Color.R / 255.0f;
                selectedColor.Y = colorDialog1.Color.G / 255.0f;
                selectedColor.Z = colorDialog1.Color.B / 255.0f;
            }
            /*CUBE
            TETRAEDER
            BIG SPHERE
            SMALL SPHERE
            BACK WALL
            LEFT WALL
            RIGHT WALL
            FRONT WALL
            ROOF
            FLOOR
            */

            /* Стены Сцены:
            Задняя - 0, Передняя - 1, Верхняя - 2, Нижняя - 3, Левая - 4, Правая - 5; */
            switch (comboBox3.SelectedIndex)
            {
                case 0: { view.ColorBackWall = selectedColor;   break; }
                case 1: { view.ColorFrontWall = selectedColor;  break; }
                case 2: { view.ColorTopWall = selectedColor;    break; }
                case 3: { view.ColorBottomWall = selectedColor; break; }
                case 4: { view.ColorLeftWall = selectedColor;   break; }
                case 5: { view.ColorRightWall = selectedColor;  break; }
            }


            switch (comboBox1.SelectedIndex)
            {
                case 0: { view.ColorCube = new Vector3(selectedColor.X, selectedColor.Y, selectedColor.Z); break; }
                case 1: { view.ColorTetraeder = selectedColor; break; }
                case 2: { view.ColorBigSphere = selectedColor; break; }
                case 3: { view.ColorSmallSphere = selectedColor; break; }
                case 4: { break; }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                view.cube = 1;
            else
                view.cube = 0;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                view.tetr = 1;
            else
                view.tetr = 0;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                view.bigSphere = 1;
            else
                view.bigSphere = 0;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                view.smallSphere = 1;
            }
            else
            {
                view.smallSphere = 0;
            }
        }
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            view.RTDepth = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: {  break; }
                case 1: {  break; }
                case 2: { view.bigCf = trackBar2.Value; break; }
                case 3: { view.bigCf = trackBar2.Value; break; }
                case 4: { break; }
            }

            switch (comboBox3.SelectedIndex)
            {
                case 0: { view.wallCf = trackBar2.Value; break; }
                case 1: { view.wallCf = trackBar2.Value; break; }
                case 2: { view.wallCf = trackBar2.Value; break; }
                case 3: { view.wallCf = trackBar2.Value; break; }
                case 4: { view.wallCf = trackBar2.Value; break; }
                case 5: { view.wallCf = trackBar2.Value; break; }
                case 6: { break; }
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: { break; }
                case 1: { break; }
                case 2: { view.bigRCf = trackBar3.Value; break; }
                case 3: { view.bigRCf = trackBar3.Value; break; }
                case 4: { break; }
            }
            switch (comboBox3.SelectedIndex)
            {
                case 0: { view.wallRCf = trackBar3.Value; break; }
                case 1: { view.wallRCf = trackBar3.Value; break; }
                case 2: { view.wallRCf = trackBar3.Value; break; }
                case 3: { view.wallRCf = trackBar3.Value; break; }
                case 4: { view.wallRCf = trackBar3.Value; break; }
                case 5: { view.wallRCf = trackBar3.Value; break; }
                case 6: { break; }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        view.Cube = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 1:
                    {
                        view.Tetraeder = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 2:
                    {
                        view.BigSphere = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 3:
                    {
                        view.SmallSphere = comboBox2.SelectedIndex + 1; break;
                    }
                case 4: { break; }
            }
           
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    {
                        view.BackWall = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 1:
                    {
                        view.FrontWall = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 2:
                    {
                        view.TopWall = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 3:
                    {
                        view.BottomWall = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 4:
                    {
                        view.LeftWall = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 5:
                    {
                        view.RightWall = comboBox2.SelectedIndex + 1;
                        break;
                    }
                case 6: { break; }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        comboBox2.SelectedIndex = view.Cube - 1;
                        break;
                    }
                case 1:
                    {
                        comboBox2.SelectedIndex = view.Tetraeder - 1;
                        break;
                    }
                case 2:
                    {
                        comboBox2.SelectedIndex = view.BigSphere - 1;
                        trackBar2.Value = view.bigCf;
                        trackBar3.Value = view.bigRCf;
                        break;
                    }
                
                case 3:
                     {
                         comboBox2.SelectedIndex = view.SmallSphere - 1;
                         break;
                     }
                case 4: { break; }
            }
            switch (comboBox3.SelectedIndex)
            { 
                case 0:
                    {
                        comboBox2.SelectedIndex = view.BackWall - 1;
                        trackBar2.Value = view.wallCf;
                        trackBar3.Value = view.wallRCf;
                        break;
                    }
                case 1:
                    {
                        comboBox2.SelectedIndex = view.LeftWall - 1;
                        trackBar2.Value = view.wallCf;
                        trackBar3.Value = view.wallRCf;
                        break;
                    }
                case 2:
                    {
                        comboBox2.SelectedIndex = view.RightWall - 1;
                        trackBar2.Value = view.wallCf;
                        trackBar3.Value = view.wallRCf;
                        break;
                    }
                case 3:
                    {
                        comboBox2.SelectedIndex = view.FrontWall - 1;
                        trackBar2.Value = view.wallCf;
                        trackBar3.Value = view.wallRCf;
                        break;
                    }
                case 4:
                    {
                        comboBox2.SelectedIndex = view.TopWall - 1;
                        trackBar2.Value = view.wallCf;
                        trackBar3.Value = view.wallRCf;
                        break;
                    }
                case 5:
                    {
                        comboBox2.SelectedIndex = view.BottomWall - 1;
                        trackBar2.Value = view.wallCf;
                        trackBar3.Value = view.wallRCf;
                        break;
                    }
                case 6: { break; }
            }
        }
    }
}
