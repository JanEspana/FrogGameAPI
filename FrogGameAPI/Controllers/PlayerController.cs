using FrogGameAPI.Models;
using FrogGameAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FrogGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDTO _response;
        public PlayerController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }
        [HttpGet("GetPlayers")]
        public ResponseDTO GetPlayers()
        {
            try
            {
                IEnumerable<Player> players = _context.Players.ToList();
                _response.Data = players;
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet("GetPlayerById")]
        public ResponseDTO GetPlayer(int id)
        {
            try
            {
                Player? player = _context.Players.FirstOrDefault(x => x.Id == id);
                _response.Data = player;
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost("PostPlayer")]
        public ResponseDTO PostPlayer(Player player)
        {
            try
            {
                _context.Players.Add(player);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut("PutPlayer")]
        public ResponseDTO PutPlayer(Player player)
        {
            try
            {
                _context.Players.Update(player);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete("DeletePlayer")]
        public ResponseDTO DeletePlayer(int id)
        {
            try
            {
                Player? player = _context.Players.FirstOrDefault(x => x.Id == id);
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
    
}
