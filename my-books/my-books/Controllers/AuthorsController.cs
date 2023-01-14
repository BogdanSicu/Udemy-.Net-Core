﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorsService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            return Ok(_authorsService.GetAuthorWithBooks(id));
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorViewModel author)
        {
            _authorsService.AddAuthor(author);
            return Ok();
        }
    }
}
