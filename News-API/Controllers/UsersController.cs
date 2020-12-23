using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using News_API.Data;
using News_API.Dtos.User;

namespace News_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
     public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthRepo _repo;

        public UsersController(IMapper mapper,IAuthRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailsDto>(user);
            return Ok(userToReturn);
        }
    }
}