
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace lab3
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Triangle3D : SceneObject<Triangle3D>
    {
        public Vector3d vertex1;
        public Vector3d vertex2;
        public Vector3d vertex3;

        //public float lineLength;
        //public float lineWidth;

        //
        //[XmlIgnore]
        public Color colorVertex1;
        public int colorVertex1A;
        public int colorVertex1R;
        public int colorVertex1G;
        public int colorVertex1B;
        //
        //[XmlIgnore]
        public Color colorVertex2;
        public int colorVertex2A;
        public int colorVertex2R;
        public int colorVertex2G;
        public int colorVertex2B;
        //
        //[XmlIgnore]
        public Color colorVertex3;
        public int colorVertex3A;
        public int colorVertex3R;
        public int colorVertex3G;
        public int colorVertex3B;
        //

        //
        // DEFAULT
        //

        //private const float DEFAULT_LINE_LENGTH = 30;
        //private const float DEFAULT_LINE_WIDTH = 3;

        //[XmlIgnore]
        private readonly Color DEFAULT_VERTEX1_COLOR = Color.Red;
        //[XmlIgnore]
        private readonly Color DEFAULT_VERTEX2_COLOR = Color.Green;
        //[XmlIgnore]
        private readonly Color DEFAULT_VERTEX3_COLOR = Color.Blue;

        public Triangle3D()
        {
            SetDefault();
        }

        private void AssignDeserialize(string fileName)
        {
            Triangle3D deserializedTriangle = DeserializeXml(fileName);

            vertex1 = deserializedTriangle.vertex1;
            vertex2 = deserializedTriangle.vertex2;
            vertex3 = deserializedTriangle.vertex3;

            colorVertex1A = deserializedTriangle.colorVertex1A;
            colorVertex1R = deserializedTriangle.colorVertex1R;
            colorVertex1G = deserializedTriangle.colorVertex1G;
            colorVertex1B = deserializedTriangle.colorVertex1B;

            colorVertex2A = deserializedTriangle.colorVertex2A;
            colorVertex2R = deserializedTriangle.colorVertex2R;
            colorVertex2G = deserializedTriangle.colorVertex2G;
            colorVertex2B = deserializedTriangle.colorVertex2B;

            colorVertex3A = deserializedTriangle.colorVertex3A;
            colorVertex3R = deserializedTriangle.colorVertex3R;
            colorVertex3G = deserializedTriangle.colorVertex3G;
            colorVertex3B = deserializedTriangle.colorVertex3B;

            colorVertex1 = Color.FromArgb(colorVertex1A, colorVertex1R, colorVertex1G, colorVertex1B);
            colorVertex2 = Color.FromArgb(colorVertex2A, colorVertex2R, colorVertex2G, colorVertex2B);
            colorVertex3 = Color.FromArgb(colorVertex3A, colorVertex3R, colorVertex3G, colorVertex3B);
        }

        public Triangle3D(string fileName)
        {
            try
            {
                AssignDeserialize(fileName);
            }
            catch
            {
                SetDefault();
            }
  
        }

        public void DrawTriangle3D()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(colorVertex1);
            GL.Vertex3(vertex1);
            GL.Color3(colorVertex2);
            GL.Vertex3(vertex2);
            GL.Color3(colorVertex3);
            GL.Vertex3(vertex3);

            GL.End();
        }

        /// <summary>
        /// Aceasta metoda modifica culoarea in functie de parametrii trimisi.
        /// </summary>
        /// <param name="vertex">Specifica un vertex al triunghiului.</param>
        /// <param name="color">Specifica canalul culorii care va fi modificat.</param>
        /// <param name="value">Specifica valoarea cu care va fi modificat canalul respectiv.</param>
        public void ChangeColor(Vertexes vertex, ARGB color, int value)
        {
            int[] colorComponent = null;

            switch (vertex)
            {
                case Vertexes.VERTEX1:
                    colorComponent = new int[] { colorVertex1A, colorVertex1R, colorVertex1G, colorVertex1B };
                    break;
                case Vertexes.VERTEX2:
                    colorComponent = new int[] { colorVertex2A, colorVertex2R, colorVertex2G, colorVertex2B };
                    break;
                case Vertexes.VERTEX3:
                    colorComponent = new int[] { colorVertex3A, colorVertex3R, colorVertex3G, colorVertex3B };
                    break;
            }

            if (colorComponent != null)
            {
                int colorIndex = (int)color;

                colorComponent[colorIndex] = Math.Max(0, Math.Min(255, value + colorComponent[colorIndex]));

                switch (vertex)
                {
                    case Vertexes.VERTEX1:
                        colorVertex1A = colorComponent[0];
                        colorVertex1R = colorComponent[1];
                        colorVertex1G = colorComponent[2];
                        colorVertex1B = colorComponent[3];
                        colorVertex1 = Color.FromArgb(colorVertex1A, colorVertex1R, colorVertex1G, colorVertex1B);
                        break;

                    case Vertexes.VERTEX2:
                        colorVertex2A = colorComponent[0];
                        colorVertex2R = colorComponent[1];
                        colorVertex2G = colorComponent[2];
                        colorVertex2B = colorComponent[3];
                        colorVertex2 = Color.FromArgb(colorVertex2A, colorVertex2R, colorVertex2G, colorVertex2B);
                        break;

                    case Vertexes.VERTEX3:
                        colorVertex3A = colorComponent[0];
                        colorVertex3R = colorComponent[1];
                        colorVertex3G = colorComponent[2];
                        colorVertex3B = colorComponent[3];
                        colorVertex3 = Color.FromArgb(colorVertex3A, colorVertex3R, colorVertex3G, colorVertex3B);
                        break;
                }
            }
        }

        public override void SetDefault()
        {
            vertex1.X = -15.0f;
            vertex1.Y = -15.0f;
            vertex1.Z = 5.0f;

            vertex2.X = 15.0f;
            vertex2.Y = 15.0f;
            vertex2.Z = 5.0f;

            vertex3.X = 0.0f;
            vertex3.Y = 15.0f;
            vertex3.Z = 5.0f;

            colorVertex1 = DEFAULT_VERTEX1_COLOR;
            colorVertex2 = DEFAULT_VERTEX2_COLOR;
            colorVertex3 = DEFAULT_VERTEX3_COLOR;

            colorVertex1A = DEFAULT_VERTEX1_COLOR.A;
            colorVertex1R = DEFAULT_VERTEX1_COLOR.R;
            colorVertex1G = DEFAULT_VERTEX1_COLOR.G;
            colorVertex1B = DEFAULT_VERTEX1_COLOR.B;

            colorVertex2A = DEFAULT_VERTEX2_COLOR.A;
            colorVertex2R = DEFAULT_VERTEX2_COLOR.R;
            colorVertex2G = DEFAULT_VERTEX2_COLOR.G;
            colorVertex2B = DEFAULT_VERTEX2_COLOR.B;

            colorVertex3A = DEFAULT_VERTEX3_COLOR.A;
            colorVertex3R = DEFAULT_VERTEX3_COLOR.R;
            colorVertex3G = DEFAULT_VERTEX3_COLOR.G;
            colorVertex3B = DEFAULT_VERTEX3_COLOR.B;
        }
    }
}
