using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaSippina.Models;

namespace pruebaSippina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticaController : ControllerBase
    {
        private readonly Conexiones _context;

        public EstadisticaController(Conexiones context)
        {
            _context = context;
        }

        // GET: api/EstadisticaDatos
        [HttpGet]
        [Route("datos")]
        public async Task<ActionResult<IEnumerable<EstadisticaResultado>>> GetestadisticasDatos()
        {
             var consulta = from estadistica in _context.estadistica
                   join categoria in _context.categoria on estadistica.categoria equals categoria.idCategoria
                   join lugar in _context.lugar on estadistica.lugar equals lugar.idLugar
                   join edades in _context.edades on estadistica.edades equals edades.idedades
                   join fecha in _context.fecha on estadistica.fecha equals fecha.idfecha
                   join cobertura in _context.cobertura on estadistica.cobertura equals cobertura.idCobertura
                   select new EstadisticaResultado
                   {
                       Dominio = categoria.dominio,
                       Categoria = categoria.categoria,
                       Indicador = categoria.indicador,
                       Poblacion = cobertura.poblacion,
                       RangoEdades = edades.rangoEdades,
                       Entidad = lugar.entidad,
                       Anio = fecha.anio,
                       Dato = estadistica.dato
                   };

            List<EstadisticaResultado> resultados = await consulta.ToListAsync();       

            return resultados;
        } 

        // GET: api/Estadistica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estadistica>>> Getestadistica()
        {
            return await  _context.estadistica.ToListAsync();
        }

        // GET: api/Estadistica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estadistica>> GetEstadistica(int id)
        {
            var estadistica = await _context.estadistica.FindAsync(id);

            if (estadistica == null)
            {
                return NotFound();
            }

            return estadistica;
        }

        // PUT: api/Estadistica/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadistica(int id, Estadistica estadistica)
        {
            if (id != estadistica.idestadistica)
            {
                return BadRequest();
            }

            _context.Entry(estadistica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadisticaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estadistica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estadistica>> PostEstadistica(Estadistica estadistica)
        {
            _context.estadistica.Add(estadistica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadistica", new { id = estadistica.idestadistica }, estadistica);
        }

        // DELETE: api/Estadistica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadistica(int id)
        {
            var estadistica = await _context.estadistica.FindAsync(id);
            if (estadistica == null)
            {
                return NotFound();
            }

            _context.estadistica.Remove(estadistica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadisticaExists(int id)
        {
            return _context.estadistica.Any(e => e.idestadistica == id);
        }
    }
}
