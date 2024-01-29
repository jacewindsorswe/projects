using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class BackgroundSpriteFactory
    {
        public enum RoomType
        {
            Room00,
            Room10,
            Room20,
            Room21,
            Room41,
            Room51,
            Room02,
            Room12,
            Room22,
            Room32,
            Room42,
            Room13,
            Room23,
            Room33,
            Room24,
            Room15,
            Room25,
            Room35,
            Room26,
            Room210,
            Room220,
            Room221,
            Room231,
            Room222,
            Room232,
            Room223,
            Room233,
            Room224,
            Room234,
            Room225,
            Room235,
            Room206,
            Room216,
            Room226,
            Room236,
            Room217,
            Room227
        }
        Dictionary<RoomType, int[]> RoomDictionary = new Dictionary<RoomType, int[]>();
        private Texture2D d1SpriteSheet, d2SpriteSheet;

        private const int ROOM_HEIGHT = 16;
        private const int ROOM_WIDTH = 15;

        public static BackgroundSpriteFactory instance = new BackgroundSpriteFactory();
        public static BackgroundSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private BackgroundSpriteFactory()
        {
        }

        private void fillDictionary()
        {
            RoomDictionary.Add(RoomType.Room00, new int[] { 1, 1, 256, 160 });
            RoomDictionary.Add(RoomType.Room10, new int[] { 258, 1, 256, 176 });
            RoomDictionary.Add(RoomType.Room20, new int[] { 515, 1, 256, 176 });
            RoomDictionary.Add(RoomType.Room21, new int[] { 515, 178, 256, 176 });
            RoomDictionary.Add(RoomType.Room41, new int[] { 1029, 178, 256, 176 });
            RoomDictionary.Add(RoomType.Room51, new int[] { 1286, 178, 256, 176 });
            RoomDictionary.Add(RoomType.Room02, new int[] { 1, 355, 256, 176 });
            RoomDictionary.Add(RoomType.Room12, new int[] { 258, 355, 256, 176 });
            RoomDictionary.Add(RoomType.Room22, new int[] { 515, 355, 256, 176 });
            RoomDictionary.Add(RoomType.Room32, new int[] { 772, 355, 256, 176 });
            RoomDictionary.Add(RoomType.Room42, new int[] { 1029, 355, 256, 176 });
            RoomDictionary.Add(RoomType.Room13, new int[] { 258, 532, 256, 176 });
            RoomDictionary.Add(RoomType.Room23, new int[] { 515, 532, 256, 176 });
            RoomDictionary.Add(RoomType.Room33, new int[] { 772, 532, 256, 176 });
            RoomDictionary.Add(RoomType.Room24, new int[] { 515, 709, 256, 176 });
            RoomDictionary.Add(RoomType.Room15, new int[] { 258, 886, 256, 176 });
            RoomDictionary.Add(RoomType.Room25, new int[] { 515, 886, 256, 176 });
            RoomDictionary.Add(RoomType.Room35, new int[] { 772, 886, 256, 176 });
            RoomDictionary.Add(RoomType.Room26, new int[] { 258, 886, 256, 176 });
            RoomDictionary.Add(RoomType.Room210, new int[] { 258, 1, 256, 176 });
            RoomDictionary.Add(RoomType.Room220, new int[] { 515, 1, 256, 176 });
            RoomDictionary.Add(RoomType.Room221, new int[] { 515, 178, 256, 176 });
            RoomDictionary.Add(RoomType.Room231, new int[] { 772, 178, 256, 176 });
            RoomDictionary.Add(RoomType.Room222, new int[] { 515, 355, 256, 176 });
            RoomDictionary.Add(RoomType.Room232, new int[] { 772, 355, 256, 176 });
            RoomDictionary.Add(RoomType.Room223, new int[] { 515, 532, 256, 176 });
            RoomDictionary.Add(RoomType.Room233, new int[] { 772, 532, 256, 176 });
            RoomDictionary.Add(RoomType.Room224, new int[] { 515, 709, 256, 176 });
            RoomDictionary.Add(RoomType.Room234, new int[] { 772, 709, 256, 176 });
            RoomDictionary.Add(RoomType.Room225, new int[] { 515, 886, 256, 176 });
            RoomDictionary.Add(RoomType.Room235, new int[] { 772, 886, 256, 176 });
            RoomDictionary.Add(RoomType.Room206, new int[] { 1, 1063, 256, 176 });
            RoomDictionary.Add(RoomType.Room216, new int[] { 258, 1063, 256, 176 });
            RoomDictionary.Add(RoomType.Room226, new int[] { 515, 1063, 256, 176 });
            RoomDictionary.Add(RoomType.Room236, new int[] { 772, 1063, 256, 176 });
            RoomDictionary.Add(RoomType.Room217, new int[] { 258, 1240, 256, 176 });
            RoomDictionary.Add(RoomType.Room227, new int[] { 515, 1240, 256, 176 });
        }

        public void LoadAllTextures(ContentManager content)
        {
            fillDictionary();
            d1SpriteSheet = content.Load<Texture2D>("LevelSprites");
            d2SpriteSheet = content.Load<Texture2D>("dungeon2spritesheet");
        }


        public ISprite CreateRoom(RoomType bT)
        {
            int[] frames = RoomDictionary.GetValueOrDefault(bT);
            if (MainMenu.Selection == 1)
            {
                return new BackgroundSprite(d1SpriteSheet, frames[0], frames[1], frames[2], frames[3]);
            }
            //System.Diagnostics.Debug.WriteLine("OUTSIDE");
            return new BackgroundSprite(d2SpriteSheet, frames[0], frames[1], frames[2], frames[3]);
        }

    }
}
