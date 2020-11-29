using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApiServer.Interfaces;
using RestApiServer.Models;

namespace RestApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HocrController : ControllerBase
    {
        private readonly IHocrService _hocrService;

        public HocrController(IHocrService hocrService)
        {
            _hocrService = hocrService;
        }
        /// <summary>
        /// Upload hocr document on server 
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     {
        ///         "Url":"http://amwrag.com/test_document.png.hocr"
        ///     }
        /// 
        /// </remarks>
        /// <param name="hocrLink">Url of hocr document</param>
        [HttpPost("AddDocument")]
        public async Task<IActionResult> Post([FromBody]HocrLinkRequest hocrLink)
        {
            if (hocrLink?.Url == null)
            {
                return BadRequest();
            }

            await _hocrService.AddDocument(hocrLink);

            return Ok();
        }
        /// <summary>
        /// Return hocr document as json value by document link-name
        /// </summary>
        /// <remarks>
        /// Example document name:
        /// 
        ///     http://amwrag.com/test_document.png.hocr
        /// 
        /// </remarks>
        /// <param name="name">Full original document url</param>
        [HttpGet("GetDocument")]
        public async Task<IActionResult> GetDocument([Required]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            return Ok(await _hocrService.GetDocument(name));
        }

        /// <summary>
        /// Search and return hocr document lines by hocr line id
        /// </summary>
        /// <param name="id">Id of hocr document line</param>
        /// <remarks>
        /// Example id:
        /// 
        ///     page_1
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet("GetHocrById")]
        public async Task<IActionResult> GetHocrById([Required]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            return Ok(await _hocrService.GetHocr(id));
        }

        /// <summary>
        /// Search and return hocr document lines by hocr document name and line class
        /// </summary>
        /// <remarks>
        /// Example document name:
        /// 
        ///     http://amwrag.com/test_document.png.hocr
        ///
        /// Example class:
        ///
        ///     ocr_par
        /// </remarks>
        /// <param name="document">Hocr document name</param>
        /// <param name="class">Hocr line class</param>
        /// <returns></returns>
        [HttpGet("GetHocrByClass")]
        public async Task<IActionResult> GetHocrByClass([Required]string document, [Required]string @class)
        {
            if (string.IsNullOrEmpty(@class) || string.IsNullOrEmpty(document))
            {
                return BadRequest();
            }
            return Ok(await _hocrService.GetHocrByClass(document, @class));
        }

        /// <summary>
        /// Search and return hocr document lines by hocr document name and line title
        /// </summary>
        /// <remarks>
        /// Example document name:
        /// 
        ///     http://amwrag.com/test_document.png.hocr
        ///
        /// Example title:
        ///
        ///     bbox 521 907 648 920   
        /// </remarks>
        /// <param name="document">Hocr document name</param>
        /// <param name="title">Hocr line title</param>
        [HttpGet("GetHocrByTitle")]
        public async Task<IActionResult> GetHocrByTitle([Required]string document, [Required]string title)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(document))
            {
                return BadRequest();
            }
            return Ok(await _hocrService.GetHocrByTitle(document, title));
        }

        /// <summary>
        /// Search and return hocr document lines by hocr document name, line title and class
        /// </summary>
        /// <remarks>
        /// Example document name:
        /// 
        ///     http://amwrag.com/test_document.png.hocr
        ///
        /// Example class:
        /// 
        ///     ocr_carea
        /// 
        /// Example title:
        ///
        ///     bbox 521 907 648 920
        /// </remarks>
        /// <param name="document">Hocr document name</param>
        /// <param name="title">Hocr line title</param>
        /// <param name="class">Hocr line class</param>
        [HttpGet("GetHocrByClassWithTitle")]
        public async Task<IActionResult> GetHocrByClassWithTitle([Required]string document, string @class, string title)
        {
            if (string.IsNullOrEmpty(document))
            {
                return BadRequest();
            }
            return Ok(await _hocrService.GetHocrByClassWithTitle(document, @class, title));
        }

        /// <summary>
        /// Search and return hocr document lines by hocr document name, line class and lang
        /// </summary>
        /// <remarks>
        /// Example document name:
        /// 
        ///     http://amwrag.com/test_document.png.hocr
        ///
        /// Example class:
        /// 
        ///     ocr_par
        /// 
        /// Example lang:
        /// 
        ///     rus
        /// </remarks>
        /// <param name="document">Hocr document name</param>
        /// <param name="class">Hocr line class</param>
        /// <param name="lang">Hocr line lang</param>
        [HttpGet("GetHocrByClassWithLang")]
        public async Task<IActionResult> GetHocrByClassWithLang([Required]string document, string @class, string lang)
        {
            if (string.IsNullOrEmpty(document))
            {
                return BadRequest();
            }
            return Ok(await _hocrService.GetHocrByClassWithLang(document, @class, lang));
        }

        /// <summary>
        /// Search and return hocr document lines by hocr document name, line title and lang
        /// </summary>
        /// <remarks>
        /// Example document name:
        /// 
        ///     http://amwrag.com/test_document.png.hocr
        ///
        /// Example title:
        /// 
        ///     bbox 521 907 648 920
        /// 
        /// Example lang:
        /// 
        ///     eng
        /// </remarks>
        /// <param name="document">Hocr document name</param>
        /// <param name="title">Hocr line title</param>
        /// <param name="lang">Hocr line lang</param>
        [HttpGet("GetHocrByTitleWithLang")]
        public async Task<IActionResult> GetHocrByTitleWithLang([Required]string document, string title, string lang)
        {
            if (string.IsNullOrEmpty(document))
            {
                return BadRequest();
            }
            return Ok(await _hocrService.GetHocrByTitleWithLang(document, title, lang));
        }

        /// <summary>
        /// Search and return hocr document lines by hocr document name, line class, title and lang
        /// </summary>
        /// <remarks>
        /// Example document name:
        /// 
        ///     http://amwrag.com/test_document.png.hocr
        ///
        /// Example class:
        /// 
        ///     ocr_par
        /// 
        /// Example title:
        /// 
        ///     bbox 521 907 648 920
        /// 
        /// Example lang:
        /// 
        ///     eng
        /// </remarks>
        /// <param name="document">Hocr document name</param>
        /// <param name="class">Hocr line class</param>
        /// <param name="title">Hocr line title</param>
        /// <param name="lang">Hocr line lang</param>
        [HttpGet("GetHocr")]
        public async Task<IActionResult> GetHocr([Required]string document, string @class, string title, string lang)
        {
            if (string.IsNullOrEmpty(document))
            {
                return BadRequest();
            }
            return Ok(await _hocrService.GetHocr(document, @class, title, lang));
        }
    }
}