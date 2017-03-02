using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Holger_hals_betvinger
{
    class Blocks
    {
        public int BlockID = 0;
        public string[] Tags = new string[] { "",""};
        public Rectangle Texture = new Rectangle(0,0,15,15);
        public Blocks(int ID, string STags)
        {
            MakeBlock(ID);
            if (STags != null)
            {
                Tags[0] = STags;
                Tags[1] = FindText(STags,"Text:",";");
            }
        }
        public Blocks(int ID)
        {
            MakeBlock(ID);
        }

        private void MakeBlock(int ID)
        {
            Texture.X = ID % 16 * 16;
            Texture.Y = ID / 16 * 16;
            BlockID = ID;
        }
        private string FindText(string text, string start, string end)
        {
            if (text.Contains(start) != false && text.Contains(end) != false)
            {
                return text.Substring(text.IndexOf(start) + start.Length, text.IndexOf(end, text.IndexOf(start)) - text.IndexOf(start) - start.Length);
            }
            else
            {
                return null;
            }
        }
    }
}
