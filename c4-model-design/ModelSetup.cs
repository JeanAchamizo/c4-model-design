using Structurizr;

namespace MIAM_C4_Model
{
    public static class ModelSetup
    {
        public static void ConfigureModel(Model model, out DeploymentNode webServer, out DeploymentNode apiServer, out DeploymentNode dbServerPrimary, out DeploymentNode dbServerSecondary, out DeploymentNode userBrowser, out DeploymentNode iotDevice, out DeploymentNode mobileDevice)
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

            // Definir contenedores del sistema MIAM
            Container webApp = miam.AddContainer("Web Application", "Delivers the static content and the SPA", "Javascript / HTML / CSS");
            Container singlePageApp = miam.AddContainer("Single-Page Application", "Provides the functionality to visualize alerts, health information, and configure other devices", "JavaScript / React");
            Container mobileApp = miam.AddContainer("Mobile Application", "Visualize alerts and health information", "iOS / Android");
            Container webApi = miam.AddContainer("Web API", "Handles requests from the SPA and mobile app, provides data access and business logic", "Java / Spring Boot");
            Container edgeApi = miam.AddContainer("Edge API", "Interacts with IoT devices and manages local data storage", "Java / Spring Boot");
            Container iotApp = miam.AddContainer("IoT Application", "Manages communication with and data collection from IoT devices", "C++");
            Container cloudDatabase = miam.AddContainer("Cloud Database", "Stores application data in the cloud", "MySQL");
            Container flashMemoryDatabase = miam.AddContainer("Flash Memory Database", "Stores local data for quick access and offline use", "Embedded Database");

            // Relación de los usuarios con las aplicaciones
            caregiver.Uses(webApp, "Visit");
            caregiver.Uses(mobileApp, "Uses");
            caregiver.Uses(singlePageApp, "Uses");
            owner.Uses(webApp, "Visit");
            owner.Uses(mobileApp, "Uses");
            owner.Uses(singlePageApp, "Uses");

            // Relación de aplicaciones con contenedores
            webApp.Uses(singlePageApp, "Delivers");
            singlePageApp.Uses(webApi, "Consults");
            mobileApp.Uses(webApi, "Consults");
            webApi.Uses(edgeApi, "Consults");
            edgeApi.Uses(webApi, "Communicates with");

            // Relación de APIs con bases de datos
            webApi.Uses(cloudDatabase, "Stores data in");
            edgeApi.Uses(flashMemoryDatabase, "Stores data in");

            // Relación de APIs con servicios externos
            webApi.Uses(googleApi, "Uses for login with Google services");
            webApi.Uses(paypal, "Uses for payment processing");

            // Relación de Edge API con IoT Application
            iotApp.Uses(edgeApi, "Consults");
            edgeApi.Uses(iotApp, "Communicates with");

            // Agregar los componentes del Web API
            Component notificationService = webApi.AddComponent("NotificationService", "Manages notifications for users", "Spring Boot Service");
            Component caregiverService = webApi.AddComponent("CaregiverService", "Manages caregiver information", "Spring Boot Service");
            Component accountService = webApi.AddComponent("AccountService", "Handles user accounts", "Spring Boot Service");
            Component paymentService = webApi.AddComponent("PaymentService", "Handles payment transactions", "Spring Boot Service");
            Component subscriptionService = webApi.AddComponent("SubscriptionService", "Manages subscriptions and plans", "Spring Boot Service");
            Component planService = webApi.AddComponent("PlanService", "Manages the different service plans", "Spring Boot Service");
            Component roleService = webApi.AddComponent("RoleService", "Manages user roles", "Spring Boot Service");

            // Asignar etiquetas a los componentes del Web API
            notificationService.AddTags("WebAPIComponent");
            caregiverService.AddTags("WebAPIComponent");
            accountService.AddTags("WebAPIComponent");
            paymentService.AddTags("WebAPIComponent");
            subscriptionService.AddTags("WebAPIComponent");
            planService.AddTags("WebAPIComponent");
            roleService.AddTags("WebAPIComponent");

            // Agregar los componentes del Edge API
            Component deviceServiceEdge = edgeApi.AddComponent("DeviceService", "Manages IoT device data and configurations", "Spring Boot Service");
            Component iotBandService = edgeApi.AddComponent("IoTBandService", "Manages IoT band data and operations", "Spring Boot Service");
            Component configurationServiceEdge = edgeApi.AddComponent("ConfigurationService", "Handles device configurations", "Spring Boot Service");
            Component metricsServiceEdge = edgeApi.AddComponent("MetricsService", "Monitors and analyzes real-time metrics", "Spring Boot Service");
            Component patientServiceEdge = edgeApi.AddComponent("PatientService", "Handles patient information", "Spring Boot Service");

            // Asignar etiquetas a los componentes del Edge API
            deviceServiceEdge.AddTags("EdgeAPIComponent");
            iotBandService.AddTags("EdgeAPIComponent");
            configurationServiceEdge.AddTags("EdgeAPIComponent");
            metricsServiceEdge.AddTags("EdgeAPIComponent");
            patientServiceEdge.AddTags("EdgeAPIComponent");

            // Asignar etiquetas a los contenedores
            webApp.AddTags("WebApp");
            singlePageApp.AddTags("SPA");
            mobileApp.AddTags("MobileApp");
            webApi.AddTags("WebAPI");
            edgeApi.AddTags("EdgeAPI");
            iotApp.AddTags("IoT");
            cloudDatabase.AddTags("CloudDB");
            flashMemoryDatabase.AddTags("LocalDB");

            // Definir nodos de despliegue y almacenar las referencias
            webServer = model.AddDeploymentNode("Cloud Web Server", "Apache Tomcat server for hosting web applications", "Ubuntu 16.04 LTS")
                                             .AddDeploymentNode("Apache Tomcat", "Apache Tomcat 8.x", "Apache Tomcat");
            apiServer = model.AddDeploymentNode("Cloud API Server", "Apache Tomcat server for hosting API applications", "Ubuntu 16.04 LTS")
                                             .AddDeploymentNode("Apache Tomcat", "Apache Tomcat 8.x", "Apache Tomcat");
            dbServerPrimary = model.AddDeploymentNode("Primary Database Server", "Primary MySQL database server for user data", "MySQL 8.x")
                                                  .AddDeploymentNode("mysql-db01", "Primary database node", "Ubuntu 16.04 LTS");
            dbServerSecondary = model.AddDeploymentNode("Secondary Database Server", "Secondary MySQL database server for user data replication", "MySQL 8.x")
                                                     .AddDeploymentNode("mysql-db02", "Secondary database node", "Ubuntu 16.04 LTS");

            // Despliegue en dispositivos del usuario
            userBrowser = model.AddDeploymentNode("User's Web Browser", "User's web browser for running SPA", "Google Chrome / Firefox / Safari / Edge");
            iotDevice = model.AddDeploymentNode("IoT Device", "Smart bands used by elderly patients", "Custom IoT OS");
            mobileDevice = model.AddDeploymentNode("User's Mobile Device", "Mobile device running the mobile application", "iOS / Android");

            // Desplegar contenedores en los nodos correspondientes
            webServer.Add(webApp);
            userBrowser.Add(singlePageApp);
            mobileDevice.Add(mobileApp);
            apiServer.Add(webApi);
            apiServer.Add(edgeApi);
            dbServerPrimary.Add(cloudDatabase);
            dbServerSecondary.Add(flashMemoryDatabase);
            iotDevice.Add(iotApp);
        }
    }
}
