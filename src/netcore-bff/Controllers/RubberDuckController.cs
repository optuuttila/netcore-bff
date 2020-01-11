using System.Collections.Generic;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netcore_bff.Model;
using netcore_data_access.Repositories;

namespace netcore_bff.Controllers {
    [Route("api/rubberDucks")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class RubberDuckController : ControllerBase {
        private readonly IMapper mapper;
        private readonly RubberDuckRepository repository;

        public RubberDuckController(RubberDuckRepository repository, IMapper mapper) {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public List<RubberDuck> Get() {
            return mapper.Map<List<RubberDuck>>(repository.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RubberDuck> GetById(int id) {
            if (!repository.TryGet(id, out var rubberDuck)) {
                return NotFound();
            }

            return mapper.Map<RubberDuck>(rubberDuck);
        }
    }
}