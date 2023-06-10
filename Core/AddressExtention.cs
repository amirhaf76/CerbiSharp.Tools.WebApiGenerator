using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerbiSharp.Tools.WebApiGenerator.Core
{
    public static class AddressExtention
    {
        public static ApiClientGeneratorConfig AddDevHitobitHapiAddress(this ApiClientGeneratorConfig apiClientGeneratorConfig)
        {
            apiClientGeneratorConfig.Url = Addresses.DEV_HITOBIT_HAPI_ADDRESS;

            return apiClientGeneratorConfig;
        }

        public static ApiClientGeneratorConfig AddAdminDevHapiAddress(this ApiClientGeneratorConfig apiClientGeneratorConfig)
        {
            apiClientGeneratorConfig.Url = Addresses.ADMIN_DEV_HAPI_ADDRESS;

            return apiClientGeneratorConfig;
        }

        public static ApiClientGeneratorConfig AddAdminDevHapiLegacyAddress(this ApiClientGeneratorConfig apiClientGeneratorConfig)
        {
            apiClientGeneratorConfig.Url = Addresses.ADMIN_DEV_HAPI_LEGACY_ADDRESS;

            return apiClientGeneratorConfig;
        }


    }
}
