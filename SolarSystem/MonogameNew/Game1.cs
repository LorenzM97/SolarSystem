using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.ObjectModel;
using Library_Solarsystem;
using System.IO;
using Newtonsoft.Json;

namespace Monogame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Communication c = new Communication();
        Texture2D background, sun, planet, moon;
        Solarsystem s = new Solarsystem();
        public float geschwindigkeit = 1;

        ObservableCollection<SpaceObject> _listTmp = new ObservableCollection<SpaceObject>();
        ObservableCollection<SpaceObject> _listSolarSystem = new ObservableCollection<SpaceObject>();
        public Game1()
        {
            WidthHeight.screenWidth = 1350;
            WidthHeight.screenHight = 700;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WidthHeight.screenWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = WidthHeight.screenHight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            string jsonText = File.ReadAllText("../../../../../jsonSolarsystems.txt");
            _listTmp = JsonConvert.DeserializeObject<ObservableCollection<SpaceObject>>(jsonText);

            var item = _listTmp[0];
            {
                foreach (var planet in item.ListPlanets)

                    if (planet.Type == "planet")
                    {
                        SpaceObject tempPlanet = new SpaceObject(planet.Type, planet.Name, planet.Size, planet.Distance, planet.Degree);
                        _listSolarSystem.Add(tempPlanet);

                        foreach(var moon in planet.ListMoons)
                        {
                            SpaceObject moonTemp = new SpaceObject(moon.Type, moon.Name, tempPlanet, moon.Size, moon.Distance, moon.Degree);
                            _listSolarSystem.Add(moonTemp);
                            tempPlanet.ListMoons.Add(moonTemp);
                        }
                    } else if(planet.Type == "sun")
                    {
                        _listSolarSystem.Add(new SpaceObject(WidthHeight.screenWidth/2, WidthHeight.screenHight/2, planet.Type, planet.Name, planet.Size));

                    }
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Bilder/Galaxie");
            sun = Content.Load<Texture2D>("Bilder/Sonne");
            planet = Content.Load<Texture2D>("Bilder/Merkur");
            moon = Content.Load<Texture2D>("Bilder/mond");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           
            foreach(var item in _listSolarSystem)
            {
                if(item.Type == "planet")
                {
                    item.Move(geschwindigkeit, WidthHeight.screenWidth / 2, WidthHeight.screenHight / 2);

                }else if (item.Type == "moon")
                {
                    item.Move(geschwindigkeit);
                }
            }

            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), Color.White);

            foreach (var item in _listSolarSystem)
            {
                if (item.Type == "planet")
                {
                   // spriteBatch.Draw(planet, new Rectangle(item.X, item.Y, item.Size, item.Size), Color.White);
                    spriteBatch.Draw(planet, new Vector2(item.X, item.Y), null, Color.White, 0, new Vector2(planet.Width / 2, planet.Height / 2), 1 / 6f, SpriteEffects.None, 0);

                }
                else
                    if (item.Type == "sun")
                {
                   // spriteBatch.Draw(sun, new Rectangle(item.X, item.Y, item.Size, item.Size), Color.White);
                    spriteBatch.Draw(sun, new Vector2(item.X, item.Y), null, Color.White, 0, new Vector2(sun.Width / 2, sun.Height / 2), 1 / 4f, SpriteEffects.None, 0);

                }
                else if (item.Type == "moon")
                {
                    //spriteBatch.Draw(moon, new Rectangle(item.X, item.Y, item.Size, item.Size), Color.White);
                    spriteBatch.Draw(moon, new Vector2(item.X, item.Y), null, Color.White, 0, new Vector2(moon.Width / 2, moon.Height / 2), 1/10f , SpriteEffects.None, 0);
                }


            }




            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
