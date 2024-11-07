
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;

namespace lab3
{
    public class Window3D : GameWindow
    {
        private KeyboardState lastKey;
        //private MouseState lastMouseState;

        private Camera camera;
        private readonly string cameraConfig;
        private Perspective perspective;
        private readonly string perspectiveConfig;
        private Triangle3D triangle;
        private readonly string triangleConfig;
        private Axes3D axes;
        private readonly string axesConfig;

        private Randomizer randomizer;

        private Color DEFAULT_BACK_COLOR = Color.LightSkyBlue;

        public Window3D() : base(800, 600, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            //
            // Pozitionarea ferestrei in centrul ecranului
            //
            // Obtinerea dimensiunii ecranului
            int screenWidth = DisplayDevice.Default.Width;
            int screenHeight = DisplayDevice.Default.Height;
            // Calcularea pozitiei de centrare a ferestrei
            int windowX = (screenWidth - Width) / 2;
            int windowY = (screenHeight - Height) / 2;
            // Setarea pozitiei a ferestrei
            Location = new Point(windowX, windowY);

            DisplayHelp();

            // extragerea numelor de fisiere de configurare din fisierul app.config
            cameraConfig = ConfigurationManager.AppSettings["cameraConfig"];
            perspectiveConfig = ConfigurationManager.AppSettings["perspectiveConfig"];
            triangleConfig = ConfigurationManager.AppSettings["triangleConfig"];
            axesConfig = ConfigurationManager.AppSettings["axesConfig"];

            // initializarea obiectelor ale scenei 3D
            randomizer = new Randomizer();
            camera = new Camera(cameraConfig);
            perspective = new Perspective(perspectiveConfig);
            axes = new Axes3D(axesConfig);
            triangle = new Triangle3D(triangleConfig);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.LightSkyBlue);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            float aspect_ratio = Width / (float)Height;
            perspective.aspect = aspect_ratio;
            
            Matrix4 perspect = perspective.GetPerspectiveFieldOfView();
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspect);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState currentKey = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            if (currentKey[Key.Escape] && !lastKey[Key.Escape])
            {
                Exit();
            }

            if (currentKey[Key.H] && !lastKey[Key.H])
            {
                DisplayHelp();
            }

            if (currentKey[Key.C] && !lastKey[Key.C])
            {
                GL.ClearColor(randomizer.GenerateRandomColor());
            }

            if (currentKey[Key.U] && !lastKey[Key.U])
            {
                GL.ClearColor(DEFAULT_BACK_COLOR);
                triangle.SetDefault();
            }

            if (currentKey[Key.F] && !lastKey[Key.F])
            {
                if (WindowState == WindowState.Fullscreen)
                    WindowState = WindowState.Normal;
                else
                    WindowState = WindowState.Fullscreen;
            }

            //
            // schimbarea culorii triunghiului
            //

            int? value = null;
            Vertexes? vertex = null;
            ARGB? color = null;

            if (currentKey[Key.Number1]) vertex = Vertexes.VERTEX1;
            if (currentKey[Key.Number2]) vertex = Vertexes.VERTEX2;
            if (currentKey[Key.Number3]) vertex = Vertexes.VERTEX3;
            if (currentKey[Key.R]) color = ARGB.RED;
            if (currentKey[Key.G]) color = ARGB.GREEN;
            if (currentKey[Key.B]) color = ARGB.BLUE;
            if (currentKey[Key.A]) color = ARGB.ALPHA;
            if (currentKey[Key.Plus]) value = 5;
            if (currentKey[Key.Minus]) value = -5;

            if (vertex.HasValue && color.HasValue && value.HasValue)
            {
                triangle.ChangeColor(vertex.Value, color.Value, value.Value);

                Color colorVertex = Color.Black;

                switch (vertex)
                {
                    case Vertexes.VERTEX1:
                        colorVertex = triangle.colorVertex1;
                        break;

                    case Vertexes.VERTEX2:
                        colorVertex = triangle.colorVertex2;
                        break;

                    case Vertexes.VERTEX3:
                        colorVertex = triangle.colorVertex3;
                        break;
                }

                Console.WriteLine($"{vertex,-7}:" +
                                  $"\tAlpha = {colorVertex.A}" +
                                  $"\tRed = {colorVertex.R,-3}" +
                                  $"\tGreen = {colorVertex.G,-3}" +
                                  $"\tBlue = {colorVertex.B,-3}");
            }

            lastKey = currentKey;

            camera.RotateMouse(currentMouse);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
              
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            Matrix4 lookAt = camera.GetLookAt();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookAt);

            // desenarea pe ecran
            axes.DrawAxes3D();
            triangle.DrawTriangle3D();

            SwapBuffers();
        }

        private void DisplayHelp()
        {
            Console.WriteLine("\n MENIU");
            Console.WriteLine(" H - Help");
            Console.WriteLine(" C - Schimbare culoare fundal in mod aleatoriu");
            Console.WriteLine(" <nr. vertexului> + <canalul> + +/- --> schimbarea culorii a unui vertex al triungiului");
            Console.WriteLine("     De exemplu: 1 + R + - --> modifica culoarea primului vertex");
            Console.WriteLine(" U - Setare valori implicite");
            Console.WriteLine(" F - Full/Normal screen");
            Console.WriteLine(" Esc - Exit");
            Console.WriteLine();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            //camera.SerializeXml(cameraConfig);
            //perspective.SerializeXml(perspectiveConfig);
            //triangle.SerializeXml(triangleConfig);
        }
    }
}
