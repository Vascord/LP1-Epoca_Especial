namespace LP1_Epoca_Especial
{
    public struct Event
    {
        /// <summary>
        /// Type of event.
        /// </summary>
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