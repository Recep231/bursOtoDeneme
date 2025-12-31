using Microsoft.AspNetCore.Mvc;
using BursOtomasyon.Web.Models;
using BursOtomasyon.Web.Services;

namespace BursOtomasyon.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIAnalizController : ControllerBase
    {
        private readonly AIAnalizService _aiAnalizService;
        private readonly OgrenciService _ogrenciService;
        private readonly BursPuanlamaService _puanlamaService;

        public AIAnalizController(
            AIAnalizService aiAnalizService,
            OgrenciService ogrenciService,
            BursPuanlamaService puanlamaService)
        {
            _aiAnalizService = aiAnalizService;
            _ogrenciService = ogrenciService;
            _puanlamaService = puanlamaService;
        }

        [HttpPost("analiz-yap")]
        public async Task<IActionResult> AnalizYap([FromBody] AnalizRequest request)
        {
            if (request == null || request.OgrenciID <= 0)
            {
                return BadRequest(new { error = "Geçersiz öğrenci ID" });
            }

            try
            {
                var ogrenci = _ogrenciService.GetById(request.OgrenciID);
                if (ogrenci == null)
                {
                    return NotFound(new { error = "Öğrenci bulunamadı" });
                }

                var bursPuani = ogrenci.BursPuani > 0 
                    ? ogrenci.BursPuani 
                    : _puanlamaService.HesaplaBursPuani(ogrenci);

                var analizSonucu = await _aiAnalizService.AnalizYapAsync(ogrenci, bursPuani);

                return Ok(new
                {
                    success = true,
                    ogrenciId = ogrenci.OgrenciID,
                    ogrenciAdi = $"{ogrenci.Ad} {ogrenci.Soyad}",
                    bursPuani = bursPuani,
                    analiz = analizSonucu
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Hata oluştu: {ex.Message}" });
            }
        }

        [HttpPost("analiz-yap-detayli")]
        public async Task<IActionResult> AnalizYapDetayli([FromBody] OgrenciModel ogrenci)
        {
            if (ogrenci == null)
            {
                return BadRequest(new { error = "Geçersiz öğrenci bilgileri" });
            }

            try
            {
                var bursPuani = ogrenci.BursPuani > 0 
                    ? ogrenci.BursPuani 
                    : _puanlamaService.HesaplaBursPuani(ogrenci);

                var analizSonucu = await _aiAnalizService.AnalizYapAsync(ogrenci, bursPuani);

                return Ok(new
                {
                    success = true,
                    bursPuani = bursPuani,
                    analiz = analizSonucu
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Hata oluştu: {ex.Message}" });
            }
        }
    }

    public class AnalizRequest
    {
        public int OgrenciID { get; set; }
    }
}

