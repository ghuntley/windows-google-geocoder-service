using System.ServiceProcess;

namespace GoogleGeocoderService
{
    partial class ServiceInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.Installer = new System.ServiceProcess.ServiceInstaller();
            // 
            // ProcessInstaller
            // 
            this.ProcessInstaller.Password = null;
            this.ProcessInstaller.Username = null;
            // 
            // Installer
            // 
            this.Installer.ServiceName = "ServiceName";
            this.Installer.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ProcessInstaller,
            this.Installer});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ProcessInstaller;
        private System.ServiceProcess.ServiceInstaller Installer;
    }
}