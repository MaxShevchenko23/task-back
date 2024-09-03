using url_shortener_server.Helpers;

var builder = WebApplication.CreateBuilder(args);

ServicesInjector.Inject(builder.Services, builder.Configuration);

var app = builder.Build();

AppBuilder.Build(app);

