using System.Web.Http;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using SharePoint.OData.RestApi;

namespace SharePoint.OData.RestApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));


            config.MapODataServiceRoute(
                routeName: "oData",
                routePrefix: null,//"Archives",
                model: GetEdmModel(),
                defaultHandler: new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer)//DefaultHttpBatchHandler(GlobalConfiguration.DefaultServer)
            );

            config.EnsureInitialized();
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "SharePoint.OData.RestApi";
            builder.ContainerName = "DefaultContainer";
            builder.EntitySet<CaseModel>("CaseModels");
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }

    }
}
