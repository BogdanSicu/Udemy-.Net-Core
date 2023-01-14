﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using System;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService; 
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            return Ok(_publishersService.GetPublisherData(id));
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publishersService.GetPublisherById(id);

            if(_response != null)
            {
                return Ok(_response);
            }

            return NotFound();
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherViewModel publisher)
        {
            return Created(nameof(AddPublisher), _publishersService.AddPublisher(publisher));
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                int x1 = 1, x2 = 0;
                int result = x1 / x2;

                _publishersService.DeletePublisherById(id);
                return Ok();
            } 
            catch(Exception ex)
            {
                //return BadRequest("Custom Message");
                return BadRequest(ex.Message);
            }
        }
    }
}
