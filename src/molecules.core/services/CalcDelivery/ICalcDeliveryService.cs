namespace molecules.core.services.CalcDelivery
{
    public interface ICalcDeliveryService
    {
        Task ExportCalcDeliveryInputAsync(string basePath);

        Task ImportCalcDeliveryOutputAsync(string basePath);
    }
}
