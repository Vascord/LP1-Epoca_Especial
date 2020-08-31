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

                // Creates the event list that will contain the events
                List<Event> eventList = new List<Event>();

                // Puts the number of events in the list
                eventList = PuttingEvents(eventList, numSwap, 1);
                eventList = PuttingEvents(eventList, numRepr, 2);
                eventList = PuttingEvents(eventList, numSelc, 3);

                // Shuffle the list
                eventList = ShuffleList(eventList);

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

        /// <summary>
        /// Method doing the Poisson algorithm to calculate the number of
        /// times a event is reproduced.
        /// </summary>
        /// <param name="lambda">lambda from the specief event depending
        /// of the expRate of the event</param>
        private int Poisson(double lambda)
        {
            // Variables for the Poisson algorithm

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

        /// <summary>
        /// Method putting the number of each type of event in a list
        /// </summary>
        /// <param name="eventList">The list containing the list of
        /// events</param>
        /// <param name="numEvent">The number of the type of the 
        /// event</param>
        /// <param name="type">Number to define the type of event</param>
        private List<Event> PuttingEvents(List<Event> eventList, int numEvent,
        int type)
        {
            // Switch case putting the events in a list
            switch(type)
            {
                //Putting the number of swap events in the list
                case(1):
                    for(int i = 0; i < numEvent; i++)
                    {
                        Event a = new Event(EventType.SWAP);
                        eventList.Add(a);
                    }
                    break;
                //Putting the number of reproduction events in the list
                case(2):
                    for(int i = 0; i < numEvent; i++)
                    {
                        Event b = new Event(EventType.REPRODUCTION);
                        eventList.Add(b);
                    }
                    break;
                //Putting the number of selection events in the list
                case(3):
                    for(int i = 0; i < numEvent; i++)
                    {
                        Event c = new Event(EventType.SELECTION);
                        eventList.Add(c);
                    }
                    break;
            }

            return eventList;
        }

        private List<Event> ShuffleList(List<Event> eventList)
        {
            // Cycle suffling the list
            for(int i = eventList.Count-1; i > 0; i--)
            {
                // Selects a random number for the position in the list
                int selected = _random.Next(i-1);

                // Swaps the events from the i position and the selected 
                // position
                Event a = eventList[selected];
                eventList[selected] = eventList[i];
                eventList[i] = a;
            }

            return eventList;
        }
    }
}