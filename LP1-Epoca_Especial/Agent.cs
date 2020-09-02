namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Class creating each agent in the world.
    /// </summary>
    public class Agent
    {
        private Position _pos;

        private AgentType _type;

        /// <summary>
        /// Public Vector to get the position of the Agent.
        /// /// </summary>
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
        /// <param name="vector">Position of the position you want the agent
        /// to have</param>
        public void Move(Position vector)
        {
            _pos = vector;
        }
    }
}