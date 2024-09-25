using Structurizr;

namespace MIAM_C4_Model
{
    public static class ModelSetup
    {
        public static void ConfigureModel(Model model)
        {
            // Definición de Personas
            Person caregiver = model.AddPerson("Caregiver", "A person who takes care of elderly patients.");
            Person owner = model.AddPerson("Nursing Home Owner", "The owner of the nursing home.");

            // Definición del sistema de software principal
            SoftwareSystem miam = model.AddSoftwareSystem("MIAM", "Smart IoT Monitoring System for elderly patients.");
            caregiver.Uses(miam, "Uses");
            owner.Uses(miam, "Uses");

            // Sistemas externos
            SoftwareSystem googleApi = model.AddSoftwareSystem("Google API", "External API provided by Google that integrates login with email.");
            SoftwareSystem paypal = model.AddSoftwareSystem("Paypal", "Payment gateway system used for processing transactions.");

            miam.Uses(googleApi, "Uses for login");
            miam.Uses(paypal, "Uses for processing payments");

            // Definir los contenedores del sistema MIAM
            Container webApp = miam.AddContainer("Web Application", "Delivers the static content and the SPA", "Javascript / HTML / CSS");
            Container singlePageApp = miam.AddContainer("Single-Page Application", "Provides the functionality to visualize alerts, health information, and configure other devices", "JavaScript / React");
            Container mobileApp = miam.AddContainer("Mobile Application", "Visualize alerts and health information", "iOS / Android");
            Container webApi = miam.AddContainer("Web API", "Handles requests from the SPA and mobile app, provides data access and business logic", "Java / Spring Boot");
            Container iotApp = miam.AddContainer("IoT Application", "Manages communication with and data collection from IoT devices", "C++");
            Container edgeApi = miam.AddContainer("Edge API", "Interacts with IoT devices and manages local data storage", "Java / Spring Boot");
            Container cloudDatabase = miam.AddContainer("Cloud Database", "Stores application data in the cloud", "SQL Database");
            Container flashMemoryDatabase = miam.AddContainer("Flash Memory Database", "Stores local data for quick access and offline use", "Embedded Database");

            // Asignar etiquetas (tags) a los contenedores
            webApp.AddTags("WebApp");
            singlePageApp.AddTags("SPA");
            mobileApp.AddTags("MobileApp");
            webApi.AddTags("API");
            iotApp.AddTags("IoT");
            edgeApi.AddTags("EdgeAPI");
            cloudDatabase.AddTags("CloudDB");
            flashMemoryDatabase.AddTags("LocalDB");

            // Relaciones entre contenedores
            caregiver.Uses(webApp, "Uses");
            caregiver.Uses(mobileApp, "Uses");
            owner.Uses(webApp, "Uses");
            owner.Uses(mobileApp, "Uses");

            webApp.Uses(singlePageApp, "Delivers");
            singlePageApp.Uses(webApi, "Consults");
            mobileApp.Uses(webApi, "Consults");

            webApi.Uses(cloudDatabase, "Stores data in");
            webApi.Uses(edgeApi, "Consults");

            iotApp.Uses(edgeApi, "Consults");
            edgeApi.Uses(flashMemoryDatabase, "Stores data in");

            webApi.Uses(googleApi, "Uses for login with Google services");
            webApi.Uses(paypal, "Uses for payment processing");
        }
    }
}
