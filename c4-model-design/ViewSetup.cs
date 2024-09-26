using Structurizr;

namespace MIAM_C4_Model
{
    public static class ViewSetup
    {
        public static void ConfigureViews(Workspace workspace, DeploymentNode webServer, DeploymentNode apiServer, DeploymentNode dbServerPrimary, DeploymentNode dbServerSecondary, DeploymentNode userBrowser, DeploymentNode iotDevice, DeploymentNode mobileDevice)
        {
            ViewSet viewSet = workspace.Views;
            SoftwareSystem miam = workspace.Model.GetSoftwareSystemWithName("MIAM");

            // Crear la vista de contexto general
            SystemContextView contextView = viewSet.CreateSystemContextView(miam, "SystemContext", "System context diagram for MIAM.");
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();
            contextView.PaperSize = PaperSize.A4_Landscape;

            // Crear la vista de contenedores general
            ContainerView containerView = viewSet.CreateContainerView(miam, "ContainerView", "Container diagram for MIAM.");
            containerView.AddAllElements();
            containerView.PaperSize = PaperSize.A4_Landscape;

            // Crear vista de despliegue usando las referencias a los nodos de despliegue
            DeploymentView deploymentView = viewSet.CreateDeploymentView(miam, "DeploymentView", "Deployment diagram for MIAM.");
            deploymentView.Add(webServer);
            deploymentView.Add(apiServer);
            deploymentView.Add(dbServerPrimary);
            deploymentView.Add(dbServerSecondary);
            deploymentView.Add(userBrowser);
            deploymentView.Add(iotDevice);
            deploymentView.Add(mobileDevice);
            deploymentView.PaperSize = PaperSize.A3_Landscape;

            // Estilos para el diagrama de despliegue
            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle(Tags.SoftwareSystem) { Background = "#1168bd", Color = "#ffffff" });
            styles.Add(new ElementStyle(Tags.Person) { Shape = Shape.Person, Background = "#08427b", Color = "#ffffff" });
            styles.Add(new ElementStyle("Google API") { Background = "#f4c542", Color = "#000000" });
            styles.Add(new ElementStyle("Paypal") { Background = "#003087", Color = "#ffffff" });

            // Estilos para los contenedores
            styles.Add(new ElementStyle("WebAPI") { Background = "#2596be", Color = "#ffffff" }); // Web API
            styles.Add(new ElementStyle("EdgeAPI") { Background = "#2596be", Color = "#ffffff" }); // Edge API
            styles.Add(new ElementStyle("WebApp") { Background = "#2596be", Color = "#000000", Shape = Shape.WebBrowser }); // Web Application
            styles.Add(new ElementStyle("SPA") { Background = "#2596be", Color = "#000000", Shape = Shape.WebBrowser }); // Single-Page Application
            styles.Add(new ElementStyle("MobileApp") { Background = "#2596be", Color = "#000000", Shape = Shape.MobileDevicePortrait }); // Mobile Application
            styles.Add(new ElementStyle("IoT") { Background = "#2596be", Color = "#000000" }); // IoT Application
            styles.Add(new ElementStyle("CloudDB") { Background = "#2596be", Color = "#ffffff", Shape = Shape.Cylinder }); // Cloud Database
            styles.Add(new ElementStyle("LocalDB") { Background = "#2596be", Color = "#ffffff", Shape = Shape.Cylinder }); // Flash Memory Database

            // Estilos para los componentes del Web API
            styles.Add(new ElementStyle("WebAPIComponent") { Background = "#85BB65", Color = "#000000", Shape = Shape.Component });

            // Estilos para los componentes del Edge API
            styles.Add(new ElementStyle("EdgeAPIComponent") { Background = "#D2691E", Color = "#000000", Shape = Shape.Component });

            // Estilos para nodos de despliegue
            styles.Add(new ElementStyle("Cloud Web Server") { Background = "#87CEEB", Color = "#000000", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Cloud API Server") { Background = "#FFD700", Color = "#000000", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Primary Database Server") { Background = "#FFA500", Color = "#000000", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Secondary Database Server") { Background = "#CD5C5C", Color = "#000000", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("User's Web Browser") { Background = "#C0C0C0", Color = "#000000", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("IoT Device") { Background = "#8A2BE2", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("User's Mobile Device") { Background = "#3CB371", Color = "#ffffff", Shape = Shape.RoundedBox });
        }
    }
}
