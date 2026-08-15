namespace web_api.Core;

internal class MiniWebAppllicationBuilder
{
    public MiniWebAppllication Build()
    {
        return new MiniWebAppllication();
    }
}

internal class WebApplicationFactory
{
    public static MiniWebAppllicationBuilder CreateBuilder()
    {
        return new MiniWebAppllicationBuilder();
    }
}