using AncoraMVVM.Phone.Sample.Resources;

namespace AncoraMVVM.Phone.Sample
{
    /// <summary>
    /// Proporciona acceso a los recursos de cadena.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return localizedResources; } }
    }
}