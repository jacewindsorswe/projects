using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint2;

namespace Sprint0
{
    public class EnemySpriteFactory
    {
        public enum EnemyFrame
        {
            Stalfos1,
            Stalfos2,
            DarknutForward,
            DarknutBackward,
            DarknutRight1,
            DarknutRight2,
            DarknutLeft1,
            DarknutLeft2,
            WizzrobeUp1,
            WizzrobeUp2,
            WizzrobeLeft1,
            WizzrobeLeft2,
            WizzrobeRight1,
            WizzrobeRight2,
            GoriyaForward,
            GoriyaBackward,
            GoriyaLeft1,
            GoriyaLeft2,
            GoriyaRight1,
            GoriyaRight2,
            BlueGoriyaForward,
            BlueGoriyaBackward,
            BlueGoriyaLeft1,
            BlueGoriyaLeft2,
            BlueGoriyaRight1,
            BlueGoriyaRight2,
            Aquamentus1,
            Aquamentus2,
            Keese1,
            Keese2,
            Gel1,
            Gel2,
            BlueGel1,
            BlueGel2,
            Wallmaster1,
            Wallmaster2,
            Blade,
            Rope1,
            Rope2,
            Rope3,
            Rope4,
            Death1,
            Death2,
            Death3,
            Death4,
            DodongoDown1,
            DodongoDown2,
            DodongoDownBomb,
            DodongoUp1,
            DodongoUp2,
            DodongoUpBomb,
            DodongoRight1,
            DodongoRight2,
            DodongoRightBomb,
            DodongoLeft1,
            DodongoLeft2,
            DodongoLeftBomb,
            MoldormSection
        }

        public static Dictionary<EnemyFrame, int[]> enemyFrameDictionary = new Dictionary<EnemyFrame, int[]>()
        {
            { EnemyFrame.Stalfos1, new int[] { 19, 11, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.Stalfos2, new int[] { 180, 11, ENEMY_WIDTH, ENEMY_HEIGHT } },

            { EnemyFrame.DarknutForward, new int[] { 341, 15, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.DarknutBackward, new int[] { 477, 15, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.DarknutRight1, new int[] { 615, 15, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.DarknutRight2, new int[] { 754, 15, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.DarknutLeft1, new int[] { 1057, 10, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.DarknutLeft2, new int[] { 920, 10, ENEMY_WIDTH, ENEMY_HEIGHT } },

            { EnemyFrame.GoriyaForward, new int[] { 20, 352, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.GoriyaBackward, new int[] { 162, 352, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.GoriyaRight1, new int[] { 306, 352, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.GoriyaRight2, new int[] { 448, 352, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.GoriyaLeft1, new int[] { 1024, 352, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.GoriyaLeft2, new int[] { 884, 352, ENEMY_WIDTH, ENEMY_HEIGHT } },

            { EnemyFrame.BlueGoriyaForward, new int[] { 671, 84, 39, 48 } },
            { EnemyFrame.BlueGoriyaBackward, new int[] { 722, 84, 39, 48 } },
            { EnemyFrame.BlueGoriyaRight1, new int[] { 770, 84, 39, 48 } },
            { EnemyFrame.BlueGoriyaRight2, new int[] { 824, 87, 42, 45 } },
            { EnemyFrame.BlueGoriyaLeft1, new int[] { 874, 84, 39, 48 } },
            { EnemyFrame.BlueGoriyaLeft2, new int[] { 923, 87, 42, 45 } },

            { EnemyFrame.WizzrobeUp1, new int[] { 295, 585, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.WizzrobeUp2, new int[] { 433, 592, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.WizzrobeRight1, new int[] { 21, 585, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.WizzrobeRight2, new int[] { 163, 591, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.WizzrobeLeft1, new int[] { 658, 593, ENEMY_WIDTH, ENEMY_HEIGHT } },
            { EnemyFrame.WizzrobeLeft2, new int[] { 803, 586, ENEMY_WIDTH, ENEMY_HEIGHT } },

            { EnemyFrame.Aquamentus1, new int[] { 1, 11, GlobalUtilities.DEFAULT_BOSS_SIZE_X, GlobalUtilities.DEFAULT_BOSS_SIZE_Y } },
            { EnemyFrame.Aquamentus2, new int[] { 26, 11, GlobalUtilities.DEFAULT_BOSS_SIZE_X, GlobalUtilities.DEFAULT_BOSS_SIZE_Y } },

            { EnemyFrame.Keese1, new int[] { 548, 45, 48, 24 } },
            { EnemyFrame.Keese2, new int[] { 608, 45, 30, 30 } },

            { EnemyFrame.Gel1, new int[] { 2, 48, 24, 24 } },
            { EnemyFrame.Gel2, new int[] { 32, 45, 18, 27 } },

            { EnemyFrame.BlueGel1, new int[] { 56, 48, 24, 24 } },
            { EnemyFrame.BlueGel2, new int[] { 86, 45, 18, 27 } },

            { EnemyFrame.Wallmaster1, new int[] { 1178, 33, 48, 48 } },
            { EnemyFrame.Wallmaster2, new int[] { 1229, 36, 42, 45 } },

            { EnemyFrame.Rope1, new int[] { 380, 177, 42, 45 } },
            { EnemyFrame.Rope2, new int[] { 431, 180, 45, 42 } },
            { EnemyFrame.Rope3, new int[] { 608, 179, 42, 45 } },
            { EnemyFrame.Rope4, new int[] { 549, 181, 45, 42 } },

            { EnemyFrame.Blade, new int[] { 491, 177, 48, 48 } },

            { EnemyFrame.MoldormSection, new int[] { 746, 186, 24, 30 } },

            { EnemyFrame.Death1, new int[] { 0, 0, GlobalUtilities.DEFAULT_DEATH_SIZE, GlobalUtilities.DEFAULT_DEATH_SIZE } },
            { EnemyFrame.Death2, new int[] { 16, 0, GlobalUtilities.DEFAULT_DEATH_SIZE, GlobalUtilities.DEFAULT_DEATH_SIZE } },
            { EnemyFrame.Death3, new int[] { 32, 0, GlobalUtilities.DEFAULT_DEATH_SIZE, GlobalUtilities.DEFAULT_DEATH_SIZE } },
            { EnemyFrame.Death4, new int[] { 48, 0, GlobalUtilities.DEFAULT_DEATH_SIZE, GlobalUtilities.DEFAULT_DEATH_SIZE } },

            { EnemyFrame.DodongoDown1, new int[]{ 1, 58, 16, 16 } },
            { EnemyFrame.DodongoDown2, new int[]{ 121, 77, 16, 16 } },
            { EnemyFrame.DodongoDownBomb, new int[] { 18, 58, 16, 16 } },
            { EnemyFrame.DodongoUp1, new int[]{ 35, 58, 16, 16 } },
            { EnemyFrame.DodongoUp2, new int[]{ 140, 77, 16, 16 } },
            { EnemyFrame.DodongoUpBomb, new int[]{ 52, 58, 16, 16 } },
            { EnemyFrame.DodongoRight1, new int[]{ 69, 62, 32, 16 } },
            { EnemyFrame.DodongoRight2, new int[]{ 102, 60, 32, 16 } },
            { EnemyFrame.DodongoRightBomb, new int[]{ 135, 59, 32, 16 } },
            { EnemyFrame.DodongoLeft1, new int[]{ 102, 42, 32, 16 } },
            { EnemyFrame.DodongoLeft2, new int[]{ 341, 18, 32, 15 } },
            { EnemyFrame.DodongoLeftBomb, new int[]{ 135, 42, 32, 15 } },
        };

        public static List<EnemyFrame> dungeonEnemies = new List<EnemyFrame>()
        {
            EnemyFrame.Blade,
            EnemyFrame.Keese1,
            EnemyFrame.Keese2,
            EnemyFrame.Gel1,
            EnemyFrame.Gel2,
            EnemyFrame.BlueGel1,
            EnemyFrame.BlueGel2,
            EnemyFrame.Wallmaster1,
            EnemyFrame.Wallmaster2,
            EnemyFrame.Rope1,
            EnemyFrame.Rope2,
            EnemyFrame.Rope3,
            EnemyFrame.Rope4,
            EnemyFrame.BlueGoriyaBackward,
            EnemyFrame.BlueGoriyaForward,
            EnemyFrame.BlueGoriyaLeft1,
            EnemyFrame.BlueGoriyaRight1,
            EnemyFrame.BlueGoriyaRight2,
            EnemyFrame.BlueGoriyaLeft2,
            EnemyFrame.DodongoDown1,
            EnemyFrame.DodongoDown2,
            EnemyFrame.DodongoDownBomb,
            EnemyFrame.DodongoUp1,
            EnemyFrame.DodongoUp2,
            EnemyFrame.DodongoUpBomb,
            EnemyFrame.DodongoRight1,
            EnemyFrame.DodongoRight2,
            EnemyFrame.DodongoRightBomb,
            EnemyFrame.DodongoLeft1,
            EnemyFrame.DodongoLeft2,
            EnemyFrame.DodongoLeftBomb,
            EnemyFrame.MoldormSection
        };

        public static List<EnemyFrame> Death = new List<EnemyFrame>()
        {
            EnemyFrame.Death1,
            EnemyFrame.Death2,
            EnemyFrame.Death3,
            EnemyFrame.Death4,
        };


        private Texture2D EnemySpriteSheet;
        private Texture2D BossSpriteSheet;
        private Texture2D DungeonEnemies;
        private Texture2D EnemyDeath;
        public const int ENEMY_HEIGHT = 127;
        public const int ENEMY_WIDTH = 127;

        public const int SCALING_FACTOR = 3;

        public static EnemySpriteFactory instance = new EnemySpriteFactory();
        public static EnemySpriteFactory Instance
        {
            get { return instance; }
        }

        private EnemySpriteFactory()
        {

        }

        public void LoadAllTextures(ContentManager content)
        {
            EnemySpriteSheet = content.Load<Texture2D>("enemies");
            BossSpriteSheet = content.Load<Texture2D>("Bosses");
            DungeonEnemies = content.Load<Texture2D>("Enemies Fixed");
            EnemyDeath = content.Load<Texture2D>("Enemy Death");


        }

        public ISprite CreateSmallEnemy(EnemySpriteFactory.EnemyFrame frame) 
        {
            int[] frames = EnemySpriteFactory.enemyFrameDictionary.GetValueOrDefault(frame);

            if(dungeonEnemies.Contains(frame))
            {
                return new EnemySprite(DungeonEnemies, frames[0], frames[1], frames[2], frames[3]);
            }
            else if (Death.Contains(frame))
            {
                return new EnemySprite(EnemyDeath, frames[0], frames[1], frames[2], frames[3]);

            }
            else
            {
                return new EnemySprite(EnemySpriteSheet, frames[0], frames[1], frames[2], frames[3]);

            }
        }

        public ISprite CreateLargeEnemy(EnemySpriteFactory.EnemyFrame frame) 
        {
            int[] frames = EnemySpriteFactory.enemyFrameDictionary.GetValueOrDefault(frame);

            return new EnemySprite(BossSpriteSheet, frames[0], frames[1], frames[2], frames[3]);
            
        }

    }


}

