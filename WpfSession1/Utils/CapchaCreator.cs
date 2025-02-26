using System;
using System.Drawing;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Size = System.Drawing.Size;
namespace WpfSession1.Utils
{
    public class CaptchaCreater
    {
        private static readonly Random _random = new Random();
        private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static Color GetRandomColor(int min = 0, int max = 256)
        {
            return Color.FromArgb(_random.Next(min, max), _random.Next(min, max), _random.Next(min, max));
        }
        public static string GetRandomText(int length)
        {
            var text = new char[length];
            for (var i = 0; i < length; i++)
            {
                text[i] = _chars[_random.Next(_chars.Length)];
            }
            return new string(text);
        }
        public static Bitmap GetSimpleCaptcha(int length) => GetSimpleCaptcha(GetRandomText(length));
        public static Bitmap GetSimpleCaptcha(string str)
        {
            Font font = new Font("Georgia", 20);
            Size textSize = TextRenderer.MeasureText(str, font);
            Bitmap img = new Bitmap((int)3 * textSize.Width, (int)(textSize.Height * 1.5));
            using (Graphics graphics = Graphics.FromImage(img))
            {
                graphics.Clear(GetRandomColor(200, 256));
                int x = 0;
                int y = (img.Height - textSize.Height) / 2;
                foreach (char lett in str)
                {
                    int lettX = x + (textSize.Width / str.Length);
                    graphics.DrawString(lett.ToString(), font, new SolidBrush(GetRandomColor(0, 200)), lettX, y);
                    graphics.DrawLine(new Pen(GetRandomColor(180, 256)), 0, _random.Next(img.Height), img.Width, _random.Next(img.Height));
                    graphics.DrawLine(new Pen(GetRandomColor(180, 256)), 0, _random.Next(img.Height), img.Width, _random.Next(img.Height));
                    graphics.DrawLine(new Pen(GetRandomColor(180, 256)), 0, _random.Next(img.Height), img.Width, _random.Next(img.Height));
                    x += (int)(textSize.Width / str.Length * 2);
                }
            }
            return img;
        }
    }
}