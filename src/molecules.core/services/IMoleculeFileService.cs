namespace molecules.core.services
{
    public interface IMoleculeFileService
    {
        Task<string> GetXyzFileContentAsync(int moleculeId);
    }
}
