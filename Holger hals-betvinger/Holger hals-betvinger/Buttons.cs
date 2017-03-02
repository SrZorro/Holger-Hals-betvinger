using System.Drawing;

namespace Holger_hals_betvinger
{
    class Buttons
    {
        public string Text = "Nothing";
        public Rectangle Coords = new Rectangle(0,0,15,15);
        public int Texture = 0;
        public int LookingTexture = 0;
        
        public Buttons(string OnButton,Point Coordinates, int ImageN)
        {
            Text = OnButton;
            Texture = ImageN;
            Coords.X = Coordinates.X;
            Coords.Y = Coordinates.Y;

            if (Texture == 0) { Coords.Width = 48; Coords.Height = 48; LookingTexture = 2; }
            if (Texture == 1) { Coords.Width = 128; Coords.Height = 80; LookingTexture = 3; }
            if (Texture == 2) { Coords.Width = 48; Coords.Height = 48; }
            if (Texture == 3) { Coords.Width = 128; Coords.Height = 80; }
            if (Texture == 4) { Coords.Width = 80; Coords.Height = 48; LookingTexture = 5; }
            if (Texture == 5) { Coords.Width = 80; Coords.Height = 48;}
            if (Texture == 6) { Coords.Width = 112; Coords.Height = 48; LookingTexture = 7; }
            if (Texture == 7) { Coords.Width = 112; Coords.Height = 48; }
        }
    }
}
