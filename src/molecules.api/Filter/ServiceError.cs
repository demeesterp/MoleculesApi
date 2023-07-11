namespace molecules.api.Filter
{
    /// <summary>
    /// Gives information about a service error
    /// </summary>
    public class ServiceError
    {
        /// <summary>
        /// Message to display to the user
        /// </summary>
        public string DisplayMessage { get; set; } = "An unexpected error happend please contact support";
    }
}
