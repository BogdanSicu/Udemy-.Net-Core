using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.ActionResults;
using my_books.Data.Exceptions;
using my_books.Data.Models;
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

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber) 
        {
            try
            {
                var _result = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            return Ok(_publishersService.GetPublisherData(id));
        }

        //[HttpGet("get-publisher-by-id/{id}")]
        //public Publisher GetPublisherById(int id)
        //{
        //    //throw new Exception("this is an exception test for middleware");

        //    var _response = _publishersService.GetPublisherById(id);

        //    if(_response != null)
        //    {
        //        //return Ok(_response);
        //        return _response;
        //    }

        //    //return NotFound();
        //    return null;
        //}

        //[HttpGet("get-publisher-by-id/{id}")]
        //public CustomActionResult GetPublisherById(int id)
        //{
        //    //throw new Exception("this is an exception test for middleware");

        //    var _response = _publishersService.GetPublisherById(id);

        //    if (_response != null)
        //    {
        //        //return Ok(_response);
        //        var _responseObj = new CustomActionResultViewModel()
        //        {
        //            Publisher = _response
        //        };

        //        return new CustomActionResult(_responseObj);
        //    }
        //    else
        //    {
        //        var _responseObj = new CustomActionResultViewModel()
        //        {
        //            Exception = new Exception("This is coming from publishers controller")
        //        };

        //        return new CustomActionResult(_responseObj);
        //    }
        //}

        [HttpGet("get-publisher-by-id/{id}")]
        public ActionResult<Publisher> GetPublisherById(int id)
        {
            //throw new Exception("this is an exception test for middleware");

            var _response = _publishersService.GetPublisherById(id);

            if (_response != null)
            {
                //return Ok(_response);
                return _response;
            }

            return NotFound();
        }

 


        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherViewModel publisher)
        {
            try
            {
                return Created(nameof(AddPublisher), _publishersService.AddPublisher(publisher));
            } 
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
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
