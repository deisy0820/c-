using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.OAuth;
using Owin;


namespace WMarket
{
    public partial class Startup
    {
        // Para obtener más información para configurar la autenticación, visite http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Permitir que la aplicación use una cookie para almacenar información para el usuario que inicia sesión
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Quitar los comentarios de las siguientes líneas para habilitar el inicio de sesión con proveedores de inicio de sesión de terceros
            app.UseMicrosoftAccountAuthentication(
                clientId: "0000000044162231",
                clientSecret: "Pr89mbtldzmNt2sspuymKzraD1MkTX6v");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(
               appId: "936176763095550",
               appSecret: "421bd5231d51ec3466ba7d191764d658");

            //app.UseGoogleAuthentication();
        }
    }
}