namespace molecules.core.services
{
    public interface ICalcDeliveryService
    {
        Task ExportCalcDeliveryInputAsync(string basePath);

        Task ImportCalcDeliveryOutputAsync(string basePath);
    }
}
