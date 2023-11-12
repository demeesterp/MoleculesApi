namespace molecules.core.services.CustomConversions
{
    public interface ICalcFileConversionService
    {
        void ConvertMoleculeToXyzFileAsync(string basePath);

        void ConvertGeoOptFileToXyzFileAsync(string basePath);

    }
}
