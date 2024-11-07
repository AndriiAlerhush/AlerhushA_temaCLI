
using OpenTK;
using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace lab2
{
    public class SimpleWindow3D : GameWindow
    {
        const float rotation_speed = 180.0f;
        float angleX, angleY;
        bool showCube = true;
        KeyboardState lastKeyPress;
        MouseState lastMouseState;

        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;

            // Obtinerea dimensiunii ecranului
            int screenWidth = DisplayDevice.Default.Width;
            int screenHeight = DisplayDevice.Default.Height;

            // Calcularea pozitiei de centrare a ferestrei
            int windowX = (screenWidth - Width) / 2;
            int windowY = (screenHeight - Height) / 2;

            // Setarea pozitiei a ferestrei
            Location = new Point(windowX, windowY);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Blue);
            //GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
                return;
            }

            else if (keyboard[OpenTK.Input.Key.P] && !keyboard.Equals(lastKeyPress))
            {
                // Ascundere comandată, prin apăsarea unei taste - cu verificare de remanență!
                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }
            lastKeyPress = keyboard;

            // Ascundere comandată, prin apăsarea butonului stang al mouse-ului - cu verificare de remanență!
            if (mouse[OpenTK.Input.MouseButton.Left] && !mouse.Equals(lastMouseState))
            {
                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }

            // Rotatia pe axa X
            if (keyboard[OpenTK.Input.Key.A])
            {
                angleX -= rotation_speed * (float)e.Time;
            }

            if (keyboard[OpenTK.Input.Key.D])
            {
                angleX += rotation_speed * (float)e.Time;
            }

            // Control prin mouse pentru rotatia pe axa Y
            if (!mouse.Equals(lastMouseState))
            {
                // Diferenta in pozitia X a mouse-ului
                float deltaX = mouse.X - lastMouseState.X;
                // Ajustarea vitezei de rotatie in functie de miscarea mouse-ului
                angleY += deltaX * 0.01f;
            }
            lastMouseState = mouse;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            // Rotire pe axa X
            GL.Rotate(angleX, 1.0f, 0.0f, 0.0f);
            // Rotire pe axa Y
            GL.Rotate(angleY, 0.0f, 1.0f, 0.0f);

            // Exportăm controlul randării obiectelor către o metodă externă (modularizare).
            if (showCube == true)
            {
                DrawCube();
                DrawAxes_OLD();
            }

            SwapBuffers();
        }

        private void DrawAxes_OLD()
        {
            GL.LineWidth(2);
            
            GL.Begin(PrimitiveType.Lines);

            // X
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(25, 0, 0);

            // Y
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 25, 0);

            // Z
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 25);

            GL.End();

            GL.LineWidth(1);
        }

        private void DrawCube()
        {
            //GL.Begin(PrimitiveType.Quads);

            //GL.Color3(Color.Silver);
            //GL.Vertex3(-3.0f, -3.0f, -3.0f);
            //GL.Vertex3(-3.0f, 3.0f, -3.0f);
            //GL.Vertex3(3.0f, 3.0f, -3.0f);
            //GL.Vertex3(3.0f, -3.0f, -3.0f);

            //GL.Color3(Color.Honeydew);
            //GL.Vertex3(-3.0f, -3.0f, -3.0f);
            //GL.Vertex3(3.0f, -3.0f, -3.0f);
            //GL.Vertex3(3.0f, -3.0f, 3.0f);
            //GL.Vertex3(-3.0f, -3.0f, 3.0f);

            //GL.Color3(Color.Moccasin);
            //GL.Vertex3(-3.0f, -3.0f, -3.0f);
            //GL.Vertex3(-3.0f, -3.0f, 3.0f);
            //GL.Vertex3(-3.0f, 3.0f, 3.0f);
            //GL.Vertex3(-3.0f, 3.0f, -3.0f);

            //GL.Color3(Color.IndianRed);
            //GL.Vertex3(-3.0f, -3.0f, 3.0f);
            //GL.Vertex3(3.0f, -3.0f, 3.0f);
            //GL.Vertex3(3.0f, 3.0f, 3.0f);
            //GL.Vertex3(-3.0f, 3.0f, 3.0f);

            //GL.Color3(Color.PaleVioletRed);
            //GL.Vertex3(-3.0f, 3.0f, -3.0f);
            //GL.Vertex3(-3.0f, 3.0f, 3.0f);
            //GL.Vertex3(3.0f, 3.0f, 3.0f);
            //GL.Vertex3(3.0f, 3.0f, -3.0f);

            //GL.Color3(Color.ForestGreen);
            //GL.Vertex3(3.0f, -3.0f, -3.0f);
            //GL.Vertex3(3.0f, 3.0f, -3.0f);
            //GL.Vertex3(3.0f, 3.0f, 3.0f);
            //GL.Vertex3(3.0f, -3.0f, 3.0f);

            //GL.End();

            GL.Begin(PrimitiveType.Quads);

            // Fața din față (roșie)
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            // Fața din spate (verde)
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            // Fața din stânga (albastră)
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            // Fața din dreapta (galbenă)
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            // Fața de sus (cyan)
            GL.Color3(0.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            // Fața de jos (magenta)
            GL.Color3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.End();
        }
    }
}
