namespace LP1_Epoca_Especial
{
    /// <summary>
    /// Struct use create the events of the simulation and defining it's type
    /// </summary>
    public struct Event
    {
        private EventType _type;

        /// <summary>
        /// Type of event.
        /// </summary>
        public EventType Type => _type;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="type">Type of Event</param>
        public Event(EventType type)
        {
            // Initializes instance variables

            _type = type;
        }

    }
}