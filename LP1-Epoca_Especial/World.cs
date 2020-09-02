namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Class defining the world of the simulation.
    /// </summary>
    public class World
    {
        private int[,] _world ;

        /// <summary>
        /// Indexing world
        /// </summary>
        public int this[int x, int y] 
        {
            get { return _world[x,y]; }
            set { _world[x,y] = value; }
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="worldSizeX">Dimension X of the world</param>
        /// <param name="worldSizeY">Dimension Y of the world</param>
        public World(int worldSizeX, int worldSizeY)
        {
            // Initializes instance variables
            _world = new int[worldSizeX, worldSizeY];
        }
    }
}