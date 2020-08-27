namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Main class initiated at the beginning of the program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Static void which is the first methods that is used. It is used
        /// to launch the the struct to have the properties of the file in 
        /// args, and to launch the simulator itself.
        /// </summary>
        /// <param name="args">The arguments of the user, used to have the file 
        /// name with the properties.</param>
        static void Main(string[] args)
        {
            //This will see the file used by the user and see the arguments 
            // wrote in it for the simulation
            Properties properties = Properties.ReadFile(args);
            //Creates the simulation with the arguments of the file
            Simulator simulation = new Simulator(properties);
            //Starts the simulation
            simulation.SimulatorRunner();
        }
    }
}
