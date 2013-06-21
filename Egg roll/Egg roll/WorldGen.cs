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
        static int tileSize = 200;
        static int worldX = 20, worldY = 24, update = (worldX / 4) * 3, methodCalls = 0, barnChunks = 0;
        static bool barn = false, isBarn = false, tempActive = false;
        static float tempPos, tempPosStop;
        static Sprite[,] world = new Sprite[worldX, worldY];
        static Texture2D spriteSheet, tileSpriteSheet, poop, pig, tree1, tree2;
        static Random rand = new Random();
        static Rectangle drawRect;

        static public void Initialize(ContentManager Content)
        {
            spriteSheet = Content.Load<Texture2D>("Sprites\\SpriteSheet");
            tileSpriteSheet = Content.Load<Texture2D>("Sprites\\tiles");
            poop = Content.Load<Texture2D>("Sprites\\poop");
            pig = Content.Load<Texture2D>("Sprites\\pig");
            tree1 = Content.Load<Texture2D>("Sprites\\tree1");
            tree2 = Content.Load<Texture2D>("Sprites\\tree2");
        }

        static public void Update(Rectangle drawRectangle, Vector2 playerPos, ref int coin)
        {
            drawRect = drawRectangle;
            if (methodCalls == 0)
            {
                RandomLevel(methodCalls, ref coin);
                methodCalls++;
            }
            if (playerPos.X > world[update, GetYPos(update, false)].PosX)
            {
                RandomLevel(methodCalls, ref coin);
                methodCalls++;
            }
            if (tempActive && playerPos.X > tempPos || playerPos.X < tempPosStop)
                isBarn = true;
            else
                isBarn = false;

        }

        public static void ResetWorld()
        {
            Clear(0, worldX);
            methodCalls = 0;
            barn = false;
            isBarn = false;
            tempActive = false;
        }

        #region CreateRandomLevel

        static void RandomLevel(int methodCalls, ref int coin)
        {
            int LowX, ypos;

            if (!barn && methodCalls % rand.Next(1, 5) == 3)
            {
                barn = true;
                barnChunks = 5;
            }

            if (methodCalls == 0)
            {
                LowX = 0;
                ypos = rand.Next(worldY / 2 - 2, worldY / 2 + 2);
            }
            else
            {
                LowX = worldX / 2;
                for (int y = 0; y < worldY; y++)
                {
                    for (int x = 0; x < LowX; x++)
                    {
                        if (world[x, y] != null && world[x, y].Name == "Coin")
                            coin = 1;
                        world[x, y] = world[x + LowX, y];
                        world[x + LowX, y] = null;
                    }
                }
                ypos = GetYPos(LowX - 1, false);
            }
            
            methodCalls *= (worldX / 2);

            int coinCount = rand.Next(4);
            if (methodCalls == 0)
                StartChunk(ypos);
            else if (barn && barnChunks == 5)
                BarnEntranceChunk(LowX, worldX, ypos, methodCalls);
            else if (barn && barnChunks == 1)
                BarnExitChunk(LowX, worldX, ypos, methodCalls);
            else if (barn)
                RandomBarnChunk(LowX, worldX, ypos, rand.Next(2, 5), methodCalls);
            else
                RandomGrassChunk(LowX, worldX, ypos, rand.Next(3, 7), methodCalls);

            UpdatePlatforms(LowX);
            if (methodCalls > 0)
            {
                AlterTerrain(LowX, methodCalls);
                AddCoins(LowX, methodCalls);
            }

            if (barn)
            {
                barnChunks--;
                if (barnChunks == 0)
                    barn = false;
            }
        }

        #region Chunks

        static void StartChunk(int ypos)
        {
            int yy = ypos;
            Rectangle block = new Rectangle(250 * 0, 250 * 0, 250, 250);
            for (int x = 0; x < worldX; x++)
            {
                if (x < 5)
                    ypos = 0;
                else
                    ypos = yy;
                for (int y = ypos; y < worldY; y++)
                {
                    world[x, y] = new Sprite(tileSpriteSheet, block, new Vector2(x * tileSize, y * tileSize), "Block");
                }
            }
        }

        static void RandomGrassChunk(int lowX, int highX, int ypos, int holeSize, int methodCalls)
        {
            Rectangle block = new Rectangle(250 * 0, 250 * 0, 250, 250);
            Rectangle platform = new Rectangle(250 * 0, 250 * 3, 250, 250);
            string name = "Block";
            int softblock = 0, hole = rand.Next(lowX, worldX - holeSize), yposWait = rand.Next(2, 4);
            for (int x = lowX; x < highX; x++)
            {
                if (x > lowX + 1 && x % yposWait == 1)
                    ypos = SetY(ypos);

                if (x > 0 && HighDrop(x, ypos))
                {
                    name = "SoftBlock";
                    softblock = 2;
                }

                if (holeSize == 0 || (x > hole && x < hole + holeSize))
                {
                    Platform(1, x, GetYPos(x - 1, true), methodCalls, new Rectangle(0, 250 * 3, 250, 250));
                }
                else
                {
                    for (int y = ypos; y < worldY; y++)
                    {
                        world[x, y] = new Sprite(tileSpriteSheet, block, new Vector2((x + methodCalls) * tileSize, y * tileSize), name);
                    }
                }
                if (softblock < 1)
                    name = "Block";

                softblock--;
            }
            AddBottom(lowX, methodCalls, block);
        }

        static void RandomBarnChunk(int lowX, int highX, int ypos, int holeSize, int methodCalls)
        {
            Rectangle block = new Rectangle(250 * 1, 250 * 0, 250, 250);
            Rectangle platform = new Rectangle(250 * 1, 250 * 3, 250, 250);


            string name = "Block";
            int softblock = 0, hole = rand.Next(lowX, worldX - holeSize), yposWait = rand.Next(2, 4);
            for (int x = lowX; x < highX; x++)
            {
                if (x > lowX + 1 && x % yposWait == 1)
                    ypos = SetY(ypos);

                if (x > 0 && HighDrop(x, ypos))
                {
                    name = "SoftBlock";
                    softblock = 2;
                }

                if (holeSize == 0 || (x > hole && x < hole + holeSize))
                {
                    Platform(1, x, GetYPos(x - 1, true), methodCalls, new Rectangle(250, 250 * 3, 250, 250));
                }
                else
                {
                    for (int y = ypos; y < worldY; y++)
                    {
                        world[x, y] = new Sprite(tileSpriteSheet, block, new Vector2((x + methodCalls) * tileSize, y * tileSize), name);
                    }
                }

                if (softblock < 1)
                    name = "Block";

                softblock--;
            }
            AddBottom(lowX, methodCalls, block);
        }

        static void BarnEntranceChunk(int lowX, int highX, int ypos, int methodCalls)
        {
            Rectangle block = new Rectangle(250 * 1, 250 * 0, 250, 250);
            RandomGrassChunk(lowX, highX - 3, ypos, 1, methodCalls);
            ypos = GetYPos(highX - 4, false);
            world[highX - 4, ypos].Name = "SoftBlock";
            for (int x = highX - 3; x < highX; x++)
            {
                for (int y = ypos; y < worldY; y++)
                {
                    world[x, y] = new Sprite(tileSpriteSheet, block, new Vector2((x + methodCalls) * tileSize, y * tileSize), "SoftBlock");
                }

                if (x != highX - 1)
                {
                    for (int y = ypos - 3; y > 0; y--)
                    {
                        world[x, y] = new Sprite(tileSpriteSheet, block, new Vector2((x + methodCalls) * tileSize, y * tileSize), "BarnBlock");
                    }
                }
            }
            tempPos = world[highX - 3, GetYPos(highX - 3, false)].PosX;
            tempActive = true;
        }

        static void BarnExitChunk(int lowX, int highX, int ypos, int methodCalls)
        {
            Rectangle block = new Rectangle(250 * 1, 250 * 0, 250, 250);
            for (int x = lowX; x < lowX + 3; x++)
            {
                for (int y = ypos; y < worldY; y++)
                {
                    world[x, y] = new Sprite(tileSpriteSheet, block, new Vector2((x + methodCalls) * tileSize, y * tileSize), "SoftBlock");
                }

                if (x > lowX)
                {
                    for (int y = ypos - 3; y > 0; y--)
                    {
                        world[x, y] = new Sprite(tileSpriteSheet, block, new Vector2((x + methodCalls) * tileSize, y * tileSize), "BarnBlock");
                    }
                }
            }
            tempPosStop = world[lowX + 2, GetYPos(lowX + 2, false)].PosX;
            tempActive = false;

            for (int y = ypos; y < worldY; y++)
            {
                world[lowX + 3, y] = new Sprite(tileSpriteSheet, new Rectangle(0, 0, 250, 250), new Vector2((lowX + 3 + methodCalls) * tileSize, y * tileSize), "SoftBlock");
            }
            RandomGrassChunk(lowX + 4, highX, ypos, 1, methodCalls);
        }

        #endregion

        static void AddBottom(int lowX, int methodCalls, Rectangle block)
        {
            for (int x = lowX; x < worldX; x++)
            {
                for (int y = worldY - 3; y < worldY; y++)
                {
                    if (world[x, y] == null)
                        world[x, y] = new Sprite(tileSpriteSheet, block, new Vector2((x + methodCalls) * tileSize, y * tileSize), "Block");
                }
            }
        }

        static void UpdatePlatforms(int lowX)
        {
            for (int x = lowX; x < worldX; x++)
            {
                if (GetTileX(x).Name == "Platform" && rand.Next(3) == 0 && world[x, GetYPos(x, false) - 2] == null)
                    world[x, GetYPos(x, false)] = null;
            }
        }

        static void AddCoins(int low, int methodCalls)
        {
            int y;
            for (int x = low; x < worldX; x++)
            {
                if (world[x, y = GetPlatformYPos(x)] != null)
                {
                    world[x, y - 1] = new Sprite(spriteSheet, new Rectangle(150 * 11, 150 * 8, 150, 150), new Vector2((x + methodCalls) * tileSize, (y - 1) * tileSize), "Coin");
                }
            }
        }

        static bool HighDrop(int x, int y)
        {
            if (y - GetYPos(x - 1, false) > 2)
                return true;
            return false;

        }

        public static Vector2 StartPos
        {
            get { return world[6, GetYPos(6, false)].Position - new Vector2(0, 200); }
        }

        static bool FiftyFifty
        {
            get
            {
                if (rand.Next(2) == 0)
                    return true;
                return false;
            }
        }

        static int GetYPos(int x, bool platformCall)
        {
            for (int y = 0; y < worldY; y++)
            {
                if (world[x, y] != null && world[x, y].Name != "Coin" && world[x, y].Name != "BarnBlock" && world[x,y].Name != "Tree" && world[x,y].Name != "Hay")
                {
                    if (world[x, y].Name == "Platform" || !platformCall)
                        return y;
                    else
                        return y - 1;
                }
            }
            return 0;
        }

        static int GetPlatformYPos(int x)
        {
            for (int y = 0; y < worldY; y++)
            {
                if (world[x, y] != null && world[x, y].Name == "Platform")
                    return y;
            }
            return 0;
        }


        static void Platform(int length, int xpos, int ypos, int methodCalls, Rectangle platform)
        {
            for (int x = xpos; x < xpos + length; x++)
            {
                if (x < worldX && x > 0 && ypos < worldY && ypos > 0)
                    world[x, ypos] = new Sprite(tileSpriteSheet, platform, new Vector2((x + methodCalls) * tileSize, ypos * tileSize), "Platform");
            }
        }

        static void AlterTerrain(int low, int methodCalls)
        {
            //BarnSlopeUp: new Rectangle(250 * 1, 250 * 2, 250, 250)
            //BarnSlopeDown = new Rectangle(250 * 1, 250 * 1, 250, 250)

            //GrassSlopeUp = new Rectangle(250 * 0, 250 * 2, 250, 250)
            //GrassSlopeDown = new Rectangle(250 * 0, 250 * 1, 250, 250)

            for (int x = low - 1; x < worldX - 1; x++)
            {
                int y = GetYPos(x, false);
                Sprite last = GetTileX(x - 1);
                Sprite present = GetTileX(x);
                Sprite next = GetTileX(x + 1);
                Sprite up;

                if (present.Name != "Platform")
                {
                    if (last.PosY == present.PosY && next.PosY == present.PosY && world[x, y - 1] == null && !barn && last.Name != "SlopeUp" && next.Name != "SlopeDown" && world[x - 1, y - 3] == null && (world[x, y - 3] == null || world[x, y - 3].PosY < 3000) && world[x + 1, y - 3] == null)
                    {
                        if (rand.Next(2) == 0)
                            world[x, y - 3] = new Sprite(tree1, tree1.Bounds, present.Position - new Vector2(tileSize, tileSize * 4.4f), "Hay");
                        else
                            world[x, y - 3] = new Sprite(tree2, tree1.Bounds, present.Position - new Vector2(tileSize, tileSize * 4.4f), "Hay");
                        world[x, y - 3].Scale *= 2.15f;
                    }

                    if (present.Name == "SoftBlock")
                    {
                        world[x, y - 1] = new Sprite(tileSpriteSheet, new Rectangle(250 * 0, 250 * 4, 250, 250), present.Position - new Vector2(0, tileSize), "Hay");
                    }

                    up = world[x, y - 1];
                    if ((x - 2) > 0 && GetTileX(x - 2).PosY == present.PosY && (x + 2) < worldX && GetTileX(x + 2).PosY == present.PosY && last.PosY == present.PosY && present.PosY == next.PosY && (up == null || up.Name != "Hay"))
                    {
                        if (rand.Next(2) == 0)
                        {
                            world[x, y - 1] = new Sprite(poop, poop.Bounds, present.Position - new Vector2(-(0.2f * tileSize), tileSize * 0.8f), "Poop");
                            world[x, y - 1].Scale *= 0.8f;
                        }
                        else
                            world[x, y - 1] = new Sprite(pig, pig.Bounds, present.Position - new Vector2(0, tileSize), "Pig");
                        if (rand.Next(10) == 5)
                        {
                            Rectangle rect;
                            if (present.Source.Left == 0)
                                rect = new Rectangle(250 * 0, 250 * 3, 250, 250);
                            else
                                rect = new Rectangle(250 * 1, 250 * 3, 250, 250);
                            Platform(3, x, y - 1, methodCalls, rect);
                        }
                    }

                    if ((last.PosY == present.PosY) && next.PosY > present.PosY && (up == null || up.Name != "Hay"))
                    {
                        if (world[x, y].Source.Left == 0)
                            world[x, y] = new Sprite(tileSpriteSheet, new Rectangle(250 * 0, 250 * 1, 250, 250), present.Position, "SlopeDown");
                        else
                            world[x, y] = new Sprite(tileSpriteSheet, new Rectangle(250 * 1, 250 * 1, 250, 250), present.Position, "SlopeDown");
                    }
                    else if ((next.PosY == present.PosY) && last.PosY > present.PosY && (up == null || up.Name != "Hay"))
                    {
                        if (world[x, y].Source.Left == 0)
                            world[x, y] = new Sprite(tileSpriteSheet, new Rectangle(250 * 0, 250 * 2, 250, 250), present.Position, "SlopeUp");
                        else
                            world[x, y] = new Sprite(tileSpriteSheet, new Rectangle(250 * 1, 250 * 2, 250, 250), present.Position, "SlopeUp");
                    }
                }
            }
        }

        static Sprite GetTileX(int x)
        {
            return world[x, GetYPos(x, false)];
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
            int temp = 6;
            y = rand.Next(y - 1, y + 2);
            if (y < temp)
                y = temp;
            else if (y > worldY - temp)
                y = worldY - temp;
            return y;
        }

        static void Clear(int lowX, int highX)
        {
            for (int y = 0; y < worldY; y++)
            {
                for (int x = lowX; x < highX; x++)
                {
                    if (world[x, y] != null)
                        world[x, y] = null;
                }
            }
        }

        #endregion

        static public Point GetMatrixPos(Vector2 point)
        {
            return new Point((int)point.X / tileSize, (int)point.Y / tileSize);
        }

        static public Sprite GetTileAtPosition(Point pos, Point dir)
        {
            int pointX = pos.X + dir.X;
            int pointY = pos.Y + dir.Y;
            if (pointX >= 0 && pointY >= 0 && pointY < worldY)
                if (methodCalls <= 1)
                    return world[pointX, pointY];
                else
                    return world[(pointX + (worldX / 2) * (methodCalls - 1)) % worldX, pointY];
            return null;
        }

        static public bool IsBarn
        {
            get { return isBarn; }
        }

        static public void DeleteOnPos(Sprite tile)
        {
            for (int x = 0; x < worldX; x++)
            {
                for (int y = 0; y < worldY; y++)
                {
                    if (world[x, y] == tile)
                        world[x, y] = null;
                }
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < worldY; y++)
            {
                for (int x = 0; x < worldX; x++)
                {
                    if (world[x, y] != null && (drawRect.Contains((int)world[x, y].Position.X, (int)world[x, y].Position.Y) || world[x,y].Name == "Hay"))
                        world[x, y].Drawzzz(spriteBatch);
                }
            }
        }
    }
}