using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class SoundEffectGenerator
    {

        Game1 game;

        public SoundEffectGenerator(Game1 game)
        {
            this.game = game;
        }

        public void Play(String type)
        {
            SoundEffect soundEffect = game.Content.Load<SoundEffect>(type);
            soundEffect.Play();
        }

    }
}
