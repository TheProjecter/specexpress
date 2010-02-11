<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        var path = System.AppDomain.CurrentDomain.BaseDirectory + "//bin//SpecExpress.Quickstart.Domain.dll";

        System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(path);
        SpecExpress.ValidationCatalog.Scan(x => x.AddAssembly(assembly));

        //Check specifications are valid
        SpecExpress.ValidationCatalog.AssertConfigurationIsValid();

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
