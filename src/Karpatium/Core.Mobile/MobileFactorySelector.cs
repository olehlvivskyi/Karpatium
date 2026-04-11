using Karpatium.Core.Mobile.Exceptions;
using Karpatium.Core.Mobile.MobileFactories;

namespace Karpatium.Core.Mobile;

internal static class MobileFactory
{
    private static readonly Dictionary<MobilePlatform, IMobileFactory> Factories = new()
    {
        { MobilePlatform.Android, new AndroidMobileFactory() },
        { MobilePlatform.Ios, new IosMobileFactory() }
    };

    internal static IMobileFactory Get(IMobileSettings mobileSettings)
    {
        if (!Factories.TryGetValue(mobileSettings.MobilePlatform, out var factory))
        {
            throw new MobileFactoryNotImplementedException(mobileSettings.MobilePlatform);
        }

        return factory;
    }
}