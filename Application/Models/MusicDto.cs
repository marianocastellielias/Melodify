﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MusicDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeOnly Duration { get; set; }
    }
}
