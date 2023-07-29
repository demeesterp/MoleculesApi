using Microsoft.Extensions.Logging;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecule.infrastructure.data.interfaces.Repositories;
using molecules.core.aggregates;
using molecules.core.factories;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.services
{
    public class CalcMoleculeService : ICalcMoleculeService
    {

        private readonly ICalcMoleculeFactory _factory;

        private readonly IMoleculeRepository _repository;

        private readonly ILogger<CalcMoleculeService> _logger;


        public CalcMoleculeService(ICalcMoleculeFactory factory,
                                        IMoleculeRepository repository,
                                            ILogger<CalcMoleculeService> logger)
        {
            _factory = factory;
            _repository = repository;
            _logger = logger;
        }

        public async Task<CalcMolecule> GetAsync(int id)
        {
            _logger.LogInformation("GetAsync {0}", id);

             var moleculeDbEntity = await _repository.GetByIdAsync(id);

             return _factory.BuildMolecule(moleculeDbEntity);
        }

        public async Task<CalcMolecule> GetForOrderItemIdAsync(int id)
        {
            _logger.LogInformation("GetForOrderItemIdAsync {0}", id);

            var moleculeDbEntity = await _repository.GetByOrdeItemIdAsync(id);

            return _factory.BuildMolecule(moleculeDbEntity);
        }

        public async Task<CalcMolecule> CreateAsync(CalcMolecule molecule)
        {
            _logger.LogInformation("CreateAsync");
            
            string moleculeStringData = "";
            
            if ( molecule.Molecule != null)
            {
                moleculeStringData = Molecule.SerializeToJsonString(molecule.Molecule);
            }
            
            var moleculeDbEntity =
                await _repository.CreateAsync(new MoleculeDbEntity()
                {
                    Id = molecule.Id,
                    OrderItemId = molecule.CalcOrderItemId,
                    MoleculeName = molecule.MoleculeName,
                    Molecule = moleculeStringData
                });

            await _repository.SaveChangesAsync();


            return _factory.BuildMolecule(moleculeDbEntity);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("DeleteAsync");

            await _repository.DeleteAsync(id);

            await _repository.SaveChangesAsync();
        }



        public async Task<CalcMolecule> UpdateAsync(int id, Molecule molecule)
        {
            _logger.LogInformation("UpdateAsync");

            var result = await _repository.UpdateAsync(id, molecule.Name, Molecule.SerializeToJsonString(molecule));

            await _repository.SaveChangesAsync();

            return _factory.BuildMolecule(result);
        }
    }
}
