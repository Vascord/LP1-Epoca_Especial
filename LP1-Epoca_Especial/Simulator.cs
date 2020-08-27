using System;
using System.Collections.Generic;

namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Class responsible for managing the simulation's main functions and 
    /// loops.
    /// </summary>
    public class Simulator
    {
        /// <summary>
        /// Simulation properties.
        /// </summary>
        private Properties _prop;

        /// <summary>
        /// Random generator.
        /// </summary>
        private Random _random;

        /// <summary>
        /// List of Agents who are paper.
        /// </summary>
        private List<Agent> _paper {get;}

        /// <summary>
        /// List of Agents who are rock.
        /// </summary>
        private List<Agent> _rock {get;}

        /// <summary>
        /// List of Agents who are scissor.
        /// </summary>
        private List<Agent> _scissor {get;}

        /// <summary>
        /// Instance of the simulation's world.
        /// </summary>
        public World world;
        
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="properties">Simulation properties</param>
        public Simulator(Properties properties)
        {
            // Initializes instance variables

            this._prop = properties;
            World world = new World( _prop.worldSizeX, _prop.worldSizeY );

            _random = new Random();
            _paper = new List<Agent>();
            _rock = new List<Agent>();
            _scissor = new List<Agent>();

            // For each position in world, place a agent of a type or not

            for(int x = 0; x<_prop.worldSizeX; x++)
            {
                for(int y = 0; y<_prop.worldSizeY; y++)
                {
                    Position p =new Position(x, y);
                    int agentType = _random.Next(4);

                    switch (agentType)
                    {
                        case(1):
                            Agent a = new Agent(p, AgentType.PAPER);   
                            _paper.Add(a);
                            break;
                        case(2):
                            Agent b = new Agent(p, AgentType.ROCK);   
                            _rock.Add(b);
                            break;
                        case(3):
                            Agent c = new Agent(p, AgentType.SCISSOR);   
                            _scissor.Add(c);
                            break;
                        default:
                            break;
                    }
                }
            }

            // Place Agents of each type in world

            foreach(Agent a in _paper)
            {
                world[a.Pos.X, a.Pos.Y] = 1;
            }
            foreach(Agent a in _rock)
            {
                world[a.Pos.X, a.Pos.Y] = 2;
            }
            foreach(Agent a in _scissor)
            {
                world[a.Pos.X, a.Pos.Y] = 3;
            }

            // Swap : only change pos
            // Reproduction : create a new agent a
            // Selection : WIP

            // Miguel Romão Fernández REMEMBER
        }

        /// <summary>
        /// Main loop of the simulation. Will stop at the end of X turns,
        /// which where choose be the user in properties.
        /// </summary>
        public void CoreLoop()
        {
            // WIP
        }
    }
}