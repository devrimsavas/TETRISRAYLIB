//ColorPalette.cs

using Raylib_cs;

namespace TetrisV1.models
{
    public static class ColorPalette
    {
        public static Random random = new Random();
        public static Color[] AllColors = new Color[]
        {
            //Color.Black,
            Color.Red,
            Color.Blue,
            Color.Beige,
            Color.White,
            Color.Gray,
            Color.DarkGray,
            Color.LightGray,
            Color.Orange,
            Color.Yellow,
            Color.Gold,
            Color.Pink,
            Color.Magenta,
            Color.Purple,
            Color.Violet,
            Color.Green,
            Color.Lime,
            Color.DarkGreen,
            Color.SkyBlue,
            Color.Brown,
            Color.Maroon,
            Color.RayWhite,
            Color.DarkBlue
        };

        public static Color[] GrayPalette =
        [
            Color.Black,
            Color.Blue,
            Color.Magenta,
            Color.LightGray,
        ];

        public static Color[] CgaPalette = new Color[]
        {
            //new Color(0, 0, 0, 255),       // 0 Black
            new Color(0, 0, 170, 255),     // 1 Blue
            new Color(0, 170, 0, 255),     // 2 Green
            new Color(0, 170, 170, 255),   // 3 Cyan
            new Color(170, 0, 0, 255),     // 4 Red
            new Color(170, 0, 170, 255),   // 5 Magenta
            new Color(170, 85, 0, 255),    // 6 Brown
            new Color(170, 170, 170, 255), // 7 Light Gray
            new Color(85, 85, 85, 255),    // 8 Dark Gray
            new Color(85, 85, 255, 255),   // 9 Light Blue
            new Color(85, 255, 85, 255),   // 10 Light Green
            new Color(85, 255, 255, 255),  // 11 Light Cyan
            new Color(255, 85, 85, 255),   // 12 Light Red
            new Color(255, 85, 255, 255),  // 13 Light Magenta
            new Color(255, 255, 85, 255),  // 14 Yellow
            new Color(255, 255, 255, 255)  // 15 White

        };
        public static Color PickRandomColor()
        {
            var color = AllColors[random.Next(AllColors.Length)];
            return color;
        }
        public static Color PickGrayColor()
        {
            var color = GrayPalette[random.Next(AllColors.Length)];
            return color;
        }
    }
}
