using AspNetCoreAnatomySamples.Infrasructure.ResourceFilter;
using AspNetCoreAnatomySamples.Models.Output;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreAnatomySamples.Controllers
{
    // IMAGINE THIS IS UNDER DEVELOPMENT AND IN PREVIEW
    [ApiController]
    [Route("[controller]")]
    [PreviewResourceFilter]
    public class AuthorsController : ControllerBase
    {
        [ProducesResponseType(200)]
        [HttpGet]
        public ActionResult<IEnumerable<AuthorOutputModel>> Get()
        {
            return Enumerable.Range(1, 200).Select(x => new AuthorOutputModel()).ToArray();
        }

        [Route("enumerable")]
        [ProducesResponseType(200)]
        [HttpGet]
        public IEnumerable<AuthorOutputModel> GetEnumerable() // converted to ObjectResult by the framework
        {
            return Enumerable.Range(1, 200).Select(x => new AuthorOutputModel()).ToArray();
        }
    }
}
