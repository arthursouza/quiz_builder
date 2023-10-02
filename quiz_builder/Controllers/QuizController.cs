using ApplicationCore.Exceptions;
using ApplicationCore.Models.Quiz;
using ApplicationCore.Models.Quiz.Attempt;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class QuizController : ControllerBase
{
    private readonly ILogger<QuizController> _logger;
    private readonly IQuizService _service;

    public QuizController(ILogger<QuizController> logger, IQuizService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] QuizModel model)
    {
        try
        {
            var result = await _service.CreateAsync(model, this.GetUserId());
            return Ok(result);
        }
        catch (QuizValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateQuizModel model)
    {
        try
        {
            _service.Update(model, this.GetUserId());
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (QuizValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _service.Remove(id, this.GetUserId());
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("getall")]
    public IActionResult Get()
    {
        try
        {
            return Ok(_service.GetAll(this.GetUserId()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    public IActionResult Get(Guid id)
    {
        try
        {
            var result = _service.Get(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Route("answer")]
    public async Task<IActionResult> Answer([FromBody] QuizAttemptModel model)
    {
        try
        {
            await _service.AnswerAsync(model, this.GetUserId());
            return Ok();
        }
        catch (QuizValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Route("publish")]
    public IActionResult Publish([FromBody] Guid id)
    {
        try
        {
            _service.Publish(id, this.GetUserId());
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (QuizValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("getmyattempts")]
    public IActionResult GetMyAttempts(int page = 0, int size = 0)
    {
        try
        {
            return Ok(_service.GetAllAttempts(this.GetUserId(), page, size));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("getattempts")]
    public IActionResult GetAttempts(Guid id, int page = 0, int size = 0)
    {
        try
        {
            return Ok(_service.GetAllAttempts(id, this.GetUserId(), page, size));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}
