using System;
using System.IO;
using System.Linq;
using System.Globalization;

namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Struct responsible for reading the file from the user with the
    /// inputs to make them available as properties.
    /// </summary>
    public struct Properties
    {
        /// <summary>
        /// Auto-implemented property that contains the horizontal dimension of 
        /// the simulation grid.
        /// </summary>
        /// <value>The horizontal dimensions of a simulation grid.</value>
        public int worldSizeX {get; private set;}
         /// <summary>
        /// Auto-implemented property that contains the vertical dimension of 
        /// the simulation grid.
        /// </summary>
        /// <value>The vertical dimensions of a simulation grid.</value>
        public int worldSizeY {get; private set;}
        /// <summary>
        /// Auto-implemented property that contains the probability of the event
        /// swap in the simulation.
        /// </summary>
        /// <value>Value between -1.0 and 1.0 for the probability of the event
        /// swap in the simulation.</value>
        public double swapRateExp {get; private set;}
        /// <summary>
        /// Auto-implemented property that contains the probability of the event
        /// reproduction in the simulation.
        /// </summary>
        /// <value>Value between -1.0 and 1.0 for the probability of the event
        /// reproduction in the simulation.</value>
        public double reprRateExp {get; private set;}
        /// <summary>
       /// Auto-implemented property that contains the probability of the event
        /// selection in the simulation.
        /// </summary>
        /// <value>Value between -1.0 and 1.0 for the probability of the event
        /// selection in the simulation.</value>
        public double selcRateExp {get; private set;}

        /// <summary>
        /// Private constructor that initializes an instance of this struct
        /// with valid values for the different command line options.
        /// </summary>
        /// <param name="X">Horizental dimension of the grid.</param>
        /// <param name="Y">Vertical dimension of the grid.</param>
        /// <param name="S">Rate of probability to have the event swap.</param>
        /// <param name="R">Rate of probability to have the event 
        /// reproduction.</param>
        /// <param name="F">Rate of probability to have the event 
        /// selection.</param>
        private Properties(int X, int Y, double S, double R, double F)
        {
            worldSizeX = X;
            worldSizeY = Y;
            swapRateExp = S;
            reprRateExp = R;
            selcRateExp = F;
        }

        /// <summary>
        /// Static method that parses the properties in the file of the user
        /// and returns a new <see cref="Properties"/> object which contains
        /// those same properties after being treated.
        /// </summary>
        /// <param name="args">Arguments from the command line.</param>
        /// <returns>An object of type <see cref="Properties"/> which contains 
        /// the simulation properties.</returns>
        public static Properties ReadFile(string[] args)
        {
            // Variables for properties
            int X = 0, Y = 0;
            double S = -3.00, R = -3.00, F = -3.00;
            // Variables for file reading
            int i = 0;
            string fileName = null;
            string line = null, firstWord = null, lastWord = null;

            string docPath = Directory.GetCurrentDirectory();

            // Check if there is juste the name of the file
            if (args.Length != 1)
            {
                Console.WriteLine("Please run with only a file name");
                return new Properties();
            }

            fileName = args[0];

            // Read the file and display it line by line, searching for
            // the properties and ignoring commentaries
            System.IO.StreamReader file =
                new System.IO.StreamReader(Path.Combine(docPath, fileName));  
            while((line = file.ReadLine()) != null)  
            {  
                i++;
                line = line.Trim();
                firstWord = line.Split(' ').First();
                if((firstWord == "#") || (firstWord == "//") || 
                (firstWord == "")) {}
                else if(firstWord == "selc-rate-exp")
                {
                    lastWord = line.Split(' ').Last();
                    if(double.TryParse(lastWord, NumberStyles.Any, 
                        CultureInfo.InvariantCulture, out F)){}
                    else
                    {
                        Console.WriteLine(
                            "Please run with a file with the proper data");
                        return new Properties();
                    }
                }
                else if(firstWord == "swap-rate-exp")
                {
                    lastWord = line.Split(' ').Last();
                    if(double.TryParse(lastWord, NumberStyles.Any, 
                        CultureInfo.InvariantCulture, out S)){}
                    else
                    {
                        Console.WriteLine(
                            "Please run with a file with the proper data");
                        return new Properties();
                    }
                }
                else if(firstWord == "repr-rate-exp")
                {
                    lastWord = line.Split(' ').Last();
                    if(double.TryParse(lastWord, NumberStyles.Any, 
                        CultureInfo.InvariantCulture, out R)) {}
                    else
                    {
                        Console.WriteLine(
                            "Please run with a file with the proper data");
                        return new Properties();
                    }
                }
                else if(firstWord == "xdim")
                {
                    lastWord = line.Split(' ').Last();
                    if(int.TryParse(lastWord, out X)) {}
                    else
                    {
                        Console.WriteLine(
                            "Please run with a file with the proper data");
                        return new Properties();
                    }
                }
                else if(firstWord == "ydim")
                {
                    lastWord = line.Split(' ').Last();
                    if(int.TryParse(lastWord, out Y)) {}
                    else
                    {
                        Console.WriteLine(
                            "Please run with a file with the proper data");
                        return new Properties();
                    }
                }
                else
                {
                    Console.WriteLine($"You have a invalid option in line {i}");
                }
            }

            // Verifies if every properties have been filled
            if(X <= 0 || Y <= 0 || S < -1.0 || R < -1.0 || F < -1.0 || 
            S > 1.0 || R > 1.0 || F > 1.0)
            {
                Console.WriteLine(
                    "Please run with a file with the proper data");
                return new Properties();
            }

            // Returns all the proprieties of the file
            return new Properties(X, Y, S, R, F);
        }
    }
}