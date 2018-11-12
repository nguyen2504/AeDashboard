using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AeDashboard.Localization
{
    public static class AeDashboardLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AeDashboardConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AeDashboardLocalizationConfigurer).GetAssembly(),
                        "AeDashboard.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
