using AutoMapper;
using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.Extensions.Options;

namespace DsK.ITSM.Security.Infrastructure
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
            this.Mapper= Mapper;            
        }
    }
}
