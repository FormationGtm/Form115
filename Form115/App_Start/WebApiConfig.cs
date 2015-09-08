namespace Form115 {
    #region UsingReg

    using System.Web.Http;
    using Microsoft.Owin.Security.OAuth;

    #endregion

    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            // Configuration et services de l'API Web
            // Configurer l'API Web pour utiliser uniquement l'authentification de jeton du porteur.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }
                );
        }
    }
}