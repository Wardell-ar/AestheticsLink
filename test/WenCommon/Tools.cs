using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WenCommon
{
    public class Tools
    {
        public static string CreateValidateString()
        {
            //供验证码展示的数据
            string chars = "abcdefghijklmnopqrstuvwxyz";
            Random r = new(DateTime.Now.Millisecond);
            string validateString = "";
            int length = 4;
            for (int i = 0; i < length; i++)
            {
                validateString += chars[r.Next(chars.Length)];
            }
            return validateString;
        }
        public static Byte[] CreateValidateCodeBuffer(string validateCode)
        {
            //创建画布
            using Bitmap bitmap = new Bitmap(200, 60);

            //创建画笔
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);//白色覆盖画布

            //设置字体
            Font font = new("微软雅黑", 12, FontStyle.Bold | FontStyle.Italic);

            //计算字符串长度
            var size = graphics.MeasureString(validateCode, font);
            using Bitmap bitmapText = new(Convert.ToInt32(Math.Ceiling(size.Width)), Convert.ToInt32(Math.Ceiling(size.Height)));

            //生成图片
            Graphics graphicsText = Graphics.FromImage(bitmapText);

            //将图片缩放到画布上
            //设置画图参数
            RectangleF rf = new(0, 0, bitmap.Width, bitmap.Height);
            LinearGradientBrush brush = new(rf, Color.Red, Color.DarkBlue, 1.2f, true);
            //绘制
            graphicsText.DrawString(validateCode, font, brush, 0, 0);
            graphics.DrawImage(bitmapText, 10, 10, 190, 50);
            //把图片放到缓冲区
            MemoryStream memoryStream = new();
            bitmap.Save(memoryStream, ImageFormat.Jpeg);

            return memoryStream.ToArray();


        }
    }
}