using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Egg_roll
{
    static class WorldGen
    {
        static int tileSize = 150;
        static int worldX = 10, worldY = 24;
        static Tile[,] world = new Tile[worldX, worldY];
        static Tile[,] world2 = new Tile[worldX, worldY];
        static Texture2D spriteSheet;
        static Texture2D tileSpriteSheet;
        //static bool gestureActive = false;
        static Color color = Color.Azure;
        static Random rand = new Random();

        static Rectangle drawRect;

        static bool first = true;

        static public void Initialize(ContentManager Content)
        {
            spriteSheet = Content.Load<Texture2D>("Sprites\\SpriteSheet");
            tileSpriteSheet = Content.Load<Texture2D>("Sprites\\tiles");
        }

        static int tempInput = 0;

        static public void Update(Rectangle drawRectangle, Vector2 playerPos)
        {
            drawRect = drawRectangle;
            if (first)
            {
                for (int i = 0; i < 1; i++)
                {
                    RandomLevel(tempInput);
                    tempInput++;
                }
                first = false;
            }
        }

        #region CreateRandomLevel

        static void RandomLevel(int temp)
        {
            Clear(2);

            for (int y = 0; y < worldY; y++)
            {
                for (int x = 0; x < worldX; x++)
                {
                    world2[x, y] = world[x, y];
                }
            }

            int ypos;
            if (temp == 0)
                ypos = 0;
            else
                ypos = GetYPos(worldX - 1, false);

            Clear(1);

            temp %= 4;
            temp *= worldX;

            bool canCreateHole = false;
            int holeInt = 0, holeWait = 3, softblock = 0, coinCount = rand.Next(4);
            //color = Color.Brown;

            for (int x = 0; x < worldX; x++)
            {
                if ((canCreateHole == true && holeInt == rand.Next(2,4)) || holeInt == 3)
                {
                    canCreateHole = false;
                    holeInt = 0;
                    holeWait = 3;
                }

                if (holeWait == 0 && x % rand.Next(5,8) == 0)
                    canCreateHole = true;

                if (x % rand.Next(2, 4) == 0)
                    ypos = SetY(ypos);

                if (!canCreateHole)
                {
                    if (x > 0 && HighDrop(x, ypos))
                    {
                        //color = Color.Green;
                        softblock = 2;
                    }
                    for (int y = ypos; y < worldY; y++)
                    {
                        world[x, y] = new Tile(tileSpriteSheet, new Rectangle(150*0, 150*0, 150, 150), new Vector2((x + temp) * tileSize, y * tileSize), Color.White, "Block");
                    }
                    if (softblock < 1)
                    {
                        //color = Color.Brown;
                    }
                    holeWait--;
                    if (holeWait < 0)
                        holeWait = 0;
                    softblock--;
                }
                else
                    holeInt++;

                if (world[x, ypos] == null)
                {
                    Platform(1, x, GetYPos(x - 1, true), temp);
                }
            }

           CalculateTerrain(temp);

           AddCoins(rand.Next(1,6), temp);

            
        }

        static public Point GetMatrixPos(Vector2 point)
        {
            return new Point((int)Math.Round(point.X / tileSize), (int)point.Y / tileSize);
        }

        static public Tile GetTileAtPosition(Point pos, Point dir)
        {
            int pointX = pos.X + dir.X;
            int pointY = pos.Y + dir.Y;
            if(pointX >= 0 && pointY >= 0 && pointX < worldX && pointY < worldY)
                return world[pointX, pointY];
            return null;
        }

        static void AddCoins(int coinCount, int temp)
        {
            while (coinCount > 0)
            {
                int x = rand.Next(0, worldX);
                int y = GetYPos(x, false);
                y--;
                if (world[x, y] == null)
                {
                    world[x, y] = new Tile(spriteSheet, new Rectangle(150* 11, 150*8, 150,150), new Vector2((x+temp) * tileSize, y * tileSize), Color.White, "Coin");
                    coinCount--;
                }
            }
        }

        static bool HighDrop(int x, int y)
        {
            if (y - GetYPos(x - 1, false) > 2)
                return true;
            return false;

        }

        static int GetYPos(int x, bool platformCall)
        {
            for (int y = 0; y < worldY; y++)
            {
                if (world[x, y] != null && world[x, y].Name != "Coin")
                {
                    if (world[x, y].TileColor == Color.White || !platformCall)
                        return y;
                    else
                        return y - 2;
                }
            }
            return 0;
        }

        static int GetYPos2(int x, bool platformCall)
        {
            for (int y = 0; y < worldY; y++)
            {
                if (world2[x, y] != null && world2[x, y].Name != "Coin")
                {
                    if (world2[x, y].TileColor == Color.White || !platformCall)
                        return y;
                    else
                        return y - 2;
                }
            }
            return 0;
        }

        static void Platform(int length, int xpos, int ypos, int temp)
        {
            for (int x = xpos; x < xpos + length; x++)
            {
                if (x < worldX && x > 0 && ypos < worldY && ypos > 0)
                    world[x, ypos] = new Tile(spriteSheet, new Rectangle(0, 150*8, 150,150), new Vector2((x+temp) * tileSize, ypos * tileSize), Color.White, "Platform");
            }

            //if (rand.Next(5 * chance) == 1)
            //    Platform(rand.Next(2, 5), xpos + length, ypos - 2, chance * 2, color);
        }

        static void CalculateTerrain(int temp)
        {
            int lowX = 0;
            if (temp == 0)
                lowX = 1;
            for (int x = lowX; x < worldX-1; x++)
            {
                int y;
                Tile last;
                if (x != 0)
                    last = world[x - 1, GetYPos(x - 1, false)];
                else
                    last = world2[worldX-1, GetYPos2(worldX-1, false)];
                Tile present = world[x, y = GetYPos(x, false)];
                Tile next = world[x + 1, GetYPos(x + 1, false)];

                if (present.Name != "Platform")
                {
                    if ((last.PosY == present.PosY || last.Name == "SlopeDown") && next.PosY > present.PosY)
                    {
                        world[x, y] = new Tile(tileSpriteSheet, new Rectangle(150*0, 150* 1, 150, 150), present.Position, Color.White, "SlopeDown");
                    }
                    else if ((next.PosY == present.PosY || next.Name == "SlopeUp") && last.PosY > present.PosY)
                    {
                        world[x, y] = new Tile(tileSpriteSheet, new Rectangle(150*0, 150*2, 150,150), present.Position, Color.White, "SlopeUp");
                    }

                    if (present.Name == "Block" && last.PosY > present.PosY && next.PosY > present.PosY)
                    {
                        world[x, y].TileColor = Color.Black;
                    }
                }
            }
        }

        static bool BlockAtPosXNull(int x)
        {
            for (int y = 0; y < 24; y++)
            {
                if (world[x, y] != null)
                    return false;
            }
            return true;
        }

        static int SetY(int y)
        {
            y = rand.Next(y - 1, y + 2);
            if (y < 7)
                y = 7;
            else if (y > worldY - 4)
                y = worldY - 4;
            return y;
        }

        static void Clear(int i)
        {
            for (int y = 0; y < worldY; y++)
            {
                for (int x = 0; x < worldX; x++)
                {
                    if (i == 1)
                    {
                        if (world[x, y] != null)
                            world[x, y] = null;
                    }
                    else if (i == 2)
                    {
                        if (world2[x, y] != null)
                            world2[x, y] = null;
                    }
                }
            }
        }

        #endregion

        static public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < worldY; y++)
            {
                for (int x = 0; x < worldX; x++)
                {
                    if (world[x, y] != null && drawRect.Contains((int)world[x,y].Position.X, (int)world[x,y].Position.Y))
                        world[x, y].Draw(spriteBatch);
                    if (world2[x, y] != null && drawRect.Contains((int)world2[x, y].Position.X, (int)world2[x, y].Position.Y))
                        world2[x, y].Draw(spriteBatch);
                }
            }

        }
    }
}
