
using OpenTK;
using OpenTK.Input;
using System;

namespace lab3
{
    /// <summary>
    /// Aceasta clasa implementeaza camera cu toate parametrile ei.
    /// </summary>
    [Serializable]
    public class Camera : SceneObject<Camera>
    {
        public float eyeX = DEFAULT_EYE_X;
        public float eyeY = DEFAULT_EYE_Y;
        public float eyeZ = DEFAULT_EYE_Z;

        public float targetX = DEFAULT_TARGET_X;
        public float targetY = DEFAULT_EYE_Y;
        public float targetZ = DEFAULT_TARGET_Z;

        public float upX = DEFAULT_UP_X;
        public float upY = DEFAULT_UP_Y;
        public float upZ = DEFAULT_UP_Z;

        private const float DEFAULT_EYE_X = 30.0f;
        private const float DEFAULT_EYE_Y = 30.0f;
        private const float DEFAULT_EYE_Z = 30.0f;

        private const float DEFAULT_TARGET_X = 0;
        private const float DEFAULT_TARGET_Y = 0;
        private const float DEFAULT_TARGET_Z = 0;

        private const float DEFAULT_UP_X = 0.0f;
        private const float DEFAULT_UP_Y = 1.0f;
        private const float DEFAULT_UP_Z = 0.0f;

        private float yaw = 0;
        private float pitch = 0;
        private float mouseSensitivity = 0.005f;

        private int lastMouseX;
        private int lastMouseY;
        private bool firstMove = true;

        public Camera()
        {
            InitializeDirection();
        }

        /// <summary>
        /// Acest constructor desearileaza un obiect al clasei Camera dintr-un fisier XML.
        /// </summary>
        /// <param name="fileName"></param>
        public Camera(string fileName)
        {
            Camera desearializedCamera = DeserializeXml(fileName);
            eyeX = desearializedCamera.eyeX;
            eyeY = desearializedCamera.eyeY;
            eyeZ = desearializedCamera.eyeZ;
            targetX = desearializedCamera.targetX;
            targetY = desearializedCamera.targetY;
            targetZ = desearializedCamera.targetZ;
            upX = desearializedCamera.upX;
            upY = desearializedCamera.upY;
            upZ = desearializedCamera.upZ;

            InitializeDirection();
        }

        public Camera(float eyeX, float eyeY, float eyeZ, float targetX, float targetY, float targetZ, float upX, float upY, float upZ)
        {
            this.eyeX = eyeX;
            this.eyeY = eyeY;
            this.eyeZ = eyeZ;
            this.targetX = targetX;
            this.targetY = targetY;
            this.targetZ = targetZ;
            this.upX = upX;
            this.upY = upY;
            this.upZ = upZ;

            InitializeDirection();
        }

        public Matrix4 GetLookAt()
        {
            return Matrix4.LookAt(eyeX, eyeY, eyeZ, targetX, targetY, targetZ, upX, upY, upZ);
        }

        /// <summary>
        /// Aceasta metoda reseteaza toti parametrii la valori implicite.
        /// </summary>
        public override void SetDefault()
        {
            eyeX = DEFAULT_EYE_X;
            eyeY = DEFAULT_EYE_Y;
            eyeZ = DEFAULT_EYE_Z;

            targetX = DEFAULT_TARGET_X;
            targetY = DEFAULT_EYE_Y;
            targetZ = DEFAULT_TARGET_Z;

            upX = DEFAULT_UP_X;
            upY = DEFAULT_UP_Y;
            upZ = DEFAULT_UP_Z;
        }

        public void Rotate(float yawOffset, float pitchOffset)
        {
            yaw += yawOffset;
            pitch += pitchOffset;

            pitch = Clamp(pitch, -89f, 89f);

            float yawRad = MathHelper.DegreesToRadians(yaw);
            float pitchRad = MathHelper.DegreesToRadians(pitch);

            float cosPitch = (float)Math.Cos(pitchRad);
            float sinPitch = (float)Math.Sin(pitchRad);
            float cosYaw = (float)Math.Cos(yawRad);
            float sinYaw = (float)Math.Sin(yawRad);

            targetX = eyeX + cosPitch * cosYaw;
            targetY = eyeY + sinPitch;
            targetZ = eyeZ + cosPitch * sinYaw;
        }

        public void RotateMouse(MouseState mouse)
        {

            int mouseX = mouse.X;
            int mouseY = mouse.Y;

            if (firstMove && mouseX != 0 && mouseY != 0)
            {
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                firstMove = false;
                return;
            }

            int deltaX = mouseX - lastMouseX;
            int deltaY = mouseY - lastMouseY;

            lastMouseX = mouseX;
            lastMouseY = mouseY;

            float yawOffset = deltaX * mouseSensitivity;
            float pitchOffset = -deltaY * mouseSensitivity;

            yaw += yawOffset;
            pitch += pitchOffset;

            pitch = Clamp(pitch, -89f, 89f);
            float yawRad = MathHelper.DegreesToRadians(yaw);
            float pitchRad = MathHelper.DegreesToRadians(pitch);

            float cosPitch = (float)Math.Cos(pitchRad);
            float sinPitch = (float)Math.Sin(pitchRad);
            float cosYaw = (float)Math.Cos(yawRad);
            float sinYaw = (float)Math.Sin(yawRad);

            targetX = eyeX + cosPitch * cosYaw;
            targetY = eyeY + sinPitch;
            targetZ = eyeZ + cosPitch * sinYaw;
        }

        private float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void InitializeDirection()
        {
            Vector3 direction = new Vector3(targetX - eyeX, targetY - eyeY, targetZ - eyeZ);
            yaw = MathHelper.RadiansToDegrees((float)Math.Atan2(direction.Z, direction.X));
            pitch = MathHelper.RadiansToDegrees((float)Math.Asin(direction.Y / direction.Length));
        }
    }
}
