using Microsoft.AspNetCore.Builder;

namespace url_shortener_server.Helpers
{
    public class AppBuilder
    {
        public static void Build(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("AllowAllOrigins");

            app.Run();
        }
    }
}
