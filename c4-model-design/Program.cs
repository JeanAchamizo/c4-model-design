using Structurizr;
using Structurizr.Api;
using System;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderModels();
        }

        static void RenderModels()
        {

            const long workspaceId = 77473;
            const string apiKey = "cad41e01-706e-4c0a-aebe-3a81a49f03ac";
            const string apiSecret = "0f67f441-4ce1-4c64-94ec-0791dbf73142";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);

            Workspace workspace = new Workspace("Software Design & Patterns - C4 Model - TakeMeHome", "Allows users to bring products from the US to Peru");

            ViewSet viewSet = workspace.Views;

            Model model = workspace.Model;

            Person traveler = model.AddPerson("Traveler", "A traveler with a registred account.");
            Person ImportCustomer = model.AddPerson("Customer", "A web app user with a registred account. ");
            Person admin = model.AddPerson("Administrator", "A web app administrator");

            // 1. Diagrama de Contexto
            SoftwareSystem ImportItSystem = model.AddSoftwareSystem("ImportIt System", "Allows users to bring.");
            SoftwareSystem gpsSystem = model.AddSoftwareSystem("GPS System", "Platform that offers a REST API of georeferential information.");
            SoftwareSystem PaymentSystem = model.AddSoftwareSystem("Payment Gateway System", "Allow the customers to make payments.");
            SoftwareSystem EmailSystem = model.AddSoftwareSystem("E-mail System", "The internal Microsoft Exchange  e-mail System.");

            traveler.Uses(ImportItSystem, "Search for products to import and offer her services");
            ImportCustomer.Uses(ImportItSystem, "Search for Tarvelers and select one to contract their services using");

            ImportItSystem.Uses(PaymentSystem, "Makes payment using");
            ImportItSystem.Uses(gpsSystem, "Uses the Google API");
            ImportItSystem.Uses(EmailSystem, "Send e-mail using");

            EmailSystem.Delivers(traveler, "Sends emails to");
            EmailSystem.Delivers(ImportCustomer, "Sends emails to");
            EmailSystem.Delivers(admin, "Sends emails to");

            // Tags
            traveler.AddTags("Traveler");
            ImportCustomer.AddTags("Customer");
            admin.AddTags("Admin");
            ImportItSystem.AddTags("SistemaMonitoreo");
            gpsSystem.AddTags("GoogleMaps");
            PaymentSystem.AddTags("PaymentSystem");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Traveler") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Customer") { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Admin") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("SistemaMonitoreo") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleMaps") { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("PaymentSystem") { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });

            SystemContextView contextView = viewSet.CreateSystemContextView(ImportItSystem, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // 2. Diagrama de Contenedores


            Container webApplication = ImportItSystem.AddContainer("Web App", "Allows users to view a dashboard with a summary of all product information.", "React");
            Container mobileApplication = ImportItSystem.AddContainer("mobile App", "Allows users to view a dashboard with a summary of all product information.", "React");
            Container landingPage = ImportItSystem.AddContainer("Landing Page", "", "React");
            Container apiRest = ImportItSystem.AddContainer("API REST", "API Rest", "NodeJS (NestJS) port 8080");

            Container identityContext = ImportItSystem.AddContainer("identity", "Allows customers, travelers and administrators to log in and log out of the account. It also handles history and permissions.", "NodeJS (NestJS)");
            Container reviewContext = ImportItSystem.AddContainer("Reviews", "Customer rates the Traveler", "NodeJS (NestJS)");
            Container messageContext = ImportItSystem.AddContainer("Messages", "Allows communication betweeen customers and travelers", "NodeJS (NestJS)");
            Container OrdersContext = ImportItSystem.AddContainer("Orders", "Allows customers to view the product details like locating them in real time", "NodeJS (NestJS)");
            Container PayOrderContext = ImportItSystem.AddContainer("Payments", "Allow customers to pay for pending orders", "JNodeJS (NestJS)");

            Container database = ImportItSystem.AddContainer("Database", "", "Oracle");

            traveler.Uses(webApplication, "Consulta");
            traveler.Uses(mobileApplication, "Consulta");
            traveler.Uses(landingPage, "Consulta");

            ImportCustomer.Uses(webApplication, "Consulta");
            ImportCustomer.Uses(mobileApplication, "Consulta");
            ImportCustomer.Uses(landingPage, "Consulta");

            admin.Uses(webApplication, "Consulta");
            admin.Uses(mobileApplication, "Consulta");
            admin.Uses(landingPage, "Consulta");

            webApplication.Uses(apiRest, "API Request", "JSON/HTTPS");
            mobileApplication.Uses(apiRest, "API Request", "JSON/HTTPS");

            apiRest.Uses(identityContext, "", "");
            apiRest.Uses(reviewContext, "", "");
            apiRest.Uses(messageContext, "", "");
            apiRest.Uses(OrdersContext, "", "");
            apiRest.Uses(PayOrderContext, "", "");


            identityContext.Uses(database, "", "");
            reviewContext.Uses(database, "", "");
            OrdersContext.Uses(database, "", "");
            OrdersContext.Uses(gpsSystem, "API Request", "JSON/HTTPS");
            messageContext.Uses(database, "", "");
            PayOrderContext.Uses(database, "", "");
            PayOrderContext.Uses(PaymentSystem, "API Request", "J");


            // Tags
            webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");
            apiRest.AddTags("APIRest");
            database.AddTags("Database");

            string contextTag = "Context";

            identityContext.AddTags(contextTag);
            reviewContext.AddTags(contextTag);
            messageContext.AddTags(contextTag);
            OrdersContext.AddTags(contextTag);
            PayOrderContext.AddTags(contextTag);


            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIRest") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle(contextTag) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

            ContainerView containerView = viewSet.CreateContainerView(ImportItSystem, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();

            // 3.1 Diagrama de Componentes (PayOrder Context)


            Component PaymentController = PayOrderContext.AddComponent("PaymentController", "Controls all transactions", "NodeJS (NestJS) REST Controller");
            Component OrderPaymentSystem = PayOrderContext.AddComponent("OrderPaymentSystem", "", "NestJS Component");
            Component PaymentHistoryObserver = PayOrderContext.AddComponent("PaymentHistoryObserver", "Saves the customer's payment history", "NestJS Component");
            Component PaymentPlataform = PayOrderContext.AddComponent("PaymentPlataform", "", "NestJS Component");
            Component PaymentExternaLogic = PayOrderContext.AddComponent("PaymentExternaLogic", "", "NestJS Component");
            Component paymentAplicationServer = PayOrderContext.AddComponent("paymentAplicationServer", "", "NestJS Component");


            apiRest.Uses(PaymentController, "", "JSON/HTTPS");
            PaymentController.Uses(OrderPaymentSystem, "Invoca métodos de monitoreo");

            OrderPaymentSystem.Uses(PaymentPlataform, "Uses", "");
            OrderPaymentSystem.Uses(PaymentExternaLogic, "Uses", "");

            paymentAplicationServer.Uses(PaymentSystem, "", "");

            PaymentExternaLogic.Uses(paymentAplicationServer, "Uses", "");
            PaymentExternaLogic.Uses(PaymentHistoryObserver, "Uses", "");

            PaymentPlataform.Uses(database, "", "");

            PaymentHistoryObserver.Uses(PaymentPlataform, "", "");

            // Tags

            PaymentController.AddTags("PaymentController");
            OrderPaymentSystem.AddTags("OrderPaymentSystem");
            PaymentHistoryObserver.AddTags("PaymentHistoryObserver");
            PaymentPlataform.AddTags("PaymentPlataform");
            PaymentExternaLogic.AddTags("PaymentExternaLogic");
            paymentAplicationServer.AddTags("paymentAplicationServer");


            styles.Add(new ElementStyle("PaymentController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("OrderPaymentSystem") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentHistoryObserver") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentPlataform") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentExternaLogic") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("paymentAplicationServer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentView = viewSet.CreateComponentView(PayOrderContext, "Components", "Component Diagram");

            componentView.PaperSize = PaperSize.A4_Landscape;
            componentView.Add(webApplication);
            componentView.Add(apiRest);
            componentView.Add(database);
            componentView.Add(PaymentSystem);
            componentView.Add(mobileApplication);
            componentView.AddAllComponents();




            // 3.2 Diagrama de Componentes (Order Context)

            Component productController = OrdersContext.AddComponent("productController", "Controls all transactions", "NodeJS (NestJS) REST Controller");
            Component SeachOrderSystem = OrdersContext.AddComponent("SeachOrderSystem", "Provee métodos para el monitoreo, pertenece a la capa Application de DDD", "NestJS Component");
            Component OrderPlataformRepository = OrdersContext.AddComponent("Order Platform Repository", "Save", "NestJS Component");
            Component aplicationServer = OrdersContext.AddComponent("aplicationServer", "", "");
            Component embeddebValue = OrdersContext.AddComponent("Embedded Value", "mapea las caracteristicas del producto", "NestJS Component");
            Component OrderOrder = OrdersContext.AddComponent("OrderProduct", "mapea los objetos valores del producto", "NestJS Component");




            apiRest.Uses(productController, "", "JSON/HTTPS");


            SeachOrderSystem.Uses(aplicationServer, "Uses");
            SeachOrderSystem.Uses(OrderPlataformRepository, "Uses", "");
            SeachOrderSystem.Uses(OrderPlataformRepository, "Uses", "");

            aplicationServer.Uses(OrderPlataformRepository, "Uses", "");
            aplicationServer.Uses(gpsSystem, "Uses", "");


            OrderPlataformRepository.Uses(database, "", "");

            productController.Uses(SeachOrderSystem, "", "JSON/HTTPS");
            productController.Uses(embeddebValue, "", "JSON/HTTPS");
            productController.Uses(OrderOrder, "", "JSON/HTTPS");

            embeddebValue.Uses(OrderPlataformRepository, "", "");

            OrderOrder.Uses(OrderPlataformRepository, "", "");
            // Tags

            SeachOrderSystem.AddTags("SeachOrderSystem");
            OrderPlataformRepository.AddTags("OrderPlataformRepository");
            productController.AddTags("productController");
            aplicationServer.AddTags("aplicationServer");
            embeddebValue.AddTags("EmbeddedValue");
            OrderOrder.AddTags("OrderOrder");



            styles.Add(new ElementStyle("productController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("SeachOrderSystem") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("OrderPlataformRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("aplicationServer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("EmbeddedValue") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("OrderOrder") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });


            ComponentView componentView1 = viewSet.CreateComponentView(OrdersContext, "Components1", "Component Diagram1");

            componentView1.PaperSize = PaperSize.A4_Landscape;
            componentView1.Add(webApplication);
            componentView1.Add(mobileApplication);
            componentView1.Add(apiRest);
            componentView1.Add(database);
            componentView1.Add(gpsSystem);
            componentView1.AddAllComponents();



            // 3.3 Diagrama de Componentes (Message Context)


            Component messageController = messageContext.AddComponent("messageController", "Controls all messages", "NodeJS (NestJS) REST Controller");
            Component messageSystem = messageContext.AddComponent("messageSystem", "", "NestJS Component");
            Component messageFacade = messageContext.AddComponent("messageFacade", "", "NestJS Component");
            Component messageAplicationServer = messageContext.AddComponent("messageAplicationServer", "", "NestJS Component");
            Component messageRepository = messageContext.AddComponent("messageRepository", "", "NestJS Component");

            Component messageDeppendingMappin = messageContext.AddComponent("messageDeppendingMappin", "", "NestJS Component");


            apiRest.Uses(messageController, "", "JSON/HTTPS");

            messageController.Uses(messageSystem, "Use");

            messageSystem.Uses(messageFacade, "", "");
            messageRepository.Uses(database, "", "");

            messageFacade.Uses(messageAplicationServer, "", "");
            messageFacade.Uses(messageDeppendingMappin, "", "");

            messageAplicationServer.Uses(EmailSystem, "", "");

            messageDeppendingMappin.Uses(messageRepository, "", "");

            // Tags

            messageController.AddTags("messageController");
            messageSystem.AddTags("messageSystem");
            messageFacade.AddTags("messageFacade");
            messageRepository.AddTags("messageRepository");
            messageAplicationServer.AddTags("messageAplicationServer");
            messageDeppendingMappin.AddTags("messageDeppendingMappin");


            styles.Add(new ElementStyle("messageController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("messageSystem") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("messageFacade") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("messageRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("messageAplicationServer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("messageDeppendingMappin") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentView2 = viewSet.CreateComponentView(messageContext, "Components2", "Component Diagram");

            componentView2.PaperSize = PaperSize.A4_Landscape;
            componentView2.Add(webApplication);
            componentView2.Add(apiRest);
            componentView2.Add(database);
            componentView2.Add(mobileApplication);
            componentView2.Add(EmailSystem);
            componentView2.AddAllComponents();


            // 3.4 Diagrama de Componentes (Review Context)


            Component reviewController = reviewContext.AddComponent("reviewController", "Controls all review", "NodeJS (NestJS) REST Controller");
            Component rewiewSystem = reviewContext.AddComponent("rewiewSystem", "-", "NestJS Component");
            Component reviewQualifyTraveler = reviewContext.AddComponent("reviewQualifyTraveler", "Saves the traveler's qualification", "NestJS Component");
            Component reviewAssociationTableMapping = reviewContext.AddComponent("reviewAssociationTableMapping", "mapping the traveler's qualification", "NestJS Component");
            Component reviewRepository = reviewContext.AddComponent("reviewRepository", "datas the traveler's", "NestJS Component");

            apiRest.Uses(reviewController, "", "JSON/HTTPS");
            reviewController.Uses(rewiewSystem, "");

            rewiewSystem.Uses(reviewQualifyTraveler, "", "");


            reviewQualifyTraveler.Uses(reviewAssociationTableMapping, "", "");
            reviewAssociationTableMapping.Uses(reviewRepository, "", "");

            reviewRepository.Uses(database, "", "");

            // Tags
            reviewController.AddTags("reviewController");
            rewiewSystem.AddTags("rewiewSystem");
            reviewQualifyTraveler.AddTags("reviewQualifyTraveler");
            reviewAssociationTableMapping.AddTags("reviewAssociationTableMapping");
            reviewRepository.AddTags("reviewRepository");

            styles.Add(new ElementStyle("reviewController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("rewiewSystem") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("reviewQualifyTraveler") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("reviewAssociationTableMapping") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("reviewRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentView3 = viewSet.CreateComponentView(reviewContext, "Components3", "Component Diagram");

            componentView3.PaperSize = PaperSize.A4_Landscape;
            componentView3.Add(webApplication);
            componentView3.Add(apiRest);
            componentView3.Add(database);
            componentView3.Add(mobileApplication);
            componentView3.AddAllComponents();



            // 3.5 Diagrama de Componentes (Authentication Context)


            Component identityController = identityContext.AddComponent("identityController", "Controla las cuentas de los usuarios", "NodeJS (NestJS) REST Controller");
            Component identitySystem = identityContext.AddComponent("identitySystem", "-", "NestJS Component");
            Component identitySignUp = identityContext.AddComponent("SignUp", "Crear cuenta", "NestJS Component");
            Component identityLogin = identityContext.AddComponent("Login", "-", "NestJS Component");
            Component identityModifyData = identityContext.AddComponent("Modify data", "Modifica los datos de los users", "NestJS Component");
            Component identityconcreteTable = identityContext.AddComponent("concreteTable", "Mappea los datos de los usuarios", "NestJS Component");
            Component identityRepository = identityContext.AddComponent("identityRepository", "Repositorio", "NestJS Component");
            Component identityServer = identityContext.AddComponent("identityServer", "-", "NestJS    Component");



            apiRest.Uses(identityController, "", "JSON/HTTPS");
            identityController.Uses(identitySystem, "Invoca métodos de monitoreo");


            identitySystem.Uses(identityLogin, "Uses");
            identitySystem.Uses(identitySignUp, "", "Uses");
            identitySystem.Uses(identityModifyData, "", "Uses");
            identitySystem.Uses(identityServer, "", "Uses");
            identitySystem.Uses(identityRepository, "", "Uses");


            identityModifyData.Uses(identityServer, "Valida y configura metodo de pago", "Uses");
            identityModifyData.Uses(identityRepository, "", "Uses");

            identitySignUp.Uses(identityconcreteTable, "", "");

            identityServer.Uses(PaymentSystem, "", "");

            identityconcreteTable.Uses(identityRepository, "", "");

            identityRepository.Uses(database, "", "");


            // Tags
            identityController.AddTags("identityController");
            identitySystem.AddTags("identitySystem");
            identitySignUp.AddTags("identitySignUp");
            identityLogin.AddTags("identityLogin");
            identityModifyData.AddTags("identityModifyData");
            identityconcreteTable.AddTags("identityconcreteTable");
            identityRepository.AddTags("identityRepository");
            identityServer.AddTags("identityServer");


            styles.Add(new ElementStyle("identityController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("identitySystem") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("identitySignUp") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("identityLogin") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("identityModifyData") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("identityconcreteTable") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("identityRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("identityServer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });


            ComponentView componentView4 = viewSet.CreateComponentView(identityContext, "Components4", "Component Diagram");

            componentView4.PaperSize = PaperSize.A4_Landscape;
            componentView4.Add(webApplication);
            componentView4.Add(apiRest);
            componentView4.Add(database);
            componentView4.Add(mobileApplication);
            componentView4.Add(PaymentSystem);
            componentView4.AddAllComponents();




            //------------------------------------------------------------
            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }





    }
}
