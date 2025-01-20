using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.APIs.REST.Controllers.V1.Messages;

[ApiController]
[ApiVersion("1.0")]
[Route("api/chat-service/v{version:apiVersion}/[controller]")]
public class MessagesController : ControllerBase
{
    // GET: api/<MessagesController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<MessagesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<MessagesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<MessagesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<MessagesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
