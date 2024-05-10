using ECommerce.API.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneral _generalService;

        public GeneralController(IGeneral generalService)
        {
            _generalService = generalService ?? throw new ArgumentNullException(nameof(generalService));
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _generalService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("sizes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllSizesAsync()
        {
            try
            {
                var sizes = await _generalService.GetAllSizesAsync();
                return Ok(sizes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("color")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllColorsAsync()
        {
            try
            {
                var colors = await _generalService.GetAllColorssAsync();
                return Ok(colors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("productCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetProductCountAsync()
        {
            try
            {
                var count = await _generalService.GetProductCountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}