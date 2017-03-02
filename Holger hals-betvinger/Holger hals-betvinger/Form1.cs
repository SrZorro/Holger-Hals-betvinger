using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Holger_hals_betvinger
{
    public partial class Form1 : Form
    {
        bool[] PressedKeys = new bool[512];
        Bitmap Textures = new Bitmap(512, 512);
        Bitmap[] Layers = new Bitmap[5];
        string[] LayersText = new string[5];
        string WorldString = "";
        Point MaxSize = new Point(500, 500);
        Bitmap ViewedBlocks = new Bitmap(256, 144);
        Point PlayerCoords = new Point(256, 256);
        Bitmap World = new Bitmap(512, 512);
        Bitmap Hals = new Bitmap(512, 512);
        int Animation = 0;
        Point Direction = new Point(0, 3);
        Random RandomNumber = new Random();
        Point OldPlayerCoords = new Point(256, 256);
        int Plates = 0;
        int[,] BeenAt = new int[32, 32];
        int[,] Hitbox = new int[32, 32];
        int[,] Doors = new int[32, 32];
        Point[] Stones = new Point[100];
        int Moves = 2;
        bool MadeLevel = false;
        int[,] AllHitbox = new int[32, 32];
        int[,] StoneCoords = new int[32, 32];
        Blocks[][,] BlockWorld = new Blocks[5][,];
        SoundPlayer[] AllSounds = new SoundPlayer[] { new SoundPlayer() };
        Bitmap[] Buttons = new Bitmap[50];
        int MenuID = 0;
        StringFormat CenterString = new StringFormat();
        bool[] LookingButton = new bool[50];
        Buttons[] AllButtons = new Buttons[32];
        string YourLanguage = "English";
        Point OldMouseCoords = new Point(0, 0);
        int LevelSide = 0;
        int InLevel = 0;
        Bitmap Background = new Bitmap(400,250);
        int[] ControlsN = new int[] { 82, 27, 122, 65, 68, 87, 83 };
        string[] ControlsNames = new string[] {"Reset","Goto Menu","Fullscreen","Left","Right","Up","Down" };
        int ChangeButton = 0;
        bool ShowSign = false;
        string SavedSignText = "";
        string[] Levels = new string[]
    {
                    /*1*/Properties.Resources.StartLevel,
                    /*2*/Properties.Resources.NoBack,
                    /*3*/Properties.Resources.LongFirstLevel,
                    /*4*/Properties.Resources.EasyMaze,
                    /*5*/Properties.Resources.HowToButton,
                    /*6*/Properties.Resources.LongGreenButtonFirst,
                    /*7*/Properties.Resources.ALotOfButtons,
                    /*8*/Properties.Resources.DoubleDoorDoublePlates,
                    /*9*/Properties.Resources.SmallPLateLevel,
                    /*10*/Properties.Resources.PlateCastle,
                    /*11*/Properties.Resources.BigButtonLevel,
                    /*12*/Properties.Resources.FirstStone,
                    /*13*/Properties.Resources.RightStoneWay,
                    /*14*/Properties.Resources.TriplePlatesStone,
                    /*15*/Properties.Resources.StoneWalls,
                    /*16*/Properties.Resources.NewYellowPlate,
                    /*17*/Properties.Resources.FirstPuzzle,
                    /*18*/Properties.Resources.Arrows,
                    /*19*/Properties.Resources.FirstTwoStone,
                    /*20*/Properties.Resources.RandomPlates,
                    /*21*/Properties.Resources.StoneGarden,
                    /*22*/Properties.Resources.TwoStones,
                    /*23*/Properties.Resources.MazeRunner,
                    /*24*/Properties.Resources.GreenWalls,
                    /*25*/Properties.Resources.RollingStones,
                    /*26*/Properties.Resources.BigDoorRoom,
                    /*27*/Properties.Resources.StoneLevel,
                    /*28*/Properties.Resources.GhostWalls,
                    /*29*/Properties.Resources.InvisibleMaze,
                    /*30*/Properties.Resources.End
    };

        //Misc
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Keys == null)
            {
                Properties.Settings.Default.Keys = ControlsN;
            }
            else
            {
                if (Properties.Settings.Default.Keys.Length != ControlsN.Length)
                {
                    Properties.Settings.Default.Keys = ControlsN;
                }
            }
            Properties.Settings.Default.Save();
            ControlsN = Properties.Settings.Default.Keys;
            CenterString.Alignment = StringAlignment.Center;
            CenterString.LineAlignment = StringAlignment.Center;
            Textures = Properties.Resources.Textures;
            Screen.Image = new Bitmap(1, 1);
            ReSize();
            Text = Language("Holger hals-betvinger") + " 1.0.0.0";

            DesignTextures();

            LoadMenu();

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
        void DesignTextures()
        {
            Graphics DrawBackground = Graphics.FromImage(Background);

            for (int a = 0; a < 25; a++)
            {
                for (int b = 0; b < 16; b++)
                {
                    if (a == 0 || b == 0 || a == 24 || b == 15)
                    {
                        DrawBackground.DrawImage(Textures, a * 16, b * 16, new Blocks(3).Texture, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        DrawBackground.DrawImage(Textures, a * 16, b * 16, new Blocks(1).Texture, GraphicsUnit.Pixel);
                    }
                }
            }
            DrawBackground.FillRectangle(new SolidBrush(Color.FromArgb(100,0,0,0)), new Rectangle(0,0,400,250));

            DrawBackground.Dispose();

            Buttons[0] = DrawButton(new int[3, 3]
{
                    { 3,3,3},
                    {3,1,3 },
                    {3,3,3 }
});
            Buttons[1] = DrawButton(new int[8, 5]
{
                    { 3,3,3,3,3},
                    {3,1,1,1,3 },
                    {3,1,1,1,3 },
                    {3,1,1,1,3 },
                    {3,1,1,1,3 },
                    {3,1,1,1,3 },
                    {3,1,1,1,3 },
                    { 3,3,3,3,3}
});
            Buttons[2] = DrawButton(new int[3, 3]
{
                    {10,10,10},
                    {10,7,10 },
                    {10,10,10 }
});
            Buttons[3] = DrawButton(new int[8, 5]
{
                    { 10,10,10,10,10},
                    {10,7,7,7,10 },
                    {10,7,7,7,10 },
                    {10,7,7,7,10 },
                    {10,7,7,7,10 },
                    {10,7,7,7,10 },
                    {10,7,7,7,10 },
                    { 10,10,10,10,10}
});
            Buttons[4] = DrawButton(new int[5, 3]
{
                    { 3,3,3},
                    {3,1,3 },
                    {3,1,3 },
                    {3,1,3 },
                    {3,3,3 }
});
            Buttons[5] = DrawButton(new int[5, 3]
{
                    { 10,10,10},
                    {10,7,10 },
                    {10,7,10 },
                    {10,7,10 },
                    {10,10,10 }
});
            Buttons[6] = DrawButton(new int[7, 3]
{
                    { 3,3,3},
                    {3,1,3 },
                    {3,1,3 },
                    {3,1,3 },
                    {3,1,3 },
                    {3,1,3 },
                    {3,3,3 }
});
            Buttons[7] = DrawButton(new int[7, 3]
{
                    { 10,10,10},
                    {10,7,10 },
                    {10,7,10 },
                    {10,7,10 },
                    {10,7,10 },
                    {10,7,10 },
                    {10,10,10 }
});
            Buttons[8] = DrawButton(new int[16, 3]
{
                    {61,77,93 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {62,78,94 },
                    {63,79,95 }
});
        }

        //Interact
        private void GameTick_Tick(object sender, EventArgs e)
        {
            if (Animation > 7)
            {
                Direction.Y = Direction.X;
                Direction.X = 0;
                Animation = 0;
                if (BlockWorld[1][31 - PlayerCoords.X / 16, 31 - PlayerCoords.Y / 16].BlockID == 53)
                {
                    Plates++;
                    OpenDoors(true);
                }
                if (BlockWorld[1][31 - PlayerCoords.X / 16, 31 - PlayerCoords.Y / 16].BlockID == 70)
                {
                    Plates++;
                    OpenDoors(true);
                }
                if (BlockWorld[1][31 - PlayerCoords.X / 16, 31 - PlayerCoords.Y / 16].BlockID == 65)
                {
                    SavedSignText = "" + BlockWorld[1][31 - PlayerCoords.X / 16, 31 - PlayerCoords.Y / 16].Tags[1];
                    ShowSign = true;
                }
                else if (ShowSign)
                {
                    ShowSign = false;
                }
                if (BlockWorld[4][31 - PlayerCoords.X / 16, 31 - PlayerCoords.Y / 16].BlockID == 242)
                {
                    InLevel++;
                    if (InLevel > Properties.Settings.Default.Level)
                    {
                        Properties.Settings.Default.Level++;
                        Properties.Settings.Default.Save();
                    }
                    LoadLevel();
                }
            }
            AllHitbox = Hitbox;
            StoneCoords = new int[32, 32];
            Point EmptyPoint = new Point(0,0);
            for (int a = 0; a < Stones.Length; a++)
            {
                if (Stones[a] != EmptyPoint)
                {
                    AllHitbox[Stones[a].X / 16, Stones[a].Y / 16] = 2;
                    StoneCoords[Stones[a].X / 16, Stones[a].Y / 16] = 1;
                }
            }
            if (Direction.X == 0)
            {
                OldPlayerCoords = PlayerCoords;
                if (BlockWorld[1][31 - (PlayerCoords.X / 16 + 1), 31 - PlayerCoords.Y / 16].BlockID != 57 && PressedKeys[ControlsN[3]] == true && ((BeenAt[PlayerCoords.X / 16 + 1, PlayerCoords.Y / 16] == 0 && Hitbox[PlayerCoords.X / 16 + 1, PlayerCoords.Y / 16] == 0) || (StoneCoords[PlayerCoords.X / 16 + 1, PlayerCoords.Y / 16] == 1 && AllHitbox[PlayerCoords.X / 16 + 2, PlayerCoords.Y / 16] == 0))) { Direction.X = 1; BeenAt[PlayerCoords.X / 16 + 1, PlayerCoords.Y / 16] = Moves; Moves += 1; }
                else if (BlockWorld[1][31 - (PlayerCoords.X / 16 - 1), 31 - PlayerCoords.Y / 16].BlockID != 74 && PressedKeys[ControlsN[4]] == true && ((BeenAt[PlayerCoords.X / 16 - 1, PlayerCoords.Y / 16] == 0 && Hitbox[PlayerCoords.X / 16 - 1, PlayerCoords.Y / 16] == 0) || (StoneCoords[PlayerCoords.X / 16 - 1, PlayerCoords.Y / 16] == 1 && AllHitbox[PlayerCoords.X / 16 - 2, PlayerCoords.Y / 16] == 0))) { Direction.X = 2; BeenAt[PlayerCoords.X / 16 - 1, PlayerCoords.Y / 16] = Moves; Moves += 1; }
                else if (BlockWorld[1][31 - PlayerCoords.X / 16, 31 - (PlayerCoords.Y / 16 + 1)].BlockID != 58 && PressedKeys[ControlsN[5]] == true && ((BeenAt[PlayerCoords.X / 16, PlayerCoords.Y / 16 + 1] == 0 && Hitbox[PlayerCoords.X / 16, PlayerCoords.Y / 16 + 1] == 0) || (StoneCoords[PlayerCoords.X / 16, PlayerCoords.Y / 16 + 1] == 1 && AllHitbox[PlayerCoords.X / 16, PlayerCoords.Y / 16 + 2] == 0))) { Direction.X = 3; BeenAt[PlayerCoords.X / 16, PlayerCoords.Y / 16 + 1] = Moves; Moves += 1; }
                else if (BlockWorld[1][31 - PlayerCoords.X / 16, 31 - (PlayerCoords.Y / 16 - 1)].BlockID != 73 && PressedKeys[ControlsN[6]] == true && ((BeenAt[PlayerCoords.X / 16, PlayerCoords.Y / 16 - 1] == 0 && Hitbox[PlayerCoords.X / 16, PlayerCoords.Y / 16 - 1] == 0) || (StoneCoords[PlayerCoords.X / 16, PlayerCoords.Y / 16 - 1] == 1 && AllHitbox[PlayerCoords.X / 16, PlayerCoords.Y / 16 - 2] == 0))) { Direction.X = 4; BeenAt[PlayerCoords.X / 16, PlayerCoords.Y / 16 - 1] = Moves; Moves += 1; }
            }
            if (Direction.X == 1) { PlayerCoords.X += 2; }
            else if (Direction.X == 2) { PlayerCoords.X -= 2; }
            else if (Direction.X == 3) { PlayerCoords.Y += 2; }
            else if (Direction.X == 4) { PlayerCoords.Y -= 2; }
            if (Direction.X != 0)
            {
                Animation++;
                DrawHals();

                for (int a = 0; a < Stones.Length; a++)
                {
                    if (Stones[a].X / 16 == PlayerCoords.X / 16 && Stones[a].Y / 16 == PlayerCoords.Y / 16)
                    {
                        if (BlockWorld[1][31 - Stones[a].X / 16, 31 - Stones[a].Y / 16].BlockID == 53)
                        {
                            Plates--;
                            OpenDoors(false);
                        }
                        if (BlockWorld[1][31 - Stones[a].X / 16, 31 - Stones[a].Y / 16].BlockID == 69)
                        {
                            Plates--;
                            OpenDoors(false);
                        }
                        if (Direction.X == 1) { Stones[a].X += 16;}
                        if (Direction.X == 2) { Stones[a].X -= 16;}
                        if (Direction.X == 3) { Stones[a].Y += 16;}
                        if (Direction.X == 4) { Stones[a].Y -= 16;}
                        if (BlockWorld[1][31 - Stones[a].X / 16, 31 - Stones[a].Y / 16].BlockID == 53)
                        {
                            Plates++;
                            OpenDoors(true);
                        }
                        if (BlockWorld[1][31 - Stones[a].X / 16, 31 - Stones[a].Y / 16].BlockID == 69)
                        {
                            Plates++;
                            OpenDoors(true);
                        }
                        if (BeenAt[Stones[a].X / 16, Stones[a].Y / 16] > 0)
                        {
                            PlayLevel();
                        }
                    }
                }
            }
            if (Direction.Y == 0 && Direction.X != 0) { Direction.Y = Direction.X; }

            Redraw();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (MenuID == 4)
            {
                ControlsN[ChangeButton] = e.KeyValue;
                ChangeButton++;
                if (ChangeButton < ControlsN.Length)
                {
                    NewButtonsMenu(ChangeButton);
                }
                else
                {
                    Properties.Settings.Default.Keys = ControlsN;
                    Properties.Settings.Default.Save();
                    LoadMenu();
                }
            }
            else
            {
                if (PressedKeys[75] == true && e.KeyValue != 75)
                {
                    Text = "" + e.KeyValue;
                }
                if (PressedKeys[84] == false && e.KeyValue == 84)
                {
                    OpenFileDialog OpenTexture = new OpenFileDialog() { Title = "Open textures" };
                    if (OpenTexture.ShowDialog() == DialogResult.OK)
                    {
                        Textures = new Bitmap(OpenTexture.FileName);
                    }
                }
                if (PressedKeys[76] == false && e.KeyValue == 76)
                {
                    OpenFileDialog SaveFile = new OpenFileDialog() { Title = "Load file" };
                    if (SaveFile.ShowDialog() == DialogResult.OK)
                    {
                        MadeLevel = true;
                        StreamReader ReadSafe = new StreamReader(SaveFile.FileName);


                        WorldString = ReadSafe.ReadToEnd();

                        ReadSafe.Dispose();
                        LoadSafe();
                    }
                }

                if (PressedKeys[ControlsN[1]] == false && e.KeyValue == ControlsN[1])
                {
                    LoadMenu();
                }
                if (e.KeyValue == ControlsN[0])
                {
                    PlayLevel();
                }
                if (PressedKeys[ControlsN[2]] == false && e.KeyValue == ControlsN[2])
                {
                    Fullscreen();
                }
            }
            PressedKeys[e.KeyValue] = true;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            PressedKeys[e.KeyValue] = false;
        }

        //Show
        private void Form1_Resize(object sender, EventArgs e)
        {
            ReSize();
        }
        void Fullscreen()
        {
            if (WindowState != FormWindowState.Maximized)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
        }
        void ReSize()
        {
            if (decimal.Divide(Size.Height, 9) >= decimal.Divide(Size.Width, 16))
            {
                MaxSize.X = Size.Width;
                Screen.Size = new Size(MaxSize.X, Convert.ToInt32(decimal.Divide(MaxSize.X, 16) * 9));
                MaxSize = new Point(Screen.Size.Width, Screen.Size.Height);
            }
            else
            {
                MaxSize.Y = Size.Height;
                Screen.Size = new Size(Convert.ToInt32(decimal.Divide(MaxSize.Y, 9) * 16), MaxSize.Y);
                MaxSize = new Point(Screen.Size.Width, Screen.Size.Height);
            }
            //Screen.Image = new Bitmap(MaxSize.X,MaxSize.Y);
            Redraw();
        }
        void Redraw()
        {
            Screen.Image = new Bitmap(MaxSize.X, MaxSize.Y);
            if (GameTick.Enabled == true)
            {
                ViewedBlocks = new Bitmap(256, 144);


                Graphics DrawView = Graphics.FromImage(ViewedBlocks);
                DrawView.DrawImage(World, PlayerCoords.X - 512 + 144, PlayerCoords.Y - 512 + 64);
                DrawView.DrawImage(Hals, PlayerCoords.X - 512 + 128, PlayerCoords.Y - 512 + 64);
                if (Direction.X == 1 || (Direction.X == 0 && Direction.Y == 1)) { DrawView.DrawImage(Textures, 128, 48, new Rectangle(16, 13 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.X == 2 || (Direction.X == 0 && Direction.Y == 2)) { DrawView.DrawImage(Textures, 128, 48, new Rectangle(0, 12 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.X == 3 || (Direction.X == 0 && Direction.Y == 3)) { DrawView.DrawImage(Textures, 128, 48, new Rectangle(0, 13 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.X == 4 || (Direction.X == 0 && Direction.Y == 4)) { DrawView.DrawImage(Textures, 128, 48, new Rectangle(16, 12 * 16, 15, 15), GraphicsUnit.Pixel); }

                //DrawView.DrawImage(Textures, 0, 128, new Rectangle(0, 16 * 11, 15, 15), GraphicsUnit.Pixel);
                //for (int a = 1; a < 15; a++)
                //{
                //    DrawView.DrawImage(Textures, a*16, 128, new Rectangle(16, 16 * 11, 15, 15), GraphicsUnit.Pixel);
                //}
                //DrawView.DrawImage(Textures, 240, 128, new Rectangle(32, 16 * 11, 15, 15), GraphicsUnit.Pixel);

                for (int a = 0; a < Stones.Length; a++)
                {
                    if (Stones[a].X != 0 && Stones[a].Y != 0)
                    {
                        DrawView.DrawImage(Textures, (PlayerCoords.X - Stones[a].X + 128), (PlayerCoords.Y - Stones[a].Y + 48), new Rectangle(96, 3 * 16, 15, 15), GraphicsUnit.Pixel);
                    }
                }

                if (ShowSign == true)
                {
                    // Text:tx:0,ty:0,tdx:256,tdy:48,Size:12,Color:SandyBrown,Kage er godt.%;
                    DrawView.DrawImage(Buttons[8], 0, 0);
                    DrawView.DrawImage(SignText(SavedSignText), 0, 0);
                }

                DrawView.Dispose();
            }


            Graphics DrawScreen = Graphics.FromImage(Screen.Image);
            DrawScreen.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            DrawScreen.DrawImage(ViewedBlocks, 0, 0, MaxSize.X, MaxSize.Y);
            DrawScreen.Dispose();
        }
        void DrawHals()
        {
            Graphics DrawHals = Graphics.FromImage(Hals);

            if (Direction.Y == Direction.X)
            {
                if (Direction.X == 1) { DrawHals.DrawImage(Textures, 512 - PlayerCoords.X + 16, 512 - PlayerCoords.Y - 16, new Rectangle(32 + RandomNumber.Next(0, 14), 14 * 16, 2, 15), GraphicsUnit.Pixel); }
                if (Direction.X == 2) { DrawHals.DrawImage(Textures, 512 - PlayerCoords.X - 2, 512 - PlayerCoords.Y - 16, new Rectangle(32 + RandomNumber.Next(0, 14), 14 * 16, 2, 15), GraphicsUnit.Pixel); }
                if (Direction.X == 3) { DrawHals.DrawImage(Textures, 512 - PlayerCoords.X, 512 - PlayerCoords.Y, new Rectangle(16, 14 * 16 + RandomNumber.Next(0, 14), 15, 2), GraphicsUnit.Pixel); }
                if (Direction.X == 4) { DrawHals.DrawImage(Textures, 512 - PlayerCoords.X, 512 - PlayerCoords.Y - 18, new Rectangle(16, 14 * 16 + RandomNumber.Next(0, 14), 15, 2), GraphicsUnit.Pixel); }
            }
            else if (Animation > 0)
            {
                if (Direction.Y == 1 && Direction.X == 3) { DrawHals.DrawImage(Textures, 512 - OldPlayerCoords.X, 512 - OldPlayerCoords.Y - 16, new Rectangle(48 + ((Animation - 1) / 2 * 32), 14 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.Y == 1 && Direction.X == 4) { DrawHals.DrawImage(Textures, 512 - OldPlayerCoords.X, 512 - OldPlayerCoords.Y - 16, new Rectangle(48 + ((Animation - 1) / 2 * 32), 13 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.Y == 2 && Direction.X == 4) { DrawHals.DrawImage(Textures, 512 - OldPlayerCoords.X, 512 - OldPlayerCoords.Y - 16, new Rectangle(64 + ((Animation - 1) / 2 * 32), 13 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.Y == 2 && Direction.X == 3) { DrawHals.DrawImage(Textures, 512 - OldPlayerCoords.X, 512 - OldPlayerCoords.Y - 16, new Rectangle(64 + ((Animation - 1) / 2 * 32), 14 * 16, 15, 15), GraphicsUnit.Pixel); }

                if (Direction.Y == 3 && Direction.X == 2) { DrawHals.DrawImage(Textures, 512 - OldPlayerCoords.X, 512 - OldPlayerCoords.Y - 16, new Rectangle(48 + ((Animation - 1) / 2 * 32), 11 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.Y == 3 && Direction.X == 1) { DrawHals.DrawImage(Textures, 512 - OldPlayerCoords.X, 512 - OldPlayerCoords.Y - 16, new Rectangle(64 + ((Animation - 1) / 2 * 32), 11 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.Y == 4 && Direction.X == 2) { DrawHals.DrawImage(Textures, 512 - OldPlayerCoords.X, 512 - OldPlayerCoords.Y - 16, new Rectangle(48 + ((Animation - 1) / 2 * 32), 12 * 16, 15, 15), GraphicsUnit.Pixel); }
                if (Direction.Y == 4 && Direction.X == 1) { DrawHals.DrawImage(Textures, 512 - OldPlayerCoords.X, 512 - OldPlayerCoords.Y - 16, new Rectangle(64 + ((Animation - 1) / 2 * 32), 12 * 16, 15, 15), GraphicsUnit.Pixel); }



            }

            DrawHals.Dispose();
        }
        void OpenDoors(bool Add)
        {
            for (int a = 0; a < 32; a++)
            {
                for (int b = 0; b < 32; b++)
                {
                    if (Add)
                    {
                        Doors[a, b]--;
                    }
                    else
                    {
                        Doors[a, b]++;
                    }
                    if (Doors[a, b] == 0)
                    {
                        Graphics DrawNewDoor = Graphics.FromImage(World);

                        Hitbox[a, b] = 0;
                        DrawNewDoor.DrawImage(Textures, 512 - a * 16 - 16, 512 - b * 16 - 16, new Rectangle(16, 3 * 16, 15, 15), GraphicsUnit.Pixel);

                        DrawNewDoor.Dispose();
                    }
                    if (Doors[a, b] > 0)
                    {
                        Graphics DrawNewDoor = Graphics.FromImage(World);

                        Hitbox[a, b] = 1;
                        DrawNewDoor.DrawImage(Textures, 512 - a * 16 - 16, 512 - b * 16 - 16, new Rectangle(0, 3 * 16, 15, 15), GraphicsUnit.Pixel);

                        DrawNewDoor.Dispose();
                    }
                }
            }
        }
        Bitmap SignText(string RawText)
        {
            int Start = 0;
            int End = 0;
            string[] TextThing = new string[100];
            Bitmap SignTextB = new Bitmap(256, 48);
            for (int a = 0; a < RawText.Length; a++)
            {
                if (RawText.Substring(a,1) == "%")
                {
                    for (int b = 0; b < 100; b++)
                    {
                        if (TextThing[b] == null)
                        {
                            End = a;
                            TextThing[b] = RawText.Substring(Start, End - Start);
                            Start = a;
                            break;
                        }
                    }
                }
            }
            Graphics DrawSign = Graphics.FromImage(SignTextB);
            for (int a = 0; a < 100; a++)
            {
                if (TextThing[a] == null)
                {
                    break;
                }
                else
                {
                    //tx:1,ty:1,tdx:10,tdy:10,
                    Rectangle Box = new Rectangle(0,0,10,10);
                    int TextSize = 10;
                    string TextColor = "Yellow";
                    TextThing[a] = TextThing[a].Replace("%", "");

                    if (FindText(TextThing[a], "tx:", ",") == null) { Box.X = 0; } else { Box.X = Convert.ToInt32(FindText(TextThing[a], "tx:", ",")); TextThing[a] = TextThing[a].Replace("tx:" + Box.X + ",",""); }
                    if (FindText(TextThing[a], "ty:", ",") == null) { Box.Y = 0; } else { Box.Y = Convert.ToInt32(FindText(TextThing[a], "ty:", ",")); TextThing[a] = TextThing[a].Replace("ty:" + Box.Y + ",", ""); }
                    if (FindText(TextThing[a], "tdx:", ",") == null) { Box.Width = 256; } else { Box.Width = Convert.ToInt32(FindText(TextThing[a], "tdx:", ",")); TextThing[a] = TextThing[a].Replace("tdx:" + Box.Width + ",", ""); }
                    if (FindText(TextThing[a], "tdy:", ",") == null) { Box.Height = 48; } else { Box.Height = Convert.ToInt32(FindText(TextThing[a], "tdy:", ",")); TextThing[a] = TextThing[a].Replace("tdy:" + Box.Height + ",", ""); }
                    if (FindText(TextThing[a], "Size:", ",") == null) { TextSize = 10; } else { TextSize = Convert.ToInt32(FindText(TextThing[a], "Size:", ",")); TextThing[a] = TextThing[a].Replace("Size:" + TextSize + ",", ""); }
                    if (FindText(TextThing[a], "Color:", ",") == null) { TextColor = "yellow"; } else { TextColor = FindText(TextThing[a], "Color:", ","); TextThing[a] = TextThing[a].Replace("Color:" + TextColor + ",", ""); }

                    StringFormat MidleString = new StringFormat();
                    MidleString.Alignment = StringAlignment.Center;

                    DrawSign.DrawString(Language(TextThing[a]), new Font("", TextSize), new SolidBrush(Color.FromName(TextColor)), new Rectangle(Box.X, Box.Y, Box.Width - Box.X, Box.Height - Box.Y),MidleString);
                }
            }
            DrawSign.Dispose();
            return SignTextB;
        }

        //Load World
        void LoadSafe()
        {
            MenuID = 1000;
            Bitmap EmptyBitmap = new Bitmap(512, 512);
            Doors = new int[32, 32];
            World = EmptyBitmap;

            for (int d = 0; d < 5; d++)
            {
                Layers[d] = EmptyBitmap;
                BlockWorld[d] = new Blocks[32, 32];
                for (int a = 0; a < 32; a++)
                {
                    for (int b = 0; b < 32; b++)
                    {
                        BlockWorld[d][a,b] = new Blocks(0);
                    }
                }
            }
            for (int c = 0; c < 5; c++)
            {
                Graphics DrawBlock = Graphics.FromImage(Layers[c]);

                string LoadText = FindText(WorldString, "Layer" + c + ">", "<Layer" + c);
                LayersText[c] = LoadText;

                if (LoadText != null)
                {
                    for (int a = 0; a < 32; a++)
                    {
                        for (int b = 0; b < 32; b++)
                        {
                            if (FindText(LoadText, "x=" + a + ",y=" + b + ":(", ");") != null)
                            {
                                string EBlockInfo = ":-:" + FindText(LoadText, "x=" + a + ",y=" + b + ":(", ");") + ",";
                                int EBlockId = Convert.ToInt32(FindText(EBlockInfo, ":-:", ","));
                                string EBlockTag = "";
                                if (FindText(EBlockInfo, "Tags:[", "]") != null)
                                {
                                    EBlockTag = FindText(EBlockInfo, "Tags:[", "]");
                                }
                                BlockWorld[c][a, b] = new Blocks(EBlockId, EBlockTag);
                                if (c <= 2)
                                {
                                    DrawBlock.DrawImage(Textures, a * 16, b * 16, BlockWorld[c][a, b].Texture, GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                }
                DrawBlock.Dispose();
            }
            PlayLevel();
        }
        void PlayLevel()
        {
            GameTick.Enabled = true;
            Direction = new Point(0, 3);
            Animation = 0;
            Hals = new Bitmap(512,512);
            BeenAt = new int[32, 32];
            Stones = new Point[100];
            Hitbox = new int[32, 32];
            MenuID = 1000;
            ShowSign = false;


            for (int b = 0; b < 32; b++)
            {
                for (int c = 0; c < 32; c++)
                {
                    if (BlockWorld[4][b, c].BlockID == 243)
                    {
                        PlayerCoords = new Point(512 - b * 16 - 16, 512 - c * 16);
                        Graphics DrawHals = Graphics.FromImage(Hals);

                        BeenAt[31 - b, 31 - c] = 1;
                        BeenAt[31 - b, 31 - c + 1] = 1;
                        DrawHals.DrawImage(Textures, b * 16 + 16, c * 16, new Rectangle(0, 14 * 16, 15, 15), GraphicsUnit.Pixel);


                        DrawHals.Dispose();
                    }
                    if (BlockWorld[4][b, c].BlockID == 240)
                    {
                        Hitbox[31 - b, 31 - c] = 1;
                    }
                    if (BlockWorld[3][b, c].BlockID == 54)
                    {
                        for (int a = 0; a < Stones.Length; a++)
                        {
                            if (Stones[a].X == 0 && Stones[a].Y == 0)
                            {
                                Stones[a] = new Point((31 - b) * 16, (31 - c) * 16);
                                break;
                            }
                        }
                    }
                    if (BlockWorld[1][b, c].BlockID == 48)
                    {
                        int DoorNumber = BlockWorld[3][b, c].BlockID - 95;
                        Doors[31 - b, 31 - c] = DoorNumber;
                        Hitbox[31 - b, 31 - c] = 1;
                    }
                    else
                    {
                        Doors[31 - b, 31 - c] = -20;
                    }
                }
            }

            Graphics DrawWorld = Graphics.FromImage(World);
            for (int a = 0; a < 3; a++)
            {
                DrawWorld.DrawImage(Layers[a], 0, 0);
            }
            DrawWorld.Dispose();
            OpenDoors(true);
            OpenDoors(false);

            Redraw();

        }
        void LoadLevel()
        {
            Screen.Image = new Bitmap(1, 1);

            if (MadeLevel == false)
            {
                WorldString = Levels[InLevel - 1];
            }
            LoadSafe();
            MenuID = 1000;
        }

        //Use Menu
        private void Screen_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < 32; a++)
            {
                if (AllButtons[a] == null)
                {
                    
                }
                else
                {
                    if (MouseOn(new Rectangle(AllButtons[a].Coords.X, AllButtons[a].Coords.Y, AllButtons[a].Coords.Width, AllButtons[a].Coords.Height)))
                    {
                        if (MenuID == 3 && (a == 2 || a == 3))
                        {
                            LoadMenu();
                            break;
                        }
                        else if ((MenuID == 4 || MenuID == 5) && (a == 0 || a == 1))
                        {
                            LoadMenu();
                            break;
                        }
                        else if (a == 1)
                        {
                            if (MenuID == 1)
                            {
                                LevelSide = 0;
                                LevelMenu();
                                break;
                            }
                            else if (MenuID == 2 && Math.Ceiling(decimal.Divide(Properties.Settings.Default.Level, 10)) > LevelSide + 1)
                            {
                                LevelSide++;
                                LevelMenu();
                                break;
                            }
                            else if (MenuID == 2)
                            {
                                LoadMenu();
                                break;
                            }
                            else if (MenuID == 3)
                            {
                                NewButtonsMenu(0);
                                break;
                            }
                        }
                        else if (a == 0)
                        {
                            if (MenuID == 2 && LevelSide == 0)
                            {
                                LoadMenu();
                                break;
                            }
                            else if (MenuID == 2)
                            {
                                LevelSide--;
                                LevelMenu();
                                break;
                            }
                            else if (MenuID == 1)
                            {
                                OptionMenu();
                                break;
                            }
                            else if (MenuID == 3)
                            {
                                LangMenu();
                                break;
                            }
                        }
                        else if (MenuID == 5)
                        {
                            if (a >= 2)
                            {
                                YourLanguage = AllButtons[a].Text;
                                LoadMenu();
                                break;
                            }
                        }
                        else if (MenuID == 2 && a == 20)
                        {
                            LevelSide = Convert.ToInt32(Math.Floor(decimal.Divide(Properties.Settings.Default.Level - 1,10)));
                            LevelMenu();
                            break;
                        }
                        else if (a > 1 && MenuID == 2)
                        {
                            InLevel = (a + LevelSide * 10 - 1);
                            LoadLevel();
                            break;
                        }
                    }
                }
            }
        }
        private void Screen_MouseMove(object sender, MouseEventArgs e)
        {
            if (GameTick.Enabled == false)
            {
                OverButton(false);
            }
        }
        void OverButton(bool IgnoreOldC)
        {
            Point MouseCoords = Screen.PointToClient(Cursor.Position);
            MouseCoords.X = Convert.ToInt32(decimal.Divide(MouseCoords.X, Screen.Width) * 400);
            MouseCoords.Y = Convert.ToInt32(decimal.Divide(MouseCoords.Y, Screen.Height) * 250);
            if (((OldMouseCoords.X + (Screen.Width / 250 * 2) >= MouseCoords.X && OldMouseCoords.X - (Screen.Width / 250 * 2) <= MouseCoords.X && OldMouseCoords.Y + (Screen.Height / 400 * 2) >= MouseCoords.Y && OldMouseCoords.Y - (Screen.Height / 400 * 2) <= MouseCoords.Y) == false) || IgnoreOldC)
            {
                OldMouseCoords = MouseCoords;
                if (MenuID >= 1 || MenuID <= 999)
                {
                    Graphics DrawMenu = Graphics.FromImage(ViewedBlocks);
                    for (int a = 0; a < 32; a++)
                    {
                        if (AllButtons[a] == null)
                        {
                            
                        }
                        else
                        {
                            if (MouseOn(new Rectangle(AllButtons[a].Coords.X, AllButtons[a].Coords.Y, AllButtons[a].Coords.Width, AllButtons[a].Coords.Height)) && LookingButton[a] == false)
                            {
                                LookingButton[a] = true;
                                DrawMenu.DrawImage(Buttons[AllButtons[a].LookingTexture], AllButtons[a].Coords.X, AllButtons[a].Coords.Y);
                                DrawMenu.DrawString(AllButtons[a].Text, new Font("", 12), new SolidBrush(Color.Yellow), new Rectangle(AllButtons[a].Coords.X, AllButtons[a].Coords.Y, AllButtons[a].Coords.Width, AllButtons[a].Coords.Height), CenterString);
                            }
                            else if (MouseOn(new Rectangle(AllButtons[a].Coords.X, AllButtons[a].Coords.Y, AllButtons[a].Coords.Width, AllButtons[a].Coords.Height)) == false && LookingButton[a])
                            {
                                LookingButton[a] = false;
                                DrawMenu.DrawImage(Buttons[AllButtons[a].Texture], AllButtons[a].Coords.X, AllButtons[a].Coords.Y);
                                DrawMenu.DrawString(AllButtons[a].Text, new Font("", 12), new SolidBrush(Color.Goldenrod), new Rectangle(AllButtons[a].Coords.X, AllButtons[a].Coords.Y, AllButtons[a].Coords.Width, AllButtons[a].Coords.Height), CenterString);
                            }
                        }
                    }
                    DrawMenu.Dispose();
                    Redraw();
                }
            }
        }

        bool MouseOn(Rectangle Inside)
        {
            Point MouseCoords = Screen.PointToClient(Cursor.Position);
            MouseCoords.X = Convert.ToInt32(decimal.Divide(MouseCoords.X, Screen.Width) * 400);
            MouseCoords.Y = Convert.ToInt32(decimal.Divide(MouseCoords.Y, Screen.Height) * 250);
            if (MouseCoords.X >= Inside.X && MouseCoords.X <= Inside.X + Inside.Width && MouseCoords.Y >= Inside.Y && MouseCoords.Y <= Inside.Y + Inside.Height)
            {
                return true;
            }
            return false;
        }

        //Open Menu
        void LoadMenu()
        {
            MadeLevel = false;
            MenuID = 1;
            GameTick.Enabled = false;
            ViewedBlocks = new Bitmap(Background);
            Graphics DrawMenu = Graphics.FromImage(ViewedBlocks);

            DrawMenu.DrawString(Language("Holger hals-betvinger"), new Font("", 16), new SolidBrush(Color.Gold), new Rectangle(50, 10, 300, 40), CenterString);
            DrawMenu.DrawString(Language("By: Vilder50 and Thenonamezz"), new Font("", 16), new SolidBrush(Color.Gold), new Rectangle(0, 200, 400, 50), CenterString);

            DrawMenu.Dispose();

            AllButtons = new Buttons[32];
            AllButtons[0] = new Buttons(Language("Options"), new Point(36, 85), 1);
            AllButtons[1] = new Buttons(Language("Levels"), new Point(236, 85), 1);
            DrawButtons();
            OverButton(true);
        }
        void LevelMenu()
        {
            MenuID = 2;
            ViewedBlocks = new Bitmap(Background);
            Graphics DrawMenu = Graphics.FromImage(ViewedBlocks);

            DrawMenu.DrawString(Language("Level Menu"), new Font("", 16), new SolidBrush(Color.Gold), new Rectangle(50, 10, 300, 40), CenterString);

            DrawMenu.Dispose();

            AllButtons = new Buttons[32];
            if (LevelSide == 0)
            {
                AllButtons[0] = new Buttons(Language("Back"), new Point(19, 19), 4);
            }
            else
            {
                    AllButtons[0] = new Buttons("<", new Point(19, 19), 0);
            }
            if ((LevelSide * 10) + 10 < Properties.Settings.Default.Level)
            {
                AllButtons[1] = new Buttons(">", new Point(333, 19), 0);
                AllButtons[20] = new Buttons(">>", new Point(281, 19), 0);
            }
            else
            {
                AllButtons[1] = new Buttons(Language("Back"), new Point(301, 19), 4);
            }
            for (int a = 0; a < 10; a++)
            {
                if (a + (LevelSide * 10) < Levels.Length && a + (LevelSide * 10) < Properties.Settings.Default.Level)
                {
                    AllButtons[a + 2] = new Buttons((a + 1 + (LevelSide * 10)) + "", new Point(16 + (a % 5 * 80), 80 + (a / 5 * 80)), 0);
                }
            }
            DrawButtons();
            OverButton(true);
        }
        void OptionMenu()
        {
            MenuID = 3;
            GameTick.Enabled = false;
            ViewedBlocks = new Bitmap(Background);
            Graphics DrawMenu = Graphics.FromImage(ViewedBlocks);

            DrawMenu.DrawString(Language("Options"), new Font("", 16), new SolidBrush(Color.Gold), new Rectangle(50, 10, 300, 40), CenterString);

            DrawMenu.Dispose();

            AllButtons = new Buttons[32];
            AllButtons[0] = new Buttons(Language("Language"), new Point(36, 85), 1);
            AllButtons[1] = new Buttons(Language("Controls"), new Point(236, 85), 1);
            AllButtons[2] = new Buttons(Language("Back"), new Point(19, 19), 4);
            AllButtons[3] = new Buttons(Language("Back"), new Point(301, 19), 4);
            DrawButtons();
            OverButton(true);
        }
        void NewButtonsMenu(int Button)
        {
            if (Button == 0) { ChangeButton = 0; }
            MenuID = 4;
            ViewedBlocks = new Bitmap(Background);
            Graphics DrawMenu = Graphics.FromImage(ViewedBlocks);

            DrawMenu.DrawString(Language("Controls"), new Font("", 16), new SolidBrush(Color.Gold), new Rectangle(50, 10, 300, 40), CenterString);
            DrawMenu.DrawString(Language("Button for: ") + Language(ControlsNames[Button]), new Font("", 16), new SolidBrush(Color.Gold), new Rectangle(50, 10, 300, 390), CenterString);

            DrawMenu.Dispose();

            AllButtons = new Buttons[32];
            AllButtons[0] = new Buttons(Language("Back"), new Point(19, 19), 4);
            AllButtons[1] = new Buttons(Language("Back"), new Point(301, 19), 4);
            DrawButtons();
            OverButton(true);
        }
        void LangMenu()
        {
            MenuID = 5;
            GameTick.Enabled = false;
            ViewedBlocks = new Bitmap(Background);
            Graphics DrawMenu = Graphics.FromImage(ViewedBlocks);

            DrawMenu.DrawString(Language("Language"), new Font("", 16), new SolidBrush(Color.Gold), new Rectangle(50, 10, 300, 40), CenterString);

            DrawMenu.Dispose();

            AllButtons = new Buttons[32];
            AllButtons[0] = new Buttons(Language("Back"), new Point(19, 19), 4);
            AllButtons[1] = new Buttons(Language("Back"), new Point(301, 19), 4);
            AllButtons[2] = new Buttons(Language("Dansk"), new Point(44, 110), 6);
            AllButtons[3] = new Buttons(Language("English"), new Point(244, 110), 6);
            DrawButtons();
            OverButton(true);
        }

        //Draw Menu
        void DrawButtons()
        {
            Graphics DrawMenu = Graphics.FromImage(ViewedBlocks);
            for (int a = 0; a < 32; a++)
            {
                if (AllButtons[a] != null)
                {
                    LookingButton[a] = false;
                    DrawMenu.DrawImage(Buttons[AllButtons[a].Texture], AllButtons[a].Coords.X, AllButtons[a].Coords.Y);
                    DrawMenu.DrawString(AllButtons[a].Text, new Font("", 12), new SolidBrush(Color.Goldenrod), new Rectangle(AllButtons[a].Coords.X, AllButtons[a].Coords.Y, AllButtons[a].Coords.Width, AllButtons[a].Coords.Height), CenterString);
                }
            }
            DrawMenu.Dispose();
            Redraw();
        }
        Bitmap DrawButton(int[,] BTextures)
        {
            Bitmap ButtonLook = new Bitmap(BTextures.GetLength(0) * 16, BTextures.GetLength(1) * 16);
            Graphics DrawSmallButton = Graphics.FromImage(ButtonLook);

            for (int a = 0; a < BTextures.GetLength(0); a++)
            {
                for (int b = 0; b < BTextures.GetLength(1); b++)
                {
                    DrawSmallButton.DrawImage(Textures, a * 16, b * 16, new Blocks(BTextures[a, b]).Texture, GraphicsUnit.Pixel);
                }
            }

            DrawSmallButton.Dispose();

            return ButtonLook;
        }

        //Language
        string Language(string TextName)
        {

            if (YourLanguage == "Dansk")
            {
                if (TextName == "Holger hals-betvinger") { return "Holger hals-betvinger"; }
                else if (TextName == "Levels") { return "Baner"; }
                else if (TextName == "Options") { return "Indstilinger"; }
                else if (TextName == "Level Menu") { return "Bane menu"; }
                else if (TextName == "Back") { return "Menu"; }
                else if (TextName == "Controls") { return "Styring"; }
                else if (TextName == "Language") { return "Sprog"; }
                else if (TextName == "By: Vilder50 and Thenonamezz") { return "Af: Vilder50 og Thenonamezz"; }
                else if (TextName == "Welcome to:") { return "Velkommen til:"; }
                else if (TextName == "Go through the door to complete the level!") { return "Gå igennem døren for at klare banen!"; }
                else if (TextName == "Click <R> to restart the level.") { return "Klik <R> for at genstarte banen"; }
                else if (TextName == "You can't go over yourself.") { return "Du kan ikke gå over dig selv."; }
                else if (TextName == "...So it can be hard to go back!") { return "...Så det kan være svært at gå tilbage"; }
                else if (TextName == "Click the green buttons to open the door.") { return "Klik de grønne knapper for at åbne døren."; }
                else if (TextName == "Some doors makes you win.") { return "Nogen døre gør at du vinder."; }
                else if (TextName == "Other doors block your way.") { return "Nogen døre står i vejen."; }
                else if (TextName == "These doors needs 1 plate pressed to open.") { return "Du skal klikke 1 knap for at åbne disse døre."; }
                else if (TextName == "These doors needs 2 plates pressed to open.") { return "Du skal klikke 2 knapper for at åbne disse døre."; }
                else if (TextName == "You can click <esc> to go to the menu.") { return "Du kan klikke <esc> for at gå til menuen."; }
                else if (TextName == "You can push stones.") { return "Du kan skubbe til sten."; }
                else if (TextName == "Stones can activate green plates.") { return "Sten kan aktivere grønne knapper."; }
                else if (TextName == "Only stones can activate yellow plates.") { return "Kun sten kan aktivere gule knapper"; }
                else if (TextName == "You can only go through arrows on one side...") { return "Du kan ikke gå igennem pile på den spidse side..."; }
                else if (TextName == "THE END") { return "SLUT!"; }
                else if (TextName == "Button for: ") { return "Knappen for: "; }
                else if (TextName == "Reset") { return "Genstart"; }
                else if (TextName == "Goto Menu") { return "Gå til menuen"; }
                else if (TextName == "Fullscreen") { return "Fuldskærm"; }
                else if (TextName == "Left") { return "Venstre"; }
                else if (TextName == "Right") { return "Højre"; }
                else if (TextName == "Up") { return "Op"; }
                else if (TextName == "Down") { return "Ned"; }

                return TextName;
            }
            if (YourLanguage == "English")
            {
                return TextName;
            }
            return "Noname";
        }
    }
}
