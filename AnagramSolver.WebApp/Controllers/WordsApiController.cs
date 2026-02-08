using Microsoft.AspNetCore.Mvc;
using AnagramSolver.Contracts;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("api/words")]
    public class WordsApiController : ControllerBase
    {
        private readonly IWordRepository _repo;

        public WordsApiController(IWordRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get(int page = 1, int pageSize = 100)
        {
            if (page < 1)
            {
                page = 1;
            }

            if(pageSize < 1)
            {
                pageSize = 1;
            }

            var all = (await _repo.GetAllWordsAsync()).Select(w => (w ?? "").Trim()).Where(w => w.Length > 0).ToList();

            var items = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<string>>> GetById(int id)
        {
            var all = (await _repo.GetAllWordsAsync()).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

            if(id < 0 || id >= all.Count)
            {
                return NotFound();
            }

            return Ok(all[id]);
        }

        [HttpPost]
        public ActionResult Create([FromBody] string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return BadRequest("Neįvestas žodis.");              //cia fake metodas, priminimas pridet funkcionaluma
            }

            return Ok($"Žodis '{word}' pridėtas.");
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return Ok($"Žodis '{id}' ištrintas.");    //cia irgi
        }
    }
}
