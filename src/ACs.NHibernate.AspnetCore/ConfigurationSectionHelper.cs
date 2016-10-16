using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ACs.NHibernate.AspnetCore
{
    public static  class ConfigurationSectionHelper
    {
        public static IDictionary<string, string> ToDictionary(this IConfigurationSection configurationSection)
        {
            var nhibernateConfiguration = new Dictionary<string, string>();
		
			configurationSection.Bind(nhibernateConfiguration);
            return nhibernateConfiguration;
        }

    }
}
