using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Game
    {
        public static bool _gameOver = false;
        private static Scene[] _scenes;
        private static int _currentSceneIndex;
        
        public static int CurrentSceneIndex
        {
            get
            {
                return _currentSceneIndex;
            }
        }

        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        public static Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return new Scene();

            return _scenes[index];
        }

        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }

        public static int AddScene(Scene scene)
        {

            if (scene == null)
                return -1;

            Scene[] tempArray = new Scene[_scenes.Length + 1];

            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            int index = _scenes.Length;

            tempArray[index] = scene;

            _scenes = tempArray;

            return index;
        }


        public static bool RemoveScene(Scene scene)
        {
            if (scene == null)
                return false;

            bool sceneRemoved = false;

            Scene[] tempArray = new Scene[_scenes.Length - 1];

            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            if (sceneRemoved)
                _scenes = tempArray;

            return sceneRemoved;
        }

        public static void SetCurrentScene(int index)
        {

            if (index < 0 || index >= _scenes.Length)
                return;


            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            _currentSceneIndex = index;
        }

        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }

        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        }

        public Game()
        {
            _scenes = new Scene[0];
        }


       


        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            //Creates a new window for raylib with the Room for the background
            Raylib.InitWindow(1024, 760, "Link's Quest");
            Raylib.SetTargetFPS(60);
            Raylib.LoadImage("Images/Map.png");


            //This is the room that the player will play in
            //Room room = new Room(Raylib., Console.WindowWidth);


          

            //Create a new scene for our actors to exist in
            Scene scene1 = new Scene();

            //Creates the new characters
            Link link = new Link(0, 4, Color.YELLOW, '?');
            Enemy enemy1 = new Enemy(5, 7, new Vector2(5, 6), new Vector2(4, 3), new Vector2(9, 6), Color.BLUE, '!');
            Enemy enemy2 = new Enemy(12, 4, new Vector2(9, 2), new Vector2(5, 2), new Vector2(6, 5), Color.BLUE, '+');
            

            //Sets the characters starting value
            

            

            //Adds the characters to the scene
            scene1.AddActor(link);
            scene1.AddActor(enemy1);
            scene1.AddActor(enemy2);

            //Adds or removes parent or child for Actor if needed
            link.AddChild(enemy2);

        }


        //Called every frame.
        public void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update(deltaTime);

            if (_gameOver == true)
                End();

        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLACK);
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }


        //Called when the game ends.
        public void End()
        {
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while(!_gameOver && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }

            End();
        }
    }
}
