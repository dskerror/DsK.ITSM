using AutoMapper;
using DsK.ITSM.Models;
using Microsoft.Extensions.Options;

namespace DsK.ITSM.Services
{
    public partial class SecurityService
    {   
        private readonly TokenSettingsModel _tokenSettings;
        private readonly DsKitsmContext db;
        private IMapper Mapper;

        public SecurityService(IOptions<TokenSettingsModel> tokenSettings, DsKitsmContext db, IMapper Mapper)
        {
            _tokenSettings = tokenSettings.Value;
            this.db = db;
            this.Mapper = Mapper;
        }
    }
}
