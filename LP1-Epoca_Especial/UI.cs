using System;

namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Class use create an UI for the user
    /// </summary>
    public class UI
    {
        private World _world;

        private Properties _prop;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="world">MultiDimensional array, being the world
        /// of the simulation</param>
        /// <param name="prop">Properities of the simulation</param>
        public UI(World world, Properties prop)
        {
            this._world = world;
            this._prop = prop;
        }

        /// <summary>
        /// Method responsible for showing the world of the simalation.
        /// </summary>
        public void VisualizacaoUI()
        {
            Console.Clear();
            for(int x = 0; x < _prop.worldSizeX; x++)
            {
                for(int y = 0; y < _prop.worldSizeY; y++)
                {
                    switch(_world[x,y])
                    {
                        case(1):
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("  ");
                            Console.ResetColor();
                            break;
                        }
                        case(2):
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("  ");
                            Console.ResetColor();
                            break;
                        }
                        case(3):
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write("  ");
                            Console.ResetColor();
                            break;
                        }
                        default:
                        {
                            Console.Write("  ");
                            break;
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}