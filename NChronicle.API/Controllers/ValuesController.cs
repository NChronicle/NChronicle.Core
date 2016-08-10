using System.Collections.Generic;
using System.Web.Http;

namespace NChronicle.API.Controllers {

    public class ValuesController : ApiController {

        public IEnumerable<string> Get() {
            return new string[] { "LOL!", "LOL2!" };
        }

        public string Get(int id) {
            return "LOL";
        }

        public void Post([FromBody] string value) {
        }

        public void Put(int id, [FromBody]string value) {
        }

        public void Delete(int id) {
        }

    }

}
