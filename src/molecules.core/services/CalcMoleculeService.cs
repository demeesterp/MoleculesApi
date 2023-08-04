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

        public async Task<CalcMolecule?> FindAsync(string orderName, string basisSet, string moleculeName)
        {
            _logger.LogInformation("FindAsync for OrderName {0}, basisSet {1} moleculeNae {2} ", orderName, basisSet, moleculeName);

            var moleculeDbEntity = await _repository.FindAsync(orderName, basisSet, moleculeName);

            return moleculeDbEntity== null ? null : _factory.BuildMolecule(moleculeDbEntity);
        }

        public async Task<List<CalcMolecule>> FindAllByNameAsync(string moleculeName)
        {
            _logger.LogInformation("FindAllByNameAsync for moleculeName {0}", moleculeName); 
            
            var moleculeDbEntities = await _repository.FindAllByNameAsync(moleculeName);

            return moleculeDbEntities.OrderByDescending(i => i.OrderName)
                                    .ThenByDescending(i => i.MoleculeName)
                                    .ThenByDescending(i => i.Id)
                                    .Select(_factory.BuildMolecule).ToList();
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
                    OrderName = molecule.OrderName,
                    BasisSet = molecule.BasisSet,
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
