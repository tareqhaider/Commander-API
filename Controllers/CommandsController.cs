using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommander _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommander repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //private readonly MockCommander _repository = new MockCommander();

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));

            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDTO);

            _repository.CreateCommand(commandModel);

            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);

            //return Ok(commandReadDTO);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDTO.Id }, commandReadDTO);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDTO, commandItem);

            _repository.UpdateCommand(commandItem);

            _repository.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDTO> patchItem)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem == null)
            {
                return NotFound();
            }

            var commanndPatch = _mapper.Map<CommandUpdateDTO>(commandItem);

            patchItem.ApplyTo(commanndPatch, ModelState);

            if (!TryValidateModel(commanndPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commanndPatch, commandItem);

            _repository.UpdateCommand(commandItem);

            _repository.SaveChanges();

            return NoContent();


        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandItem);
            _repository.SaveChanges();

            return NoContent();
        }

    }
}