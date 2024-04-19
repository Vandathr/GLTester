using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;


namespace GLTester
{
    internal class VisualControl
    {
        private readonly Model refToModel;

        private readonly VideoAdapterManager ManagerOfVideoAdapter;

        private readonly GameWindow Game;


        private readonly GraphicsMode[] graphicModeN = new GraphicsMode[] { GraphicsMode.Default };
        private readonly GameWindowFlags[] windowFlagN = new GameWindowFlags[] { GameWindowFlags.Default };
        private readonly DisplayDevice[] displayDeviceN = new DisplayDevice[] { DisplayDevice.Default };
        private readonly GraphicsContextFlags[] graphicsContextFlagN = new GraphicsContextFlags[] { GraphicsContextFlags.Default };

        private readonly Action<MaterialFace>[] polygonModeCommandN;


        private int cursorStandardPositionX;
        private int cursorStandardPositionY;


        private OpenTK.Input.MouseState StateOfMouse;
        private OpenTK.Input.KeyboardState StateOfKeyboard;


        private float frameTime = 0.0f;
        private int fps = 0;

        private Color StandardBackgroundColor;


        public void RunGame() => Game.Run(40);


        public VisualControl(Model refToModel)
        {
            this.refToModel = refToModel;

            ManagerOfVideoAdapter = new VideoAdapterManager(refToModel);


            polygonModeCommandN = new Action<MaterialFace>[] 
            { 
                (necessaryFace) => { },
                (necessaryFace) => GL.PolygonMode(necessaryFace, PolygonMode.Point),
                (necessaryFace) => GL.PolygonMode(necessaryFace, PolygonMode.Line),
                (necessaryFace) => GL.PolygonMode(necessaryFace, PolygonMode.Fill)
            };



            Game = new GameWindow(refToModel.GetScreenWidth(), refToModel.GetScreenHeight(), graphicModeN[refToModel.GetGraphicsModeIndex()],
            refToModel.GetWindowTitle(), windowFlagN[refToModel.GetGameWindowFlagsIndex()], displayDeviceN[refToModel.GetDisplayDeviceIndex()],
            refToModel.GetManorOpenGLVersion(), refToModel.GetMinorOpenGLVersion(), graphicsContextFlagN[refToModel.GetContextFlagIndex()]);

            Game.CursorVisible = false;

            Game.Bounds = new Rectangle(0, 0, refToModel.GetScreenWidth(), refToModel.GetScreenHeight());

            Game.Load += (sender, e) => LoadEvent(sender, e);

            Game.UpdateFrame += (sender, e) => UpdateFrameEvent(sender, e);
            Game.RenderFrame += (sender, e) => RenderFrameEvent(sender, e);

            Game.Unload += (sender, e) => ClearFormResourcez();

            Game.Resize += (sender, e) => ResizeEvent(sender, e); GL.Viewport(0, 0, Game.Width, Game.Height);

            Game.Move += (sender, e) =>
            {
                cursorStandardPositionX = Game.X + Game.Width / 2;
                cursorStandardPositionY = Game.Y + Game.Height / 2;
            };

            WriteStandardDataIntoConsole();
        }

        #region Eventz

        private void LoadEvent(Object sender, EventArgs e)
        {
            // setup settings, load textures, sounds

            cursorStandardPositionX = Game.X + Game.Width / 2;
            cursorStandardPositionY = Game.Y + Game.Height / 2;

            StandardBackgroundColor = ManagerOfVideoAdapter.GetSkyColor();

            GL.ClearColor(StandardBackgroundColor.GetR(), StandardBackgroundColor.GetG(), StandardBackgroundColor.GetB(), StandardBackgroundColor.GetA());

            Game.VSync = VSyncMode.On;


            polygonModeCommandN[refToModel.GetFrontPoligineModeIndex()](MaterialFace.Front);
            polygonModeCommandN[refToModel.GetBackPoligineModeIndex()](MaterialFace.Back);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            OpenTK.Graphics.OpenGL.GL.LoadIdentity();

            var orthoSettingN = refToModel.GetOrthoSettingz();

            OpenTK.Graphics.OpenGL.GL.Ortho(orthoSettingN[0], orthoSettingN[1], orthoSettingN[2], orthoSettingN[3], orthoSettingN[4], orthoSettingN[5]);

            var frustymSettingN = refToModel.GetFrustumSettingN();

            OpenTK.Graphics.OpenGL.GL.Frustum(frustymSettingN[0], frustymSettingN[1], frustymSettingN[2], frustymSettingN[3], frustymSettingN[4], frustymSettingN[5]);

            

            ManagerOfVideoAdapter.LoadDataFromCPUToGPU();

        }

        private void UpdateFrameEvent(Object sender, FrameEventArgs e)
        {
            frameTime += (float)e.Time;
            fps++;

            if (frameTime >= 1)
            {
                Game.Title = fps.ToString();
                frameTime = 0;
                fps = 0;
            }

            if (Game.Focused)
            {
                StateOfMouse = OpenTK.Input.Mouse.GetCursorState();

                refToModel.MouseMove(StateOfMouse.X - cursorStandardPositionX, StateOfMouse.Y - cursorStandardPositionY);

                OpenTK.Input.Mouse.SetPosition(cursorStandardPositionX, cursorStandardPositionY);


                StateOfKeyboard = OpenTK.Input.Keyboard.GetState();

                if (StateOfKeyboard.IsKeyDown(OpenTK.Input.Key.Q))
                {
                    refToModel.ButtonDown((int)OpenTK.Input.Key.Q);
                }

                if (StateOfKeyboard.IsKeyDown(OpenTK.Input.Key.E))
                {
                    refToModel.ButtonDown((int)OpenTK.Input.Key.E);
                }


                if (StateOfKeyboard.IsKeyDown(OpenTK.Input.Key.W))
                {
                    refToModel.ButtonDown((int)OpenTK.Input.Key.W);
                }

                if (StateOfKeyboard.IsKeyDown(OpenTK.Input.Key.S))
                {
                    refToModel.ButtonDown((int)OpenTK.Input.Key.S);
                }

                if (StateOfKeyboard.IsKeyDown(OpenTK.Input.Key.A))
                {
                    refToModel.ButtonDown((int)OpenTK.Input.Key.A);
                }

                if (StateOfKeyboard.IsKeyDown(OpenTK.Input.Key.D))
                {
                    refToModel.ButtonDown((int)OpenTK.Input.Key.D);
                }


                // add game logic, input handling
                if (StateOfKeyboard.IsKeyDown(OpenTK.Input.Key.Escape))
                {
                    refToModel.ButtonDown((int)OpenTK.Input.Key.Escape);

                    if (refToModel.CheckExitCommand()) Game.Exit();

                }

            }

            refToModel.DoWork();

            WriteDebugInfoIntoConsole();
        }


        private void RenderFrameEvent(Object sender, FrameEventArgs e)
        {
            // render graphics

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.LineWidth(10.0f);

            var StandardBackgroundColor = ManagerOfVideoAdapter.GetSkyColor();

            GL.ClearColor(StandardBackgroundColor.GetR(), StandardBackgroundColor.GetG(), StandardBackgroundColor.GetB(), StandardBackgroundColor.GetA());

            ManagerOfVideoAdapter.Draw();

            Game.SwapBuffers();
        }


        private void ResizeEvent(Object sender, EventArgs e)
        {
            GL.Viewport(0, 0, Game.Width, Game.Height);
        }


        private void ClearFormResourcez() => ManagerOfVideoAdapter.ClearFromResourcez();




        #endregion


        private void WriteStandardDataIntoConsole()
        {
            Console.WriteLine(GL.GetString(StringName.Version));
            Console.WriteLine(GL.GetString(StringName.Vendor));
            //Console.WriteLine(OpenTK.Graphics.OpenGL4.GL.GetString(OpenTK.Graphics.OpenGL4.StringName.Extensions));
            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
            
        }

        private void WriteDebugInfoIntoConsole()
        {
            Console.Clear();
            Console.WriteLine("SystemTime=" + refToModel.GetSystemTime());
            Console.WriteLine(refToModel.GetWorldHourz() + ":" + refToModel.GetWorldMinutez() + ":" + refToModel.GetWorldSecondz());
            Console.WriteLine("X=" + refToModel.GetCameraPositionX());
            Console.WriteLine("Y=" + refToModel.GetCameraPositionY());
            Console.WriteLine("Z=" + refToModel.GetCameraPositionZ());
            Console.WriteLine("AX=" + refToModel.GetCameraAngleX());
            Console.WriteLine("AY=" + refToModel.GetCameraAngleY());
            Console.WriteLine("AZ=" + refToModel.GetCameraAngleZ());
            Console.WriteLine("Angle camera and zero point");
            Console.WriteLine(Math.Atan2(refToModel.GetCameraPositionY(), refToModel.GetCameraPositionX()) * MovableObject.degreeKoefficient);
            Console.WriteLine("Tree angle");
            Console.WriteLine(Math.Atan2(refToModel.GetCameraPositionY() + 10, refToModel.GetCameraPositionX() + 10) * MovableObject.degreeKoefficient);
            Console.WriteLine(refToModel.GetDebugData());

            var SkyColor = refToModel.GetSkyColor();

            Console.WriteLine("SkyColor(" + SkyColor.GetR() + ", " + SkyColor.GetG() + ", " + SkyColor.GetB() + ")");


            

        }




    }
}
