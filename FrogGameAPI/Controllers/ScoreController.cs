using FrogGameAPI.Models;
using FrogGameAPI.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FrogGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDTO _response;
        public ScoreController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }

        [HttpGet("GetScores")]
        public ResponseDTO GetScores()
        {
            try
            {
                IEnumerable<Score> scores = _context.Scores.ToList();
                _response.Data = scores;
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetScoreById")]
        public ResponseDTO GetScoreById(int id)
        {
            try
            {
                Score? score = _context.Scores.FirstOrDefault(x => x._id == id);
                _response.Data = score;
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetScoreByPlayer")]
        public ResponseDTO GetScoreByPlayerName (string playerName)
        {
            try
            {
                IEnumerable<Score> scores = _context.Scores.Where(x => x.playerName == playerName).ToList();
               }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostScore")]
        public ResponseDTO PostScore([FromBody] Score score)
        {
            try
            {
                _context.Scores.Add(score);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut("PutScore")]
        public ResponseDTO PutScore([FromBody] Score score)
        {
            try
            {
                _context.Scores.Update(score);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete("DeleteScore")]
        public ResponseDTO DeleteScore(int id)
        {
            try
            {
                Score? score = _context.Scores.FirstOrDefault(x => x._id == id);
                _context.Scores.Remove(score);
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
