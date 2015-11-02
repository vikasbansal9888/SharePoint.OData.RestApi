using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.OData;

namespace SharePoint.OData.RestApi
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using SharePoint.OData.RestApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<CaseModel>("CaseModels");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    //[EnableQuery]
    public class CaseModelsController : ODataController
    {
        //private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        //// GET: odata/CaseModels
        //public IHttpActionResult GetCaseModels(ODataQueryOptions<CaseModel> queryOptions)
        //{
        //    // validate the query.
        //    try
        //    {
        //        queryOptions.Validate(_validationSettings);
        //    }
        //    catch (ODataException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    // return Ok<IEnumerable<CaseModel>>(caseModels);
        //    return StatusCode(HttpStatusCode.NotImplemented);
        //}

        //// GET: odata/CaseModels(5)
        //public IHttpActionResult GetCaseModel([FromODataUri] int key, ODataQueryOptions<CaseModel> queryOptions)
        //{
        //    // validate the query.
        //    try
        //    {
        //        queryOptions.Validate(_validationSettings);
        //    }
        //    catch (ODataException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    // return Ok<CaseModel>(caseModel);
        //    return StatusCode(HttpStatusCode.NotImplemented);
        //}

        //// PUT: odata/CaseModels(5)
        //public IHttpActionResult Put([FromODataUri] int key, Delta<CaseModel> delta)
        //{
        //    Validate(delta.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // TODO: Get the entity here.

        //    // delta.Put(caseModel);

        //    // TODO: Save the patched entity.

        //    // return Updated(caseModel);
        //    return StatusCode(HttpStatusCode.NotImplemented);
        //}

        //// POST: odata/CaseModels
        //public IHttpActionResult Post(CaseModel caseModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // TODO: Add create logic here.

        //    // return Created(caseModel);
        //    return StatusCode(HttpStatusCode.NotImplemented);
        //}

        //// PATCH: odata/CaseModels(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public IHttpActionResult Patch([FromODataUri] int key, Delta<CaseModel> delta)
        //{
        //    Validate(delta.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // TODO: Get the entity here.

        //    // delta.Patch(caseModel);

        //    // TODO: Save the patched entity.

        //    // return Updated(caseModel);
        //    return StatusCode(HttpStatusCode.NotImplemented);
        //}

        //// DELETE: odata/CaseModels(5)
        //public IHttpActionResult Delete([FromODataUri] int key)
        //{
        //    // TODO: Add delete logic here.

        //    // return StatusCode(HttpStatusCode.NoContent);
        //    return StatusCode(HttpStatusCode.NotImplemented);
        //}

        public IHttpActionResult Get()//string caseId
        {
            string caseId = "10001";
            string baseSiteUrl = Convert.ToString(ConfigurationManager.AppSettings["BaseSiteUrl"]),
                   user = Convert.ToString(ConfigurationManager.AppSettings["UserName"]),
                   password = Convert.ToString(ConfigurationManager.AppSettings["Password"]),
                   domain = Convert.ToString(ConfigurationManager.AppSettings["Domain"]);

            JToken json = JObject.Parse(DataSource.GetCaseFolderDetails(caseId, baseSiteUrl, user, password, domain));
            return Ok(json);

            //return Ok(DataSource.MySource().AsQueryable());
        }
    }
}