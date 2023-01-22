using Microsoft.AspNetCore.Http;
using ReelRoster.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReelRoster.Models.Database
{
    public class Movie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string TagLine { get; set; }
        public string Overview { get; set; }
        public int RunTime { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public MovieRating Rating { get; set; }
        public float VoteAverage { get; set; }


        public byte[] Poster { get; set; }
        public string PosterType { get; set; }

        
        public byte[] Backdrop { get; set; }
        public string BackdropType { get; set; }

        public string TrailerUrl { get; set;}

        [NotMapped]
        [Display(Name = "Poster Image")]
        public IFormFile PosterFile { get; set; }

        [NotMapped]
        [Display(Name = "Backdrop Image")]
        public IFormFile BackdropFile { get; set; }


        public ICollection<MovieCollection> Collections { get; set; } = new HashSet<MovieCollection>();
        public ICollection<MovieCast> Cast { get; set; } = new HashSet<MovieCast>();
        public ICollection<MovieCrew> Crew { get; set; } = new HashSet<MovieCrew>();

    }
}
