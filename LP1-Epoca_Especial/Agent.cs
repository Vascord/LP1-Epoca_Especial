namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Class creating each agent in the world.
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Position of the Agent.
        /// </summary>
        private Position _pos;

        /// <summary>
        /// Type of the Agent.
        /// </summary>
        private AgentType _type;

        /// <summary>
        /// Public Vector to get the position of the Agent.
        /// </summary>
        public Position Pos => _pos;

        /// <summary>
        /// Public Vector to get the type of the Agent.
        /// </summary>
        public AgentType Type => _type;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="pos">Position of the Agent</param>
        /// <param name="type">Type of the Agent</param>
        public Agent(Position pos, AgentType type)
        {
            // Initializes instance variables

            _pos = pos;
            _type = type;
        }

        /// <summary>
        /// Changes the pos of the agent.
        /// </summary>
        public void Move(Position vector)
        {
            _pos = vector;
        }
        // public void Move(float x, float y)
        // {
        //     Vector2 newPos = new Vector2(x,y);
        //     Move(newPos);
        // }
        // void OnMove()
        // {
        //     //if vector => move(vector)
        //     //if floats => move(floats)
        // }
    }
}