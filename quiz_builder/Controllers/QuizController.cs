using ApplicationCore.Models.Quiz;
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
        //catch (WorkItemValidationException ex)
        //{
        //    return BadRequest(ex.Message);
        //}
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQuizModel model)
    {
        try
        {
            await _service.UpdateAsync(model, this.GetUserId());
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        //catch (WorkItemValidationException ex)
        //{
        //    return BadRequest(ex.Message);
        //}
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

    //[HttpGet("getall")]
    //public IActionResult GetAll()
    //{
    //    try
    //    {
    //        return Ok(_service.GetAll(GetUserId()));
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex.ToString());
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}
}
