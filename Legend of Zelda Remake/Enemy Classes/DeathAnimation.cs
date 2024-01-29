using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Enemy_Classes
{
    public class DeathAnimation 
    {
        public static Texture2D deathSpriteSheet;
        public enum DeathFrame { Enemy1, Enemy2, Enemy3, Enemy4, Bomb1, Bomb2, Bomb3, Projectile }
        public enum DeathType { Enemy, Bomb, Projectile, None }
        private DeathType typeOfDeath;
        private DeathFrame currentFrame;

        private Dictionary<DeathType, DeathFrame[]> TypeToFrameList = new()
        {
            { DeathType.Enemy, new DeathFrame[]{ DeathFrame.Enemy1, DeathFrame.Enemy2, DeathFrame.Enemy3, DeathFrame.Enemy4 } },
            { DeathType.Bomb, new DeathFrame[]{ DeathFrame.Bomb1, DeathFrame.Bomb2, DeathFrame.Bomb3 } },
            { DeathType.Projectile, new DeathFrame[]{ DeathFrame.Projectile } },
        };

        private Dictionary<DeathFrame, Rectangle> FrameDictionary = new() 
        {
            { DeathFrame.Enemy1, new Rectangle(1, 45, 15, 16) },
            { DeathFrame.Enemy2, new Rectangle(17, 45, 15, 16) },
            { DeathFrame.Enemy3, new Rectangle(33, 45, 15, 16) },
            { DeathFrame.Enemy4, new Rectangle(49, 45, 15, 16) },
            { DeathFrame.Bomb1, new Rectangle(22, 2, 16, 16) },
            { DeathFrame.Bomb2, new Rectangle(44, 2, 16, 16) },
            { DeathFrame.Bomb3, new Rectangle(44, 22, 16, 16) },
            { DeathFrame.Projectile, new Rectangle(0, 2, 16, 16) }
        }; //Has to be filled

        private double createdTime;
        public double CreatedTime 
        {
            get { return createdTime; }
        }

        private Rectangle posRectangle;

        public DeathAnimation(DeathType type, Rectangle pRec, double callTime) 
        {
            typeOfDeath = type;
            createdTime = callTime;
            posRectangle = pRec;
        }
        public void Update(GameTime gameTime) 
        {
            if (typeOfDeath == DeathType.Enemy)
            {
                if (gameTime.TotalGameTime.TotalSeconds - createdTime <= GlobalUtilities.ENEMY_DEATH_ANIMATION_SPEED / 4.0)
                {
                    currentFrame = TypeToFrameList[typeOfDeath][0];
                }
                else if (gameTime.TotalGameTime.TotalSeconds - createdTime <= GlobalUtilities.ENEMY_DEATH_ANIMATION_SPEED / 2.0)
                {
                    currentFrame = TypeToFrameList[typeOfDeath][1];
                }
                else if (gameTime.TotalGameTime.TotalSeconds - createdTime <= (GlobalUtilities.ENEMY_DEATH_ANIMATION_SPEED * 3) / 4.0)
                {
                    currentFrame = TypeToFrameList[typeOfDeath][2];
                }
                else
                {
                    currentFrame = TypeToFrameList[typeOfDeath][3];
                }
            }
            else if(typeOfDeath == DeathType.Bomb) 
            {
                if (gameTime.TotalGameTime.TotalSeconds - createdTime <= GlobalUtilities.ENEMY_DEATH_ANIMATION_SPEED / 3.0)
                {
                    currentFrame = TypeToFrameList[typeOfDeath][0];
                }
                else if (gameTime.TotalGameTime.TotalSeconds - createdTime <= (GlobalUtilities.ENEMY_DEATH_ANIMATION_SPEED * 2.0) / 3.0)
                {
                    currentFrame = TypeToFrameList[typeOfDeath][1];
                }
                else
                {
                    currentFrame = TypeToFrameList[typeOfDeath][2];
                }
            }
            else if(typeOfDeath == DeathType.Projectile) 
            {
                currentFrame = TypeToFrameList[typeOfDeath][0];
            }
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            if (typeOfDeath != DeathType.None)
            {
                if (!(currentFrame == DeathFrame.Enemy1 && typeOfDeath != DeathType.Enemy))
                {
                    spriteBatch.Draw(deathSpriteSheet, posRectangle, FrameDictionary[currentFrame], Color.White);
                }
            }
        }
        public static void LoadContent(ContentManager content) 
        {
            deathSpriteSheet = content.Load<Texture2D>("deathanimations");
        }
    }
}
