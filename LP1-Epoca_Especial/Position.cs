namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Struct to have Vector2 as int.
    /// </summary>
    public struct Position
    {
        /// <summary>
        /// Pos X of the Vector.
        /// </summary>
        public int X {get; }
        /// <summary>
        /// Pos Y of the Vector.
        /// </summary>
        public int Y {get; }

        /// <summary>
        /// Struct constructor.
        /// </summary>
        /// <param name="x">Pos X of the Vector</param>
        /// <param name="y">Pos Y of the Vector</param>
        public Position (int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}