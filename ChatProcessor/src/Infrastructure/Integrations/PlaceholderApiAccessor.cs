using PlaceholderAPI.Contract;

namespace ChatProcessor.Infrastructure.Integrations;


internal class PlaceholderApiAccessor
{
    public IPlaceholderAPIv1? Api { get; private set; }

    public void Set(IPlaceholderAPIv1 api)
    {
        Api = api;
    }
}