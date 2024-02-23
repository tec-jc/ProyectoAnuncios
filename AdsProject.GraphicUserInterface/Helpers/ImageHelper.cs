using Firebase.Auth;
using Firebase.Storage;
using System.Security.Cryptography;

namespace AdsProject.GraphicUserInterface.Helpers
{
    public class ImageHelper
    {
        public static async Task<string> SubirArchivo(Stream archivo, string nombre)
        {
            string fileName = GetRandomNumber().ToString() + nombre;
            string email = "proyectoanuncios@gmail.com";
            string clave = "proyectoanunciospass";
            string ruta = "proyectodeanuncios.appspot.com";
            string api_key = "AIzaSyBAsRIrBYmu6RnkODuJ5hd9uIZ9Zs73Jn4";

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("adds_images")
                .Child(fileName)

                .PutAsync(archivo, cancellation.Token);

            try
            {
                var downloadURL = await task;
                return downloadURL;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "";
        }

        static int GetRandomNumber()
        {
            using (RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[4];
                rngCrypt.GetBytes(tokenBuffer);
                return BitConverter.ToInt32(tokenBuffer, 0);
            }
        }
    }
}
