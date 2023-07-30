namespace molecules.core.services
{
    public interface ICalcFileConversionService
    {
        void ConvertMoleculeToXyzFileAsync(string basePath);

        void ConvertGeoOptFileToXyzFileAsync(string basePath);

    }
}
