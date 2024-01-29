using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class LinkSpriteFactory
    {
        public enum LinkType
        {
            DownIdle,
            UpIdle,
            LeftIdle,
            RightIdle,
            DownAttackSword,
            LeftAttackSword,
            RightAttackSword,
            UpAttackSword,
            UpShootProjectile,
            LeftShootProjectile,
            RightShootProjectile,
            DownShootProjectile,
            DownMove,
            LeftMove,
            RightMove,
            UpMove,
            Dead,
            SwordUp,
            SwordLeft,
            SwordRight,
            SwordDown,
            Blank,
            Winning
        }
        Dictionary<LinkType, int[]> LinkSpriteDictionary = new Dictionary<LinkType, int[]>();
        private Texture2D linkSpriteSheet;

        private const int LINK_HEIGHT = 16;
        private const int LINK_WIDTH = 15;
        private const int SPACE_WIDTH = 15;
        private const int SPACE_HEIGHT = 15;

        public static LinkSpriteFactory instance = new LinkSpriteFactory();
        public static LinkSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private LinkSpriteFactory()
        {
        }

        private void fillDictionary()
        {
            LinkSpriteDictionary.Add(LinkType.DownIdle, new int[] { 0, 0, 15, 16});
            LinkSpriteDictionary.Add(LinkType.UpIdle, new int[] { 60, 0, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.LeftIdle, new int[] { 30, 0, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.RightIdle, new int[] { 90, 30, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.DownAttackSword, new int[] { 0, 60, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.LeftAttackSword, new int[] { 30, 60, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.RightAttackSword, new int[] { 90, 60, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.UpAttackSword, new int[] { 60, 60, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.SwordDown, new int[] { 0, 198, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.SwordLeft, new int[] { 30, 195, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.SwordRight, new int[] { 93, 195, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.SwordUp, new int[] { 60, 195, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.UpShootProjectile, new int[] { 60, 60, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.LeftShootProjectile, new int[] { 30, 60, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.RightShootProjectile, new int[] { 90, 60, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.DownShootProjectile, new int[] { 0, 60, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.Blank, new int[] { 360, 0, 15, 15 });

            LinkSpriteDictionary.Add(LinkType.DownMove, new int[] { 0, 30, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.LeftMove, new int[] { 30, 30, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.RightMove, new int[] { 90, 0, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.UpMove, new int[] { 60, 30, 15, 15 });

            LinkSpriteDictionary.Add(LinkType.Dead, new int[] { 30, 150, 15, 15 });
            LinkSpriteDictionary.Add(LinkType.Winning, new int[] { 31, 150, 14, 16 });

        }

        public void LoadAllTextures(ContentManager content)
        {
            fillDictionary();
            linkSpriteSheet = content.Load<Texture2D>("link");
            
        }


        public ISprite CreateLink(LinkType bT)
        {
            //System.Diagnostics.Debug.WriteLine(bT);
            int[] frames = LinkSpriteDictionary.GetValueOrDefault(bT);
            return new LinkSprite(linkSpriteSheet, frames[0], frames[1], frames[2], frames[3]);
        }

    }
}
