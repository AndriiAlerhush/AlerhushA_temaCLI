
namespace lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Window3D window = new Window3D())
            {
                window.Run(30.0, 0.0);
            }
        }
    }
}
