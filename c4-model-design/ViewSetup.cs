using Structurizr;

namespace MIAM_C4_Model
{
    public static class ViewSetup
    {
        public static void ConfigureViews(Workspace workspace)
        {
            ViewSet viewSet = workspace.Views;

            SoftwareSystem miam = workspace.Model.GetSoftwareSystemWithName("MIAM");

            // Crear la vista de contexto
            SystemContextView contextView = viewSet.CreateSystemContextView(miam, "SystemContext", "System context diagram for MIAM.");
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();
            contextView.PaperSize = PaperSize.A4_Landscape;

            // Crear la vista de contenedores
            ContainerView containerView = viewSet.CreateContainerView(miam, "ContainerView", "Container diagram for MIAM.");
            containerView.AddAllElements();
            containerView.PaperSize = PaperSize.A4_Landscape;

            // Estilos para el diagrama de contexto
            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle(Tags.SoftwareSystem) { Background = "#1168bd", Color = "#ffffff" });
            styles.Add(new ElementStyle(Tags.Person) { Shape = Shape.Person, Background = "#08427b", Color = "#ffffff" });
            styles.Add(new ElementStyle("Google API") { Background = "#a09c9c", Color = "#000000" });
            styles.Add(new ElementStyle("Paypal") { Background = "#a09c9c", Color = "#ffffff" });

            // Estilos para el diagrama de contenedores, aplicando las etiquetas (tags)
            styles.Add(new ElementStyle("WebApp") { Background = "#488cd4", Color = "#000000", Shape = Shape.WebBrowser });
            styles.Add(new ElementStyle("SPA") { Background = "#488cd4", Color = "#000000", Shape = Shape.WebBrowser });
            styles.Add(new ElementStyle("MobileApp") { Background = "#488cd4", Color = "#000000", Shape = Shape.MobileDevicePortrait });
            styles.Add(new ElementStyle("API") { Background = "#488cd4", Color = "#000000"});
            styles.Add(new ElementStyle("IoT") { Background = "#488cd4", Color = "#000000" });
            styles.Add(new ElementStyle("EdgeAPI") { Background = "#488cd4", Color = "#000000"});
            styles.Add(new ElementStyle("CloudDB") { Background = "#488cd4", Color = "#ffffff", Shape = Shape.Cylinder });
            styles.Add(new ElementStyle("LocalDB") { Background = "#488cd4", Color = "#ffffff", Shape = Shape.Cylinder });
        }
    }
}
