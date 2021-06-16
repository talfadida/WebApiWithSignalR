using Guardium.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardium.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaintController : ControllerBase
    {
        private readonly ILogger<PaintController> _logger;
        private readonly IPageManager _pageMgr;
        private readonly IAppManager _gApp;
        private readonly IUserManager _userMgr;

        public PaintController(ILogger<PaintController> logger, IAppManager gApp, IUserManager userMgr, IPageManager pageMgr)
        {
            _gApp = gApp;            
            _userMgr = userMgr;
            _logger = logger;
            _pageMgr = pageMgr;
        }              
 

        [HttpGet("all")]
        public IEnumerable<ElementContent> Get([FromQuery]string uuid)
        {
            if (string.IsNullOrEmpty(uuid)) return new ElementContent[0];
            var page = _gApp.CreateOrGetExistingPage(_userMgr.GetCurrent(), uuid);
            //return the page's elements for being draw by the UI engine 
            return page.GetStringElementsForUi();
            
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            _gApp.ResetApp();
            return Ok();

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddElement([FromQuery] string uuid, [FromBody]ElementContent elementContent)
        {
            try
            {
                if (string.IsNullOrEmpty(uuid))
                    throw new Exception("PageUUID not provided");
                if (elementContent == null)
                    throw new Exception("elementContent is empty");


                var user = _userMgr.GetCurrent();
                var page = _gApp.CreateOrGetExistingPage(user, uuid);

                await _pageMgr.AddElement(page, user, elementContent);
                _logger.LogInformation($"Element added to page {uuid}");
                return Ok();
            }
            catch (PageManagerException pex)
            {
                _logger.LogError($"PageManagerException happened for AddElement on page {uuid}: {pex}");
                return  StatusCode(500,pex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception happened  for AddElement on page {uuid}: {ex}");
                return BadRequest(ex.Message);
            }           
            
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteElement([FromQuery] string uuid, [FromBody]string elementIdentifier)
        {

            try
            {
                var user = _userMgr.GetCurrent();
                var page = _gApp.CreateOrGetExistingPage(user, uuid);
                await _pageMgr.DeleteElement(page, user, elementIdentifier);
                _logger.LogInformation($"Element delete from page {uuid}");
                return Ok();
            }
            catch (PageManagerException pex)
            {
                _logger.LogError($"PageManagerException happened  for DeleteElement on page {uuid}: {pex}");
                return BadRequest(pex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception happened  for AddElement on page {uuid}: {ex}");
                return BadRequest(ex.Message);
            }


 
        }

    }
}
