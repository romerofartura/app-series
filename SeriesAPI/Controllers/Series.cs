using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SeriesAPI.ModelDto;

namespace SeriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Series : ControllerBase
    {
        private readonly ISerie _service;

        public Series(ISerie series)
        {
            _service = series;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Serie>>> Get()
        {
            var list = _service.Lista();

            if (list == null)
                return NotFound("Dados não encontrados.");

            return Ok(list);
        }

        [HttpGet("{id}", Name = "GetId")]
        public async Task<ActionResult<Serie>> Get(int id)
        {
            var modeloDTO = _service.RetornaPorId(id);

            if (modeloDTO == null)
                return NotFound("Dados não encontrados.");

            return Ok(modeloDTO);
        }

        [HttpPost]
        //public async Task<ActionResult> Post([FromBody] Serie modeloDTO)
        public async Task<ActionResult> Post([FromBody] SerieDTO modeloDTO)
        {
            if (modeloDTO == null)
                return BadRequest("Dados enviados inválidos");

            Serie novaSerie = new Serie(id: _service.ProximoId(),
                                            genero: (Genero)modeloDTO.Genero,
                                            titulo: modeloDTO.Titulo,
                                            ano: modeloDTO.Ano,
                                            descricao: modeloDTO.Descricao);

            if (_service.Insere(novaSerie))
                return new CreatedAtRouteResult("GetId", new { id = novaSerie.Id }, modeloDTO);
            else
                return BadRequest("Erro ao inserir registro.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Serie modeloDTO)
        {
            if (id != modeloDTO.Id)
                return BadRequest("Dados enviados inválidos");

            if (modeloDTO == null)
                return BadRequest("Dados enviados inválidos. Não pode ser nulo.");

            if (_service.Atualiza(modeloDTO.Id, modeloDTO))
                return Ok(modeloDTO);
            else
                return BadRequest("Erro ao atualizar registro.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (_service.Exclui(id))
                return Ok();
            
            return NotFound("Registro não encontrado.");
        }

    }
}
