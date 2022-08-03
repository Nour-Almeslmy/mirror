using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BusinessLogicLayer.getEligibleServiceClassesMigration;
using BusinessLogicLayer.CustomFilters;
using DataAccessLayer.DTOs;
using BusinessLogicLayer.ControllerHelper.ProfileController;
using System.Threading;
using bucketSubs.service.OAuth;

namespace bucketSubs.service.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProfileController : ApiController
    {

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IProfileControllerHelper profileControllerHelper_;
        public ProfileController(IProfileControllerHelper profileControllerHelper)
        {
            profileControllerHelper_ = profileControllerHelper;
        }

        [ValidDial]
        //[BasicAuthentication]  //replaced with a basic authentication message handler
        [CustomAuthorize(Roles ="user")]
        [HttpGet, ResponseType(typeof(ProfileStatusServiceResponseDTO))]
        [Route("getProfileStatus/{dial_}")]
        public IHttpActionResult GetDataProfileStatus(string dial_)
        {
            //var username = Thread.CurrentPrincipal.Identity.Name;

            var isMock = checkMocking();

            if (isMock)
            {
                try
                {
                    var secureServiceResponse = profileControllerHelper_.GetProfileStatusSecureServiceResponse(dial_, true);

                    if (secureServiceResponse == null)
                        return NotFound();
                    else
                        return Ok(secureServiceResponse);
                }
                catch (Exception e)
                {
                    log.Warn(e.Message);
                    return InternalServerError();
                }
            }
            else
            {
                try
                {
                    var secureServiceResponse = profileControllerHelper_.GetProfileStatusSecureServiceResponse(dial_, false);

                    return Ok(secureServiceResponse);
                }
                catch (Exception e)
                {
                    log.Warn(e.Message);
                    return InternalServerError();
                }
            }
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //[LogException]
        [ValidDial]
        [HttpGet, ResponseType(typeof(getEligibleServiceClassesMigration_Output))]
        [Route("ClassesMigration/{dial_}")]
        public IHttpActionResult GetEligibleServiceClassesMigration(string dial_)
        {
            var isMock = checkMocking();

            if (isMock)
            {
                try
                {
                    var EligibleServiceClassesMigrationResponse =
                        profileControllerHelper_.GetClassesMigrationResponse(dial_, true);

                    if (EligibleServiceClassesMigrationResponse == null)
                        return NotFound();
                    else
                        return Ok(EligibleServiceClassesMigrationResponse);
                }
                catch (Exception e)
                {
                    log.Warn(e.Message);
                    return InternalServerError();
                }
            }
            else
            {
                try
                {
                    var EligibleServiceClassesMigration_Out =
                        profileControllerHelper_.GetClassesMigrationResponse(dial_, false);

                    return Ok(EligibleServiceClassesMigration_Out);
                }
                catch (Exception e)
                {
                    log.Warn(e.Message);
                    return InternalServerError();
                }
            }
        }


        private bool checkMocking()
        {
            var mock = ConfigurationManager.AppSettings["mocking"];

            bool mockParseresult = Boolean.TryParse(mock, out bool mockFlag);

            return mockParseresult && mockFlag;
        }
    }
}
