using System;

namespace LP1_Epoca_Especial
{
    public class UI
    {
        /// <summary>
        /// World of the simulation
        /// </summary>
        private World _world;

        /// <summary>
        /// Properties of the simulation
        /// </summary>
        private Properties _prop;

        public UI(World world, Properties prop)
        {
            this._world = world;
            this._prop = prop;
        }

        /// <summary>
        /// Method responsible for showing the world of the simalation.
        /// </summary>
        public void simUI()
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