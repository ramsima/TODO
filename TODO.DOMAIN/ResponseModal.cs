using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TODO.DOMAIN
{
    public sealed class ApiResponse<T>
    {

        [Required]
        public string responseCode { get; set; } = string.Empty;

        public dynamic? meta { get; set; }
        [Required]
        public T? result { get; set; }

        [Required]
        public string message { get; set; } = string.Empty;


    }



}
