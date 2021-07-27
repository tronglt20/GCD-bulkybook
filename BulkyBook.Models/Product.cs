﻿using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id  { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
   
        [Required]
        public string Author { get; set; }
     
  
        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }
  
       
        [Display(Name = "Upload File")]
        public string ImageUrl { get; set; }


        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Cover Type")]
        public int CoverTypeId { get; set; }

        [ForeignKey("CoverTypeId")]
        public CoverType CoverType { get; set; }


    }
}
