using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

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

            // Miguel Romão Fernández REMEMBER
        }

        /// <summary>
        /// Main loop of the simulation that permits the simulation to run.
        /// </summary>
        public void CoreLoop() 
        {
            // Loop of the simulation
            while(true)
            {
                // Main Code

                // Obtaining lambda from each event

                double lambdaSwap = (_prop.worldSizeX * _prop.worldSizeY / 3.0) 
                * Math.Pow(10, _prop.swapRateExp);
                double lambdaRepr = (_prop.worldSizeX * _prop.worldSizeY / 3.0) 
                * Math.Pow(10, _prop.reprRateExp);
                double lambdaSelc = (_prop.worldSizeX * _prop.worldSizeY / 3.0) 
                * Math.Pow(10, _prop.selcRateExp);

                // Obtaining the number of times those events happen in the turn

                int numSwap = Poisson(lambdaSwap);
                int numRepr = Poisson(lambdaRepr);
                int numSelc = Poisson(lambdaSelc);

                // Swap : only change pos
                // Reproduction : create a new agent a
                // Selection : WIP
            }
        }

        /// <summary>
        /// Loop of the simulation that stops the program if you press
        /// Escape. It also creates the Thread ThreadProc to have the main
        /// code doing the Events.
        /// </summary>
        public void SimulatorRunner()
        {
            // Launch the Thread ThreadProc
            Task.Run(CoreLoop);

            // While the Escape Key is not pressed, the code don't stop
            while(Console.ReadKey().Key != ConsoleKey.Escape);

            // David D. Ajudo-me :)
        }

        private int Poisson(double lambda)
        {
            // Variables for the Poisson algorythme

            int k = 0;
            double p = 1;
            double step = 500;
            double u;

            // Loop to obtain the number of times for the event

            do{
                k += 1;
                u = _random.NextDouble();
                p *= u;
                while( (p < 1) && (lambda > 0))
                    if (lambda > step)
                    {
                        p *= Math.Exp(step);
                        lambda -= step;
                    }
                    else
                    {
                        p *= Math.Exp(lambda);
                        lambda = 0;
                    }
            }
            while(p > 1);

            return (k - 1);
        }
    }
}