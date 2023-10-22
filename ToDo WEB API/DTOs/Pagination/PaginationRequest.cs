using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDo_WEB_API.DTOs.Pagination
{
    public class PaginationRequest
    {
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        [FromQuery(Name = "page")]
        [Range(1, int.MaxValue)]
        [Required]
        public int Page { get; set; } = 1;


        /// <summary>
        /// Page size - How many items in one page
        /// </summary>
        /// <example>10</example>
        [FromQuery(Name ="pageSize")]
        [Range(1, int.MaxValue)]
        [Required]
        public int PageSize { get; set; } = 10; 
    }
}
