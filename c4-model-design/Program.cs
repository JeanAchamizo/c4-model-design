using Structurizr;
using Structurizr.Api;

namespace MIAM_C4_Model
{
    class Program
    {
        static void Main(string[] args)
        {
            const long workspaceId = 95614; // ID del workspace
            const string apiKey = "b0de7917-876a-4f0a-923b-20d1f6327b27"; // API Key
            const string apiSecret = "4d745655-b3f5-4db9-b0da-db7c1af9af0f"; // API Secret

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);

            // Crear el workspace
            Workspace workspace = new Workspace("MIAM - C4 Model", "Smart IoT Monitoring System for elderly patients.");

            // Variables para almacenar referencias de nodos de despliegue
            DeploymentNode webServer, apiServer, dbServerPrimary, dbServerSecondary, userBrowser, iotDevice, mobileDevice;

            // Configurar el modelo y obtener las referencias de nodos de despliegue
            ModelSetup.ConfigureModel(workspace.Model, out webServer, out apiServer, out dbServerPrimary, out dbServerSecondary, out userBrowser, out iotDevice, out mobileDevice);

            // Configurar las vistas pasando las referencias de nodos de despliegue
            ViewSetup.ConfigureViews(workspace, webServer, apiServer, dbServerPrimary, dbServerSecondary, userBrowser, iotDevice, mobileDevice);

            // Subir el workspace a Structurizr
            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}
