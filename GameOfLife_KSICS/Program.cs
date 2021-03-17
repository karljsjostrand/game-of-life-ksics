namespace GameOfLife_KSICS
{
  using System;
  using System.Numerics;
  using GameOfLife_KSICS.Models;
  using Raylib_cs;

  class Program
  {
    static void Main()
    {
      Scenario2D();
      //Scenario3D();
    }

    private static void Scenario2D()
    {
      var field = new Field(80, 40);

      // lines
      field.Cells[5, 3].Alive = true;
      field.Cells[6, 3].Alive = true;
      field.Cells[7, 3].Alive = true;

      // diamond
      field.Cells[24, 8].Alive = true;
      field.Cells[25, 7].Alive = true;
      field.Cells[26, 8].Alive = true;
      field.Cells[25, 9].Alive = true;

      // box
      field.Cells[45, 12].Alive = true;
      field.Cells[46, 12].Alive = true;
      field.Cells[47, 12].Alive = true;
      field.Cells[47, 13].Alive = true;
      field.Cells[47, 14].Alive = true;
      field.Cells[46, 14].Alive = true;
      field.Cells[45, 14].Alive = true;
      field.Cells[45, 13].Alive = true;

      DrawFieldIn2D(field);
    }

    private static void Scenario3D()
    {
      var field = new Field(80, 20);

      // lines
      field.Cells[5, 3].Alive = true;
      field.Cells[6, 3].Alive = true;
      field.Cells[7, 3].Alive = true;

      // diamond
      field.Cells[24, 8].Alive = true;
      field.Cells[25, 7].Alive = true;
      field.Cells[26, 8].Alive = true;
      field.Cells[25, 9].Alive = true;

      DrawFieldIn3D(field);
    }

    private static void DrawFieldIn2D(Field field)
    {
      var colorWhenAlive = Color.DARKPURPLE;
      var colorWhenDead = Color.DARKGRAY;
      var windowWidth = 1600;
      var windowHeight = 800;
      Raylib.InitWindow(windowWidth, windowHeight, "Game of Life");

      while (!Raylib.WindowShouldClose())
      {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BLACK);

        var mousePosition = Raylib.GetMousePosition();

        var cellWidth = windowWidth / field.Width - 1;
        var cellheight = windowHeight / field.Height - 1;
        foreach (var cell in field.Cells)
        {
          var posX = cell.X * (windowWidth / field.Width);
          var posY = cell.Y * (windowHeight / field.Height);
          var color = cell.Alive ? colorWhenAlive : colorWhenDead;

          Raylib.DrawRectangle(posX, posY, cellWidth, cellheight, color);
        }
        
        Raylib.EndDrawing();
      }
      Raylib.CloseWindow();
    }

    private static void DrawFieldIn3D(Field field) // TODO: camera bad
    {
      var colorWhenAlive = Color.DARKPURPLE;
      var colorWhenDead = Color.DARKGRAY;
      var windowWidth = 1600;
      var windowHeight = 800;
      Raylib.InitWindow(windowWidth, windowHeight, "Game of Life");

      // Camera
      Camera3D camera = new Camera3D();
      camera.position = new Vector3(windowWidth / 2, windowWidth / 2, windowHeight / 2);
      camera.target = new Vector3(windowWidth / 2, windowWidth / 3, 390f);
      camera.up = new Vector3(0.0f, 1.0f, 0.0f);
      camera.fovy = 90.0f;
      camera.type = CameraType.CAMERA_PERSPECTIVE;
      Raylib.SetCameraMode(camera, CameraMode.CAMERA_FIRST_PERSON); 

      Raylib.SetTargetFPS(60);

      while (!Raylib.WindowShouldClose())
      {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BLACK);

        //// Move camera
        //var time = DateTime.Now.Millisecond; // ???
        ////var cameraTime = (double) (time) * 0.3;
        ////camera.position.X = (float) Math.Cos(cameraTime) * 40.0f;
        ////camera.position.Z = (float) Math.Sin(cameraTime) * 40.0f;
        //camera.position.X = (float) Math.Sin(time) * .05f;
        //camera.position.Z = (float) Math.Cos(time) * .05f; // ?????????????????????????

        Raylib.UpdateCamera(ref camera);

        Raylib.BeginMode3D(camera);

        var cellWidth = windowWidth / field.Width - 3;
        var cellheight = windowHeight / field.Height - 3;
        foreach (var cell in field.Cells)
        {
          var posX = cell.X * (windowWidth / field.Width);
          var posY = cell.Y * (windowHeight / field.Height);
          var pos = new Vector3(posX, 1, posY);
          var color = cell.Alive ? colorWhenAlive : colorWhenDead;

          Raylib.DrawCube(pos, cellWidth, 1, cellheight, color);
        }

        Raylib.EndMode3D();

        Raylib.DrawFPS(5, 5);
        Raylib.DrawText($"Position: {camera.position}", 5, 25, 10, Color.YELLOW);
        Raylib.DrawText($"Target:   {camera.target}", 5, 35, 10, Color.YELLOW);

        Raylib.EndDrawing();
      }
      Raylib.CloseWindow();
    }
  }
}
