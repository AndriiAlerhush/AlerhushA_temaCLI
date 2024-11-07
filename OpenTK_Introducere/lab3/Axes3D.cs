
using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace lab3
{
    /// <summary>
    /// Aceasta clasa implementeaza axele de coordonate cu toate parametrile lor.
    /// </summary>
    [Serializable]
    public class Axes3D : SceneObject<Axes3D>
    {
        public float lineLength;
        public float lineWidth;

        //
        //[XmlIgnore]
        public Color colorX;
        public int colorXR;
        public int colorXG;
        public int colorXB;
        //
        //[XmlIgnore]
        public Color colorY;
        public int colorYR;
        public int colorYG;
        public int colorYB;
        //
        //[XmlIgnore]
        public Color colorZ;
        public int colorZR;
        public int colorZG;
        public int colorZB;
        //

        // DEFAULT

        private const float DEFAULT_LINE_LENGTH = 30;
        private const float DEFAULT_LINE_WIDTH = 3;

        //[XmlIgnore]
        private readonly Color DEFAULT_AXE_X_COLOR = Color.Red;
        //[XmlIgnore]
        private readonly Color DEFAULT_AXE_Y_COLOR = Color.Green;
        //[XmlIgnore]
        private readonly Color DEFAULT_AXE_Z_COLOR = Color.Blue;

        public Axes3D()
        {
            SetDefault();
        }
        
        /// <summary>
        /// Acest constructor desearileaza un obiect al clasei Axes3D dintr-un fisier XML.
        /// </summary>
        /// <param name="fileName"></param>
        public Axes3D(string fileName)
        {
            try
            {
                AssignDeserialize(DeserializeXml(fileName));
            }
            catch
            {
                SetDefault();
            }
        }

        public Axes3D(float lineLength, float lineWidth)
        {
            this.lineLength = lineLength;
            this.lineWidth = lineWidth;
            SetAxeColor(Axes.AXE_X, DEFAULT_AXE_X_COLOR);
            SetAxeColor(Axes.AXE_Y, DEFAULT_AXE_Y_COLOR);
            SetAxeColor(Axes.AXE_Z, DEFAULT_AXE_Z_COLOR);
        }

        public Axes3D(float lineLength, float lineWidth, Color colorX, Color colorY, Color colorZ)
        {
            this.lineLength = lineLength;
            this.lineWidth = lineWidth;

            this.colorX = colorX;
            this.colorY = colorY;
            this.colorZ = colorZ;

            colorXR = colorX.R;
            colorXG = colorX.G;
            colorXB = colorX.B;

            colorYR = colorY.R;
            colorYG = colorY.G;
            colorYB = colorY.B;

            colorZR = colorZ.R;
            colorZG = colorZ.G;
            colorZB = colorZ.B;
        }

        public void DrawAxes3D()
        {
            GL.LineWidth(lineWidth);

            GL.Begin(PrimitiveType.Lines);

            // X
            GL.Color3(colorX);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(lineLength, 0, 0);

            // Y
            GL.Color3(colorY);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, lineLength, 0);

            // Z
            GL.Color3(colorZ);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, lineLength);

            GL.End();

            GL.LineWidth(1.0f);
        }

        public override void SetDefault()
        {
            lineLength = DEFAULT_LINE_LENGTH;
            lineWidth = DEFAULT_LINE_WIDTH;

            colorX = DEFAULT_AXE_X_COLOR;
            colorY = DEFAULT_AXE_Y_COLOR;
            colorZ = DEFAULT_AXE_Z_COLOR;

            colorXR = DEFAULT_AXE_X_COLOR.R;
            colorXG = DEFAULT_AXE_X_COLOR.G;
            colorXB = DEFAULT_AXE_X_COLOR.B;

            colorYR = DEFAULT_AXE_Y_COLOR.R;
            colorYG = DEFAULT_AXE_Y_COLOR.G;
            colorYB = DEFAULT_AXE_Y_COLOR.B;

            colorZR = DEFAULT_AXE_Z_COLOR.R;
            colorZG = DEFAULT_AXE_Z_COLOR.G;
            colorZB = DEFAULT_AXE_Z_COLOR.B;
        }

        public void SetColor(Color color)
        {
            colorX = color;
            colorY = color;
            colorZ = color;

            colorXR = color.R;
            colorXG = color.G;
            colorXB = color.B;

            colorYR = color.R;
            colorYG = color.G;
            colorYB = color.B;

            colorZR = color.R;
            colorZG = color.G;
            colorZB = color.B;
        }

        public void SetColor(int red, int green, int blue)
        {
            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));
            SetColor(Color.FromArgb(red, green, blue));
        }

        public void SetAxeColor(Axes axe, Color color)
        {
            switch (axe)
            {
                case Axes.AXE_X:
                    colorX = color;
                    colorXR = color.R;
                    colorXG = color.G;
                    colorXB = color.B;
                    break;

                case Axes.AXE_Y:
                    colorY = color;
                    colorYR = color.R;
                    colorYG = color.G;
                    colorYB = color.B;
                    break;

                case Axes.AXE_Z:
                    colorZ = color;
                    colorZR = color.R;
                    colorZG = color.G;
                    colorZB = color.B;
                    break;
            }
        }

        public void SetAxeColor(Axes axe, int red, int green, int blue)
        {
            Color color = Color.FromArgb(red, green, blue);
            SetAxeColor(axe, color);
        }

        public void AssignDeserialize(Axes3D desearializedAxes)
        {
            lineLength = desearializedAxes.lineLength;
            lineWidth = desearializedAxes.lineWidth;

            colorXR = desearializedAxes.colorXR;
            colorXG = desearializedAxes.colorXG;
            colorXB = desearializedAxes.colorXB;

            colorYR = desearializedAxes.colorYR;
            colorYG = desearializedAxes.colorYG;
            colorYB = desearializedAxes.colorYB;

            colorZR = desearializedAxes.colorZR;
            colorZG = desearializedAxes.colorZG;
            colorZB = desearializedAxes.colorZB;

            colorX = Color.FromArgb(colorXR, colorXG, colorXB);
            colorY = Color.FromArgb(colorYR, colorYG, colorYB);
            colorZ = Color.FromArgb(colorZR, colorZG, colorZB);
        }
    }
}
