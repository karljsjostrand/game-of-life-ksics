﻿namespace GameOfLife_KSICS.Controllers
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  class FieldController
  {
    Field Field { get; }

    public FieldController(int width, int height)
    {
      Field = new Field(width, height);
    }

    public void Update()
    {
      
    }
  }
}
