namespace molecules.api.Filter
{
    /// <summary>
    /// An error item to be send to the user in case of error
    /// </summary>
    public class ServiceValidationErrorItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="propertyName">The name of the property to what it applies</param>
        public ServiceValidationErrorItem(string message, string propertyName)
        {
            Message = message;
            PropertyName = propertyName;
        }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The name of the property to what it applies
        /// </summary>
        public string PropertyName { get; set; }
    }
}
