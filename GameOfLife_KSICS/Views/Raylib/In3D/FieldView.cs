namespace GameOfLife_KSICS.Views.Raylib.In3D
{
  using System;
  using System.Collections.Generic;
  using System.Numerics;
  using System.Text;
  using GameOfLife_KSICS.Interfaces;
  using Raylib_cs;

  // TODO: Camera and window size is bad.
  class FieldView : IView
  {
    private static Color backgroundColor = Color.BLACK;
    private static Color colorWhenAlive = Color.DARKGRAY;
    private static Color colorWhenDead = Color.BLACK;
    private static Color colorWhenYoung = Color.GRAY;
    private static Color colorWhenAdult = Color.LIGHTGRAY;
    private static Color colorWhenOld = Color.WHITE;
    private static Color colorWhenOlder = Color.GOLD;

    public (int Width, int Height) WindowSize { get; set; }

    private Camera3D camera = new Camera3D();

    public FieldView(int windowWidth, int windowHeight)
    {
      WindowSize = (windowWidth, windowHeight);

      // Camera
      camera.position = new Vector3(WindowSize.Width / 2, Math.Min(WindowSize.Width, WindowSize.Height), WindowSize.Height / 2);
      camera.target = new Vector3(WindowSize.Width / 2, 0, WindowSize.Height / 2);
      camera.up = new Vector3(0.0f, 1.0f, 0.0f);
      camera.fovy = 90.0f;
      camera.type = CameraType.CAMERA_PERSPECTIVE;
      Raylib.SetCameraMode(camera, CameraMode.CAMERA_FREE);
    }

    public void Draw(IField field)
    {
      Raylib.UpdateCamera(ref camera);

      Raylib.BeginDrawing();
      Raylib.ClearBackground(backgroundColor);

      // TODO: camera bad
      // Move camera
      //var time = DateTime.Now.Millisecond;
      //var cameraTime = (double) (time) * 0.3;
      ////camera.position.X = (float) Math.Cos(cameraTime) * 40.0f;
      ////camera.position.Z = (float) Math.Sin(cameraTime) * 40.0f;
      //camera.position.X = (float) Math.Sin(time) * .05f;
      //camera.position.Z = (float) Math.Cos(time) * .05f; // ?????????????????????????

      if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
      {
        camera.position.Z = 100.0f;
        camera.position.X = 100.0f;
        camera.position.Y = 100.0f;
      }

      Raylib.BeginMode3D(camera);

      var cellWidth = WindowSize.Width / field.Width - 10;
      var cellheight = WindowSize.Height / field.Height - 10;
      for (int y = 0; y < field.Height; y++)
      {
        for (int x = 0; x < field.Width; x++)
        {
          if (field.Cells[x, y].Alive)
          {
            var posX = x * (WindowSize.Width / field.Width);
            var posY = y * (WindowSize.Height / field.Height);
            var pos = new Vector3(posX, 1, posY);

            // Color according to alive state of cell.
            var color = field.Cells[x, y].Alive ? colorWhenAlive : colorWhenDead;

            if (field.Cells[x, y].Alive && field.Cells[x, y].Age > 0)
            {
              // Color by age.
              color = field.Cells[x, y].Age switch
              {
                1 => colorWhenYoung,
                2 => colorWhenAdult,
                3 => colorWhenOld,
                _ => colorWhenOlder,
              };
            }

            Raylib.DrawCube(pos, cellWidth, 10, cellheight, color); 
          }
        }
      }

      Raylib.EndMode3D();

      Raylib.DrawFPS(5, 5);
      //Raylib.DrawText();

      Raylib.EndDrawing();
    }
  }
}
