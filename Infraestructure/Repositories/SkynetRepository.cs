using Core.Enumerations;
using Core.Intefaces;
using Core.Models.Configuration;
using Core.Models;
using Infraestructure.Interfaces;
using Infraestructure.Services;

namespace Infraestructure.Repositories
{
    public class SkynetRepository : ISkynetRepository
    {
        private readonly BDModel _vepBd;
        private readonly ICryptoService _cryptoService;
        private readonly IDBService _bdService;
        private readonly IConfigurationService _configurationService;
        private readonly string _connectionString;

        public SkynetRepository(ICryptoService cryptoService, IDBService bdService, IConfigurationService configurationService, IWritableOptions<ConfigurationDB> configurationBD)
        {
            _cryptoService = cryptoService;
            _bdService = bdService;
            _configurationService = configurationService;

            _vepBd = _configurationService.Get<ConfigurationDB>(Configuration.ConnectionStrings).DB_SKYNET;
            if (!_vepBd.Password.StartsWith("$"))
            {
                configurationBD.Update(configuration =>
                {
                    configuration.DB_SKYNET.Password = $"${_cryptoService.Encode(_vepBd.Password)}";
                });
            }
            else
                _vepBd.Password = cryptoService.Decode(_vepBd.Password.Replace("$",""));

            _connectionString = $"Server={_vepBd.Server};Database={_vepBd.Name};" +
                              $"User Id={_vepBd.User};Password={_vepBd.Password}";

        }
        public async Task<ResponseDB> CallSP(string sp, Dictionary<string, dynamic> parameters) => await _bdService.CallSP(_connectionString, sp, parameters);
        public async Task<ResponseDB> CallSPData(string sp, Dictionary<string, dynamic> parameters) => await _bdService.CallSPData(_connectionString, sp, parameters);
    }
}
