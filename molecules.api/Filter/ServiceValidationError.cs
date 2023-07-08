namespace molecules.api.Filter
{
    /// <summary>
    /// Gives information about a service error
    /// </summary>
    public class ServiceValidationError
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceValidationError()
        {
            ValidationErrors = new List<ServiceValidationErrorItem>();
        }

        /// <summary>
        /// The list of error
        /// </summary>
        public IList<ServiceValidationErrorItem> ValidationErrors { get; set; }

    }
}