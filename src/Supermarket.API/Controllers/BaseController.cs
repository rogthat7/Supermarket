using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Supermarket.API.Controllers
{
    public class BaseController : Controller
    {
        protected const string ERR_GENERALERROR = "GENERAL ERROR";
        protected const string ERR_NOTAUTHENTICATED = "NOT AUTHENTICATED";
        protected const string ERR_NOTAUTHORIZED = "NOT AUTHORIZED";
        protected const string ERR_NOTLIVE = "NOT LIVE";
        protected const string ERR_LIVEOVER = "LIVE OVER QUERY LIMIT";
        protected const string ERR_INVALID_INPUT = "ERROR: INVALID INPUT";
        protected const string ERR_INVALID_INPUT_ortID = "ERROR: INVALID INPUT- ortId is not valid";
        protected const string ERR_INVALID_CAT = "ERROR: INVALID CAT";
        protected const string ERR_INVALID_AUTH = "User not authenticated";
        protected const string ERR_EMPTY_TOKEN = "Token payload is missing ";
        protected const string ERR_MISSING_PERMISSION = "User not authorized for current operation";
        protected const string ERR_MISSING_EXTERNALKEY = "External key is missing";
        protected const string RECORD_UPDATED = "Object already Frozen , data not saved";
        protected const string RECORD_SAVED_SUCCESSFULLY = "Record Successfully Saved";
        protected const string RECORD_SAVE_ERROR = "Error Saving Record";
        public  IConfiguration _config;
        protected List<string> PermiCheckList = new List<string>();
        //protected bool saveDataEnabledFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["SaveObjectToDB"]);
        protected string clientIp;
        public BaseController(IConfiguration config)
        {
            this._config = config;
        }
        protected bool IsAuthorised(out string errorMessage)
        {
            errorMessage = null;
    // clientIp = Request.GetClientIp();
    //             var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
    //             if (principal == null)
    //             {
    //                 errorMessage = ERR_INVALID_AUTH;
    //                 return false;
    //             }

    //         var permList = principal.Claims.Where(c => c.Type == "appData");

    //         if (permList == null || permList.Count<Claim>() <= 0)
    //         {
    //             errorMessage = ERR_EMPTY_TOKEN;
    //             return false;
    //         }

    //         modelAuth.email = principal.Claims.First(c => c.Type.Contains("emailaddress")).Value;
    //         if (principal.Claims.Any(c => c.Type == ("customerName")))
    //             modelAuth.customerName = principal.Claims.First(c => c.Type.Contains("customerName")).Value;
    //         if (principal.Claims.Any(c => c.Type == ("userId")))
    //             modelAuth.userId = principal.Claims.First(c => c.Type.Contains("userId")).Value;
    //         if (principal.Claims.Any(c => c.Type == ("customerId")))
    //             modelAuth.customerId = principal.Claims.First(c => c.Type.Contains("customerId")).Value;

    //         //TODO: Check specific permission required
    //         foreach (var l2 in permList)
    //         {
    //             ModelAuth auth = JsonConvert.DeserializeObject<ModelAuth>(l2.Value);
    //             if (auth.modelR != null)
    //             {

    //                 if (!String.IsNullOrEmpty(auth.modelR.databaseName))
    //                 {
    //                     HttpContextHelper contextHelper = new HttpContextHelper();
    //                     contextHelper.setContext("dbName", auth.modelR.databaseName);

    //                 }
    //                 else
    //                 {
    //                     HttpContextHelper contextHelper = new HttpContextHelper();
    //                     contextHelper.setContext("dbName", "");
    //                 }

    //                 modelAuth.modelR = auth.modelR;
    //                 foreach (string per in PermiCheckList)
    //                 {
    //                     if (!Convert.ToBoolean(JObject.Parse(l2.Value).SelectToken(per)))
    //                     {
    //                         errorMessage = ERR_MISSING_PERMISSION;
    //                         return false;
    //                     }
    //                 }
    //                 errorMessage = string.Empty;
    //                 return true;
    //             }
    //         }

    //         errorMessage = ERR_MISSING_PERMISSION;
            return false;
        }
        #region Display for only api
        // [HttpGet]
        // [Route("")]
        // [ApiExplorerSettings(IgnoreApi = true)]
        // public async Task<IActionResult> Get()
        // {
        //     return Ok("IAZI Service.Models API " + _config["ApiVersion"]);
        // }
#endregion
        protected IActionResult ErrorAsync( IEnumerable<IdentityError> errors, string uri, object value = null)
        {
            var messagestr = "";
            
            if (value != null)
            {
                foreach(var error in  errors) {
                var input_value = Newtonsoft.Json.JsonConvert.SerializeObject(error.Description, Formatting.Indented);
                if (!string.IsNullOrEmpty(input_value.ToString()))
                    messagestr +=  Environment.NewLine + input_value.ToString();
                }
            }

            messagestr =  "ERROR INFO : " + messagestr;
            Trace.TraceError(messagestr);
            return BadRequest(messagestr);
        }

        // public List<IInput> AddExternalKeyIfNull(List<IInput> input)
        // {
        //     input.ForEach(x =>
        //     {
        //         x.ExternalKey = String.IsNullOrWhiteSpace(x.ExternalKey) ? Guid.NewGuid().ToString() : x.ExternalKey;
        //     });
        //     return input;
        // }

        // public bool CheckExternalKey(List<IInput> input)
        // {
        //     return input.Any(x => String.IsNullOrWhiteSpace(x.ExternalKey));
        // }

    }
    public static class Permission
    {
        //Internal
        public static string modeinternal { get { return "modelr.modeinternal"; } }
        public static string isloanadvisory { get { return "modelr.isloanadvisory"; } }
        //PPCH
        public static string PPCH_A1 { get { return "modelr.PPCH.A1"; } }
        public static string PPCH_A2 { get { return "modelr.PPCH.A2"; } }
        public static string PPCH_A3 { get { return "modelr.PPCH.A3"; } }
        public static string PPCH_A1caprate { get { return "modelr.PPCH.A1caprate"; } }
        public static string PPCH_A1Heuristic { get { return "modelr.PPCH.A1Heuristic"; } }

        //PPAT
        public static string PPAT_A2 { get { return "modelr.PPAT.A2"; } }
        public static string PPAT_A3 { get { return "modelr.PPAT.A3"; } }
        //RACH
        public static string RACH_FCF { get { return "modelr.RACH.FCF"; } }
        public static string RACH_GIR { get { return "modelr.RACH.GIR"; } }
        //PRCH
        public static string PRCH_R1 { get { return "modelr.PRCH.R1"; } }
        public static string PRCH_R3 { get { return "modelr.PRCH.R3"; } }
        public static string PRCH_R4 { get { return "modelr.PRCH.R4"; } }
        public static string PRCH_R5 { get { return "modelr.PRCH.R5"; } }
        //OPCH
        public static string OPCH_A2 { get { return "modelr.OPCH.A2"; } }
        public static string OPCH_A3 { get { return "modelr.OPCH.A3"; } }
        //ORCH
        public static string ORCH_Residential { get { return "modelr.ORCH.Residential"; } }
        public static string ORCH_Commercial { get { return "modelr.ORCH.Commercial"; } }
        //LPCH
        public static string LPCH_A2 { get { return "modelr.LPCH.A2"; } }
        public static string AGING { get { return "modelr.aging"; } }

        //OPDE
        public static string OPDE_A2 { get { return "modelr.OPDE.A2"; } }
        public static string OPDE_A3 { get { return "modelr.OPDE.A3"; } }



        //Insurance
        public static string InsuranceCH { get { return "modelr.GVCH.modebase"; } }

        //SharedPrice
        public static string sharedappraisal { get { return "modelr.sharedappraisal"; } }


        //Construction Price
        public static string CPCH_A2 { get { return "modelr.CPCH.A2"; } }

    }
}