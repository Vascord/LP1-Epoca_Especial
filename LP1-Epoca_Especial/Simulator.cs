using System;
using System.Collections.Generic;
using System.Threading;

namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Class responsible for managing the simulation's main functions and 
    /// loops.
    /// </summary>
    public class Simulator
    {
        private Properties _prop;

        private Random _random;

        private List<Agent> _agents;

        private World _world;

        private UI _ui;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="properties">Simulation properties</param>
        public Simulator(Properties properties)
        {
            // Variables

            int x,y;
            this._prop = properties;
            _world = new World( _prop.worldSizeX, _prop.worldSizeY );

            _random = new Random();
            _agents = new List<Agent>();
            _ui = new UI(_world, _prop);

            // For each position in world, place a agent of a type or not

            for(x = 0; x<_prop.worldSizeX; x++)
            {
                for(y = 0; y<_prop.worldSizeY; y++)
                {
                    Position p = new Position(x, y);
                    int agentType = _random.Next(4);

                    switch (agentType)
                    {
                        case(1):
                            Agent a = new Agent(p, AgentType.PAPER);   
                            _agents.Add(a);
                            break;
                        case(2):
                            Agent b = new Agent(p, AgentType.ROCK); 
                            _agents.Add(b);  
                            break;
                        case(3):
                            Agent c = new Agent(p, AgentType.SCISSOR);   
                            _agents.Add(c);
                            break;
                        default:
                            break;
                    }
                }
            }

            // Place Agents of each type in world

            _world = WorldUpdate(_agents, _world);

            // The ui creates a visualization of the world for the
            // user to see
            _ui.VisualizacaoUI();
        }

        /// <summary>
        /// Main loop of the simulation that permits the simulation to run.
        /// </summary>
        public void CoreLoop() 
        {
            // Loop of the simulation
            while(true)
            {
                // Initializes instance variables

                double lambdaSwap,lambdaRepr,lambdaSelc;
                int numSwap,numRepr,numSelc;
                List<Event> eventList;

                // Obtaining lambda from each event

                lambdaSwap = (_prop.worldSizeX * _prop.worldSizeY / 3.0) 
                * Math.Pow(10, _prop.swapRateExp);
                lambdaRepr = (_prop.worldSizeX * _prop.worldSizeY / 3.0) 
                * Math.Pow(10, _prop.reprRateExp);
                lambdaSelc = (_prop.worldSizeX * _prop.worldSizeY / 3.0) 
                * Math.Pow(10, _prop.selcRateExp);

                // Obtaining the number of times those events happen in the turn

                numSwap = Poisson(lambdaSwap);
                numRepr = Poisson(lambdaRepr);
                numSelc = Poisson(lambdaSelc);

                // Creates the event list that will contain the events
                eventList = new List<Event>();

                // Puts the number of events in the list
                eventList = PuttingEvents(eventList, numSwap, 1);
                eventList = PuttingEvents(eventList, numRepr, 2);
                eventList = PuttingEvents(eventList, numSelc, 3);

                // Shuffle the list
                eventList = ShuffleList(eventList);

                // Will do this turn with the events in the event list
                for(int i = 0; i < eventList.Count; i++)
                {
                    // Get the pos of a random location in the world and get a
                    // pos next to it according to the Von Neumann
                    // neighbourhood
                    Position originalPos = GetPos();
                    Position adjacentPos = AdjacentPos(originalPos);
                    
                    // See the type of event and does it for the two selected
                    // cases
                    switch(eventList[i].Type)
                    {
                        case(EventType.SWAP):
                        {
                            _agents = SwapEvent(_agents, _world, originalPos, 
                            adjacentPos);
                            
                            break;
                        }
                        case(EventType.REPRODUCTION):
                        {
                            _agents = ReproductionEvent(_agents, _world, 
                            originalPos, adjacentPos);

                            break;
                        }
                        case(EventType.SELECTION):
                        {
                            _agents = SelectionEvent(_agents, _world, 
                            originalPos, adjacentPos);

                            break;
                        }
                    }
                }
                // Clear the console
                Console.Clear();

                // The world is updated
                _world = WorldUpdate(_agents, _world);

                // The ui creates a visualization of the world for the
                // user to see
                _ui.VisualizacaoUI();
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
            Thread main = new Thread(CoreLoop);
            main.Start();

            // While the Escape Key is not pressed, the code don't stop
            while(Console.ReadKey().Key != ConsoleKey.Escape);

            // Close the program ( all threading )
            Environment.Exit(0);
        }

        /// <summary>
        /// Method doing the Poisson algorithm to calculate the number of
        /// times a event is reproduced.
        /// </summary>
        /// <param name="lambda">Lambda from the specified event depending
        /// of the expRate of the event</param>
        /// <return>Int of the number of times the event will occur<return>
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
        /// Method putting the number of a specific type of event in a list
        /// </summary>
        /// <param name="eventList">The list containing the list of
        /// events</param>
        /// <param name="numEvent">The number of the type of the 
        /// event</param>
        /// <param name="type">Number to define the type of event</param>
        /// <return>The event list with the number of the specific type
        /// of event<return>
        private List<Event> PuttingEvents(List<Event> eventList, int numEvent,
        int type)
        {
            // Variables

            int i;

            // Switch case putting the events in a list
            switch(type)
            {
                //Putting the number of swap events in the list
                case(1):
                    for(i = 0; i < numEvent; i++)
                    {
                        Event a = new Event(EventType.SWAP);
                        eventList.Add(a);
                    }
                    break;
                //Putting the number of reproduction events in the list
                case(2):
                    for(i = 0; i < numEvent; i++)
                    {
                        Event b = new Event(EventType.REPRODUCTION);
                        eventList.Add(b);
                    }
                    break;
                //Putting the number of selection events in the list
                case(3):
                    for(i = 0; i < numEvent; i++)
                    {
                        Event c = new Event(EventType.SELECTION);
                        eventList.Add(c);
                    }
                    break;
            }

            return eventList;
        }

        /// <summary>
        /// Method to shuffle the method list using the Fisher-Yates
        /// algorithm
        /// </summary>
        /// <param name="eventList">List with the events for the turn</param>
        /// <return>The event list shuffled<return>
        private List<Event> ShuffleList(List<Event> eventList)
        {
            // Variables
            int i, selected;

            // Cycle suffling the list
            for(i = eventList.Count-1; i > 0; i--)
            {
                // Selects a random number for the position in the list
                selected = _random.Next(i-1);

                // Swaps the events from the i position and the selected 
                // position
                Event a = eventList[selected];
                eventList[selected] = eventList[i];
                eventList[i] = a;
            }

            return eventList;
        }

        /// <summary>
        /// Method to get a random pos in the world
        /// </summary>
        /// <return>The position of the chosen case<return>
        private Position GetPos()
        {
            // Variables
            int x,y;

            // Generates a random position
            x = _random.Next(_prop.worldSizeX);
            y = _random.Next(_prop.worldSizeY);
            Position pos = new Position(x, y);

            return pos;
        }

        /// <summary>
        /// Method to pick the pos of an adjacent case of the original pos
        /// </summary>
        /// <param name="originalPos">Position of the random picked pos</param>
        /// <return>The position of a adjacent position to the original position
        /// <return>
        private Position AdjacentPos(Position originalPos)
        {
            // Variables
            int x, y, randomDirection;
            Position pos;

            // Will search for a adjacent position to the original position
            do{
                x = originalPos.X;
                y = originalPos.Y;
                randomDirection = _random.Next(4);

                switch(randomDirection)
                {
                    case(0):
                    {
                        y -= 1;
                        break;
                    }
                    case(1):
                    {
                        x += 1;
                        break;
                    }
                    case(2):
                    {
                        y += 1;
                        break;
                    }
                    case(3):
                    {
                        x -= 1;
                        break;
                    }
                }
            }while((y < -1) || (y > _prop.worldSizeY) || (x < -1) || 
            (x > _prop.worldSizeX));

            // Turn around the boarder if meet the right conditions
            if(x == -1)
            {
                x = _prop.worldSizeX-1;
            }
            else if(y == -1)
            {
                y = _prop.worldSizeY-1;
            }
            else if(x == _prop.worldSizeX)
            {
                x = 0;
            }
            else if(y == _prop.worldSizeY)
            {
                y = 0;
            }

            pos = new Position(x, y);

            return pos;
        }

        /// <summary>
        /// Method to update the World depending of the agents
        /// </summary>
        /// <param name="_agents">List with the agents</param>
        /// <param name="world">Multi-dimensional array used as a world of the 
        /// simulation</param>
        /// <return>The actualized World after checking the agents list<return>
        private World WorldUpdate(List<Agent> _agents, World world)
        {
            // For each agent, the world is updates depending of the agent
            foreach(Agent a in _agents)
            {
                switch(a.Type)
                {
                    case(AgentType.PAPER):
                    {
                        world[a.Pos.X, a.Pos.Y] = 1;
                        break;
                    }
                    case(AgentType.ROCK):
                    {
                        world[a.Pos.X, a.Pos.Y] = 2;
                        break;
                    }
                    case(AgentType.SCISSOR):
                    {
                        world[a.Pos.X, a.Pos.Y] = 3;
                        break;
                    }
                    default:
                    {
                        world[a.Pos.X, a.Pos.Y] = 0;
                        break;
                    }
                }
            }

            return world;
        }

        /// <summary>
        /// Method to do the swap event
        /// </summary>
        /// <param name="_agents">List with the agents</param>
        /// <param name="world">Multi-dimensional array used as a world of the 
        /// simulation</param>
        /// <param name="originalPos">First position affected by the 
        /// swap event</param>
        /// <param name="adjacentPos">Second position affected by the 
        /// swap event</param>
        /// <return>List of agents after the modifications of the event<return>
        private List<Agent> SwapEvent(List<Agent> _agents, World world, 
        Position originalPos, Position adjacentPos)
        {
            // Variables
            int i;

            // If there's a agent in one of the cases, then it moves
            for(i = 0; i < _agents.Count ; i++)
            {
                if((_agents[i].Pos.X == originalPos.X) && 
                (_agents[i].Pos.Y == originalPos.Y))
                {
                    _agents[i].Move(adjacentPos);
                }
                else if((_agents[i].Pos.X == adjacentPos.X) && 
                    (_agents[i].Pos.Y == adjacentPos.Y))
                {
                    _agents[i].Move(originalPos);
                }
            }

            return _agents;
        }

        /// <summary>
        /// Method to do the reproduction event
        /// </summary>
        /// <param name="_agents">List with the agents</param>
        /// <param name="world">Multi-dimensional array used as a world of the 
        /// simulation</param>
        /// <param name="originalPos">First position affected by the 
        /// reproduction event</param>
        /// <param name="adjacentPos">Second position affected by the 
        /// reproduction event</param>
        /// <return>List of agents after the modifications of the event<return>
        private List<Agent> ReproductionEvent(List<Agent> _agents, World world, 
        Position originalPos, Position adjacentPos)
        {
            // Variables
            int i;

            // Adds the new agent to the world
            for(i = 0; i < _agents.Count ; i++)
            {
                if((_agents[i].Pos.X == originalPos.X) && 
                (_agents[i].Pos.Y == originalPos.Y))
                {
                    if(world[adjacentPos.X, adjacentPos.Y] == 0)
                    {
                        Agent a = new Agent(adjacentPos, _agents[i].Type);
                        _agents.Add(a);
                        break;
                    }
                }
                else if((_agents[i].Pos.X == adjacentPos.X) && 
                (_agents[i].Pos.Y == adjacentPos.Y))
                {
                    if(world[originalPos.X, originalPos.Y] == 0)
                    {
                        Agent b = new Agent(originalPos, _agents[i].Type);
                        _agents.Add(b);
                        break;
                    }
                }
            }

            return _agents;
        }

        /// <summary>
        /// Method to do the Selection event
        /// </summary>
        /// <param name="_agents">List with the agents</param>
        /// <param name="world">Multi-dimensional array used as a world of the 
        /// simulation</param>
        /// <param name="originalPos">First position affected by the 
        /// selection event</param>
        /// <param name="adjacentPos">Second position affected by the 
        /// selection event</param>
        /// <return>List of agents after the modifications of the event<return>
        private List<Agent> SelectionEvent(List<Agent> _agents, World world, 
        Position originalPos, Position adjacentPos)
        {
            // Variables
            int posInAgent1 = -1, posInAgent2 = -1, i;

            // Resets the position of both cases
            world[adjacentPos.X, adjacentPos.Y] = 0;
            world[originalPos.X, originalPos.Y] = 0;

            // Will take the position of the agents in the random cases
            for(i = 0; i < _agents.Count ; i++)
            {
                if((_agents[i].Pos.X == originalPos.X) && 
                (_agents[i].Pos.Y == originalPos.Y))
                {
                    posInAgent1 = i;
                }
                else if((_agents[i].Pos.X == adjacentPos.X) && 
                    (_agents[i].Pos.Y == adjacentPos.Y))
                {
                    posInAgent2 = i;
                }
            }

            // Checks if the two positions are not empty
            if((posInAgent1 > -1) && (posInAgent2 > -1))
            {
                // agent a and b are created to clash in a paper, rock, scissor game
                Agent a = new Agent(originalPos, _agents[posInAgent1].Type);
                Agent b = new Agent(adjacentPos, _agents[posInAgent2].Type);

                // See if the agent a or b win against one or another
                if((a.Type == AgentType.PAPER) && (b.Type == AgentType.ROCK))
                {
                    _agents.Remove(_agents[posInAgent2]);
                    world[adjacentPos.X, adjacentPos.Y] = 0;
                }
                else if((a.Type == AgentType.ROCK) && (b.Type == AgentType.SCISSOR))
                {
                    _agents.Remove(_agents[posInAgent2]);
                    world[adjacentPos.X, adjacentPos.Y] = 0;
                }
                else if((a.Type == AgentType.SCISSOR) && 
                (b.Type == AgentType.PAPER))
                {
                    _agents.Remove(_agents[posInAgent2]);
                    world[adjacentPos.X, adjacentPos.Y] = 0;
                }
                else if((b.Type == AgentType.PAPER) && (a.Type == AgentType.ROCK))
                {
                    _agents.Remove(_agents[posInAgent1]);
                    world[originalPos.X, adjacentPos.Y] = 0;
                }
                else if((b.Type == AgentType.ROCK) && (a.Type == AgentType.SCISSOR))
                {
                    _agents.Remove(_agents[posInAgent1]);
                    world[originalPos.X, adjacentPos.Y] = 0;
                }
                else if((b.Type == AgentType.SCISSOR) && 
                (a.Type == AgentType.PAPER))
                {
                    _agents.Remove(_agents[posInAgent1]);
                    world[originalPos.X, adjacentPos.Y] = 0;
                }
            }

            return _agents;
        }
    }
}