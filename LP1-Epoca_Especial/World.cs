namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Class defining the world of the simulation.
    /// </summary>
    public class World
    {
        /// <summary>
        /// Dimension X of the world.
        /// </summary>
        private int _worldSizeX;

        /// <summary>
        /// Dimension Y of the world.
        /// </summary>
        private int _worldSizeY;

        /// <summary>
        /// MultiDimentional Array defined by _worldSizeX and _worldSize Y
        /// </summary>
        private int[,] world;

        /// <summary>
        /// Indexing world
        /// </summary>
        public int this[int x, int y] 
        {
            get { return world[x,y]; }
            set { world[x,y] = value;}
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="worldSizeX">Dimension X of the world</param>
        /// <param name="worldSizeY">Dimension Y of the world</param>
        public World(int worldSizeX, int worldSizeY)
        {
            // Initializes instance variables
            
            this._worldSizeX = worldSizeX;
            this._worldSizeY = worldSizeY;

            world = new int[worldSizeX, worldSizeY];
        }
    }
}