using System.ComponentModel;

namespace GoogleGeocoderService
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : System.Configuration.Install.Installer
    {
        public ServiceInstaller()
        {
            // Execute the stuff from designer.cs
            InitializeComponent();

            // override defaults from designer.cs and configure the name of the windows service.
            Installer.ServiceName = AppConfig.ApplicationName;
        }
    }
}
