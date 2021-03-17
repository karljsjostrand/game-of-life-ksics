namespace GameOfLife_KSICS.Views.Raylib
{
  using System;
  using System.Collections.Generic;
  using System.Numerics;
  using System.Text;
  using GameOfLife_KSICS.Models;
  using Raylib_cs;

  class FieldView
  {
    private static Field _field = new Field(80, 40);
    private static int _windowWidth = 1600;
    private static int _windowHeight = 800;
    private static Color _colorWhenAlive = Color.DARKPURPLE;
    private static Color _colorWhenDead = Color.DARKGRAY;
    private static int _targetFps = 60;

    static FieldView()
    {
      // add line
      _field.Cells[5, 3].Alive = true;
      _field.Cells[6, 3].Alive = true;
      _field.Cells[7, 3].Alive = true;

      // add tub
      _field.Cells[74, 8].Alive = true;
      _field.Cells[75, 7].Alive = true;
      _field.Cells[76, 8].Alive = true;
      _field.Cells[75, 9].Alive = true;

      // add box
      _field.Cells[45, 12].Alive = true;
      _field.Cells[46, 12].Alive = true;
      _field.Cells[47, 12].Alive = true;
      _field.Cells[47, 13].Alive = true;
      _field.Cells[47, 14].Alive = true;
      _field.Cells[46, 14].Alive = true;
      _field.Cells[45, 14].Alive = true;
      _field.Cells[45, 13].Alive = true;

      // add glider
      _field.Cells[20, 18].Alive = true;
      _field.Cells[21, 19].Alive = true;
      _field.Cells[22, 17].Alive = true;
      _field.Cells[22, 18].Alive = true;
      _field.Cells[22, 19].Alive = true;
    }

    public static void DrawIn2D(Field field)
    {
      _field = field;
      DrawIn2D();
    }

    public static void DrawIn2D()
    {
      Raylib.SetTargetFPS(_targetFps);

      Raylib.InitWindow(_windowWidth, _windowHeight, "Game of Life");

      while (!Raylib.WindowShouldClose())
      {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BLACK);

        var mousePosition = Raylib.GetMousePosition(); // TODO: use to click cells?

        var cellWidth = _windowWidth / _field.Width - 1;
        var cellheight = _windowHeight / _field.Height - 1;
        foreach (var cell in _field.Cells)
        {
          var posX = cell.X * (_windowWidth / _field.Width);
          var posY = cell.Y * (_windowHeight / _field.Height);

          // color according to alive state of cell
          var color = cell.Alive ? _colorWhenAlive : _colorWhenDead;

          Raylib.DrawRectangle(posX, posY, cellWidth, cellheight, color);
          Raylib.DrawFPS(5, 5); // TODO: remove
        }

        Raylib.EndDrawing();
      }

      Raylib.CloseWindow();
    }

    public static void DrawIn3D() // TODO: camera bad
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

        var cellWidth = windowWidth / _field.Width - 3;
        var cellheight = windowHeight / _field.Height - 3;
        foreach (var cell in _field.Cells)
        {
          var posX = cell.X * (windowWidth / _field.Width);
          var posY = cell.Y * (windowHeight / _field.Height);
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
