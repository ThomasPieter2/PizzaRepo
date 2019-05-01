using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaReservation.API.Models;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaReservation.API.Controllers
{
    [Route("api/[controller]")]
    public class SizesController : ControllerBase
    {
        private readonly ISizeRepo _sizesRepo;
        private readonly IMapper _mapper;

        public SizesController(ISizeRepo sizesRepo, IMapper mapper)
        {
            _sizesRepo = sizesRepo;
            _mapper = mapper;
        }

        #region Read (GET)

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeDTO>>> GetSizes()
        {
            var sizes = await _sizesRepo.GetSizesAsync();
            List<SizeDTO> sizesdto = new List<SizeDTO>();
            sizes.ForEach(s => sizesdto.Add(_mapper.Map<SizeDTO>(s)));

            return Ok(sizesdto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SizeDTO>> GetSize(Guid id)
        {
            var size = await _sizesRepo.GetSizeAsync(id);
            if (size == null) return NotFound();
            return _mapper.Map<SizeDTO>(size);
        }

        #endregion

        #region Create (POST)

        [HttpPost]
        public async Task<IActionResult> CreateSize([FromBody] Size size)
        {
            if (size == null) BadRequest();
            if (await _sizesRepo.CreateSizeAsync(size)) return CreatedAtAction(nameof(GetSize), new { id = size.SizeId }, size);
            else return Conflict($"Item already exists.");
        }

        #endregion

        #region Update (PUT)

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSizeAsync(Guid id, [FromBody] Size size)
        {
            var search = await _sizesRepo.GetSizeAsync(id);
            if (search == null) return BadRequest();
            size.SizeId = id;
            await _sizesRepo.UpdateSizeAsync(size);
            return NoContent();
        }

        #endregion

        #region Delete (DELETE)

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSizeAsync(Guid id)
        {
            if (await _sizesRepo.RemoveSizeAsync(id)) return NoContent();
            else return NotFound();
        }

        #endregion
    }
}
