using System;

namespace Pentago
{
    class Sounds
    {

        public Sounds()
        {
            //makes a sound class
        }

        public void play(String str)
        {
            switch(str)
            {
                case "click":
                    playSound(Properties.Resources.click);
                    break;
                case "marble":
                    playSound(Properties.Resources.marble);
                    break;
                case "arrow":
                    playSound(Properties.Resources.arrows);
                    break;
                case "winner":
                    playSound(Properties.Resources.winner);
                    break;
                case "error":
                    playSound(Properties.Resources.error);
                    break;
                default:
                    break;
            }
        }

        private void playSound(System.IO.Stream sound)
        {
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(sound);
            snd.Play();
        }
    }
}